using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using RPGgame.Data;
using RPGgame.Dto;
using RPGgame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RPGgame.Services
{
    public class CharacterService:ICharacterService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _dataContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private static List<Character> characters = new List<Character>
        {
            new Character
            {
                Id = 1,
                Name = "Ishwar"
            },
            new Character
            {
                Id = 2,
                Name = "Gupta"
            }
        };
        public CharacterService(IMapper mapper,DataContext dataContext,IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _dataContext = dataContext;
            _httpContextAccessor = httpContextAccessor;
        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        private int GetUserRole() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role));

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            List<Character> dbCharacters = GetUserRole().Equals("Admin")? await _dataContext.Characters.ToListAsync():
                await _dataContext.Characters.Where(x=>x.user.Id==GetUserId()).ToListAsync();
            serviceResponse.Data = dbCharacters.Select(x=> _mapper.Map<GetCharacterDto>(x)).ToList();
            return serviceResponse;
        }
        public async Task<ServiceResponse<GetCharacterDto>> GetCharacter(int id)
        {
            ServiceResponse<GetCharacterDto> serviceResponse = new ServiceResponse<GetCharacterDto>();
            Character dbCharacter = await _dataContext.Characters.FirstOrDefaultAsync(x => x.Id == id&&x.user.Id == GetUserId());
            //GetCharacterDto character = _mapper.Map<GetCharacterDto>(characters.FirstOrDefault(x => x.Id == id));
            GetCharacterDto character = _mapper.Map<GetCharacterDto>(dbCharacter);
            serviceResponse.Data = character;
            return serviceResponse;
        }
        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacters(AddCharacterDto ch)
        {
            ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            Character character = _mapper.Map<Character>(ch);
            //character.Id = characters.Max(x => x.Id) + 1;
            //characters.Add(character);
            character.user = await _dataContext.Users.FirstOrDefaultAsync(x => x.Id == GetUserId());
            await _dataContext.Characters.AddAsync(character);
            await _dataContext.SaveChangesAsync();
            serviceResponse.Data = (_dataContext.Characters.Where(x=>x.user.Id==GetUserId()).Select(x=>_mapper.Map<GetCharacterDto>(x))).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
        {
            ServiceResponse<GetCharacterDto> serviceResponse = new ServiceResponse<GetCharacterDto>();
            try
            {
                Character dbCharacter = await _dataContext.Characters.Include(x=>x.user).FirstOrDefaultAsync(x => x.Id == updatedCharacter.Id && x.user.Id == GetUserId());
                //Character character = characters.FirstOrDefault(x => x.Id == updatedCharacter.Id);
                //character.Id = updatedCharacter.Id;
                //character.Name = updatedCharacter.Name;
                //character.Strength = updatedCharacter.Strength;
                //character.Defense = updatedCharacter.Defense;
                //character.Intellgence = updatedCharacter.Intellgence;
                //Character character = characters.FirstOrDefault(x => x.Id == updatedCharacter.Id);
                if(dbCharacter!=null)
                {
                    dbCharacter.Id = updatedCharacter.Id;
                    dbCharacter.Name = updatedCharacter.Name;
                    dbCharacter.Strength = updatedCharacter.Strength;
                    dbCharacter.Defense = updatedCharacter.Defense;
                    dbCharacter.Intellgence = updatedCharacter.Intellgence;
                    serviceResponse.Data = _mapper.Map<GetCharacterDto>(dbCharacter);
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Character Not Found";
                }
                return serviceResponse;
            }
            catch(Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
                return serviceResponse;
            }

        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            try
            {
                Character character = characters.FirstOrDefault(x => x.Id == id&&x.user.Id==GetUserId());
                if(character!=null)
                {
                    _dataContext.Characters.Remove(character);
                    await _dataContext.SaveChangesAsync();
                    serviceResponse.Data = (_dataContext.Characters.Where(x => x.user.Id == GetUserId()).Select(x => _mapper.Map<GetCharacterDto>(x)).ToList());
                }
                //characters.Remove(character);
                //await _dataContext.Characters.Delete;
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Character Not Found";
                }
                
                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
                return serviceResponse;
            }
        }
    }
}
