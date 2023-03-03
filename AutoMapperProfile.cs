using AutoMapper;
using RPGgame.Dto;
using RPGgame.Dto.Fight;
using RPGgame.Dto.Skill;
using RPGgame.Dto.Weapon;
using RPGgame.Models;
using System.Linq;

namespace RPGgame
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character, GetCharacterDto>().ForMember(dto=>dto.Skills,x=>x.MapFrom(x=>x.CharacterSkills.Select(x=>x.skill)));
            CreateMap<AddCharacterDto, Character>();
            CreateMap<Weapon, GetWeaponDto>();
            CreateMap<Skill, GetSkillDto>();
            CreateMap<Character, HighScoreDto>();
        }
    }
}
