using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPGgame.Models
{
    public class Skill
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Damage { get; set; }
        public List<CharacterSkill> CharacterSkills { get; set; }
    }
}
