using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RPGgame.Data;
using RPGgame.Dto;
using RPGgame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RPGgame.Services
{
    public class CharacterSkillService : ICharacterSkillService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _dataContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CharacterSkillService(IMapper mapper, DataContext dataContext, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _dataContext = dataContext;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ServiceResponse<GetCharacterDto>> AddCharacterSkill(AddCharacterSkillDto newCharacterSkill)
        {
            ServiceResponse<GetCharacterDto> response = new ServiceResponse<GetCharacterDto>();
            try
            {
                Character character = await _dataContext.Characters
                    .Include(x => x.weapon)
                    .Include(x => x.CharacterSkills).ThenInclude(x => x.skill)
                    .FirstOrDefaultAsync(x => x.Id == newCharacterSkill.CharacterId &&
                    x.user.Id == int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)));
                if(character==null)
                {
                    response.Success = false;
                    response.Message = "Character Not Found";
                }
                Skill skill = await _dataContext.Skills.FirstOrDefaultAsync(x => x.Id == newCharacterSkill.SkillId);
                if(skill==null)
                {
                    response.Success = false;
                    response.Message = "Skill Not Found";
                }
                CharacterSkill characterSkill = new CharacterSkill
                {
                    character = character,
                    skill = skill
                };
                await _dataContext.CharacterSkills.AddAsync(characterSkill);
                await _dataContext.SaveChangesAsync();
                response.Data =  _mapper.Map<GetCharacterDto>(character);
            }
            catch(Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}
