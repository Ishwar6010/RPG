using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RPGgame.Dto;
using RPGgame.Models;
using RPGgame.Services;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RPGgame.Controllers
{
    [Authorize(Roles ="Player,Admin")]
    [ApiController]
    [Route("[controller]")]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _characterService;
        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            int id = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
            return Ok(await _characterService.GetAllCharacters());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(int id)
        {
            return Ok(await _characterService.GetCharacter(id));
        }
        [HttpPost]
        public async Task<IActionResult> AddCharacter(AddCharacterDto ch)
        {
            return Ok(await _characterService.AddCharacters(ch));
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCharacter(UpdateCharacterDto ch)
        {
            ServiceResponse<GetCharacterDto> serviceResponse = await _characterService.UpdateCharacter(ch);
            if (serviceResponse == null)
                return NotFound(serviceResponse);
            return Ok(await _characterService.UpdateCharacter(ch));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCharacter(int id)
        {
            ServiceResponse<List<GetCharacterDto>> serviceResponse = await _characterService.DeleteCharacter(id);
            if (serviceResponse == null)
                return NotFound(serviceResponse);
            return Ok(await _characterService.DeleteCharacter(id));
        }
    }
}
