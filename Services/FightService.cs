using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RPGgame.Data;
using RPGgame.Dto.Fight;
using RPGgame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPGgame.Services
{
    public class FightService:IFightService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        public FightService(DataContext dataContext,IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<AttackResultDto>> WeaponAttack(WeaponAttackDto request)
        {
            ServiceResponse<AttackResultDto> serviceResponse = new ServiceResponse<AttackResultDto>();
            try
            {
                Character attacker = await _dataContext.Characters.Include(x => x.weapon).FirstOrDefaultAsync(x => x.Id == request.AttackerId);
                Character opponent = await _dataContext.Characters.FirstOrDefaultAsync(x => x.Id == request.OpponentId);
                int damage = weaponAttack(attacker, opponent);
                if (opponent.HitPoints < 0)
                    serviceResponse.Message = $"{opponent.Name} has been defeated";
                _dataContext.Characters.Update(opponent);
                await _dataContext.SaveChangesAsync();
                serviceResponse.Data = new AttackResultDto
                {
                    Attacker = attacker.Name,
                    Opponent = opponent.Name,
                    Damage = damage,
                    AttackerHP = attacker.HitPoints,
                    OpponentHP = opponent.HitPoints
                };
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        private static int weaponAttack(Character attacker, Character opponent)
        {
            int damage = attacker.weapon.Damage + (new Random().Next(attacker.Strength));
            damage = damage - (new Random().Next(opponent.Defense));
            if (damage > 0)
            {
                opponent.HitPoints = opponent.HitPoints - damage;
            }

            return damage;
        }

        public async Task<ServiceResponse<AttackResultDto>> SkillAttack(SkillAttackDto request)
        {
            ServiceResponse<AttackResultDto> serviceResponse = new ServiceResponse<AttackResultDto>();
            try
            {
                Character attacker = await _dataContext.Characters.Include(x => x.CharacterSkills).ThenInclude(x => x.skill).FirstOrDefaultAsync(x => x.Id == request.AttackerId);
                Character opponent = await _dataContext.Characters.FirstOrDefaultAsync(x => x.Id == request.OpponentId);
                CharacterSkill characterSkill = attacker.CharacterSkills.FirstOrDefault(x => x.SkillId == request.SkillId);
                if (characterSkill == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = $"{attacker.Name} does not knows the skill";
                }
                int damage = skillAttack(attacker, opponent, characterSkill);
                if (opponent.HitPoints < 0)
                    serviceResponse.Message = $"{opponent.Name} has been defeated";
                _dataContext.Characters.Update(opponent);
                await _dataContext.SaveChangesAsync();
                serviceResponse.Data = new AttackResultDto
                {
                    Attacker = attacker.Name,
                    Opponent = opponent.Name,
                    Damage = damage,
                    AttackerHP = attacker.HitPoints,
                    OpponentHP = opponent.HitPoints
                };
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        private static int skillAttack(Character attacker, Character opponent, CharacterSkill characterSkill)
        {
            int damage = characterSkill.skill.Damage + (new Random().Next(attacker.Intellgence));
            damage = damage - (new Random().Next(opponent.Defense));
            if (damage > 0)
            {
                opponent.HitPoints = opponent.HitPoints - damage;
            }

            return damage;
        }

        public async Task<ServiceResponse<FightResultDto>> Fight(FightRequestDto request)
        {
            ServiceResponse<FightResultDto> response = new ServiceResponse<FightResultDto>
            {
                Data = new FightResultDto()
            };
            try
            {
                List<Character> characters = await _dataContext.Characters.Include(x => x.weapon).Include(x => x.CharacterSkills).ThenInclude(x => x.skill)
                    .Where(x => request.CharacterIds.Contains(x.Id)).ToListAsync();
                bool defeated = false;
                while(!defeated)
                {
                    foreach(Character attacker in characters)
                    {
                        List<Character> opponents = characters.Where(x => x.Id != attacker.Id).ToList();
                        Character opponent = opponents[new Random().Next(opponents.Count)];
                        int damage = 0;
                        string attackUsed = string.Empty;
                        bool useWeapon = new Random().Next(2) == 0;
                        if(useWeapon)
                        {
                            attackUsed = attacker.weapon.name;
                            damage = weaponAttack(attacker, opponent);
                        }
                        else
                        {
                            int randomSkill = new Random().Next(attacker.CharacterSkills.Count);
                            attackUsed = attacker.CharacterSkills[randomSkill].skill.Name;
                            damage = skillAttack(attacker, opponent, attacker.CharacterSkills[randomSkill]);
                        }
                        response.Data.Logs.Add($"{attacker.Name} attacks {opponent.Name} using {attackUsed} with {(damage >= 0 ? damage:0)} damage");
                        if(opponent.HitPoints<=0)
                        {
                            defeated = true;
                            attacker.Victories++;
                            opponent.Defeats++;
                            response.Data.Logs.Add($"{opponent.Name} has been defeated");
                            response.Data.Logs.Add($"{attacker.Name} won having still {attacker.HitPoints} Hit Points");
                            break;
                        }
                    }
                }
                characters.ForEach(x => {
                    x.Fights++;
                    x.HitPoints = 100;
                    }) ;
                _dataContext.Characters.UpdateRange(characters);
                await _dataContext.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<List<HighScoreDto>>> GetHighScore()
        {
            
            List<Character> characters = await _dataContext.Characters.Where(x => x.Fights > 0).OrderByDescending(x => x.Victories).ThenBy(x => x.Defeats).ToListAsync();
            ServiceResponse<List<HighScoreDto>> response = new ServiceResponse<List<HighScoreDto>>
            {
                Data = characters.Select(x => _mapper.Map<HighScoreDto>(x)).ToList()
            };
            return response;
        }
    }
}
