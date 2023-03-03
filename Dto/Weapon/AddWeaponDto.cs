using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPGgame.Dto.Weapon
{
    public class AddWeaponDto
    {
        public string name { get; set; }
        public int Damage { get; set; }
        public int characterId { get; set; }
    }
}
