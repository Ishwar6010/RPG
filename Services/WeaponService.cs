using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RPGgame.Data;
using RPGgame.Dto;
using RPGgame.Dto.Weapon;
using RPGgame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RPGgame.Services
{
    public class WeaponService : IWeaponService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _dataContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public WeaponService(IMapper mapper, DataContext dataContext, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _dataContext = dataContext;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ServiceResponse<GetCharacterDto>> AddWeapon(AddWeaponDto newWeapon)
        {
            ServiceResponse<GetCharacterDto> response = new ServiceResponse<GetCharacterDto>();
            try
            {
                Character character = await _dataContext.Characters.FirstOrDefaultAsync(x=>x.Id==newWeapon.characterId &&
                x.user.Id==int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)));
                if(character==null)
                {
                    response.Success = false;
                    response.Message = "character not found";
                }
                Weapon weapon = new Weapon
                {
                    name = newWeapon.name,
                    characterId = newWeapon.characterId,
                    character = character
                };
                await _dataContext.Weapons.AddAsync(weapon);
                await _dataContext.SaveChangesAsync();
                response.Data = _mapper.Map<GetCharacterDto>(weapon);
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
