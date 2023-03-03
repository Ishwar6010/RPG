using RPGgame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPGgame.Dto.Weapon
{
    public class GetWeaponDto
    {
        public int Id { get; set; }
        public string name { get; set; }
        public int Damage { get; set; }
        public Character character { get; set; }
        public int characterId { get; set; }
    }
}
