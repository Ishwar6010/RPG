using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPGgame.Models
{
    public class Weapon
    {
        public int Id { get; set; }
        public string name { get; set; }
        public int Damage { get; set; }
        public Character character { get; set; }
        public int characterId { get; set; }
    }
}
