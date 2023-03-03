using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPGgame.Models
{
    public class CharacterSkill
    {
        public int CharacterId { get; set; }
        public Character character { get; set; }
        public int SkillId { get; set; }
        public Skill skill { get; set; }
    }
}
