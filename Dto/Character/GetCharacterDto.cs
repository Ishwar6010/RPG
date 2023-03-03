using RPGgame.Dto.Skill;
using RPGgame.Dto.Weapon;
using RPGgame.Models;
using System.Collections.Generic;

namespace RPGgame.Dto
{
    public class GetCharacterDto
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; } = "Ishwar";
        public int HitPoints { get; set; } = 0;
        public int Strength { get; set; } = 0;
        public int Defense { get; set; } = 0;
        public int Intellgence { get; set; } = 0;
        public RPGClass Type { get; set; } = RPGClass.Mage;
        public GetWeaponDto getWeaponDto { get; set; }
        public List<GetSkillDto> Skills { get; set; }
        public int Fights { get; set; }
        public int Victories { get; set; }
        public int Defeats { get; set; }
    }
}
