using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RPGgame.Dto;
using RPGgame.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPGgame.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CharacterSkillController : ControllerBase
    {
        private readonly ICharacterSkillService _characterSkillService;
        public CharacterSkillController(ICharacterSkillService characterSkillService)
        {
            _characterSkillService = characterSkillService;
        }
        [HttpPost]
        public async Task<IActionResult> AddCharacterSkill(AddCharacterSkillDto characterSkillDto)
        {
            return Ok(await _characterSkillService.AddCharacterSkill(characterSkillDto));
        }
    }
}
