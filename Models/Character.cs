using System.Collections.Generic;

namespace RPGgame.Models
{
    public class Character
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; } = "Ishwar";
        public int HitPoints { get; set; } = 0;
        public int Strength { get; set; } = 0;
        public int Defense { get; set; } = 0;
        public int Intellgence { get; set; } = 0;
        public RPGClass Type { get; set; } = RPGClass.Mage;
        public User user { get; set; }
        public Weapon weapon { get; set; }
        public List<CharacterSkill> CharacterSkills { get; set; }
        public int Fights { get; set; }
        public int Victories { get; set; }
        public int Defeats { get; set; }
    }
}
