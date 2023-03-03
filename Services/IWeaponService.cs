using RPGgame.Dto;
using RPGgame.Dto.Weapon;
using RPGgame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPGgame.Services
{
    public interface IWeaponService
    {
        Task<ServiceResponse<GetCharacterDto>> AddWeapon(AddWeaponDto newWeapon) ;
    }
}
