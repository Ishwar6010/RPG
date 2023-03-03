using RPGgame.Dto;
using RPGgame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPGgame.Services
{
    public interface ICharacterSkillService
    {
        Task<ServiceResponse<GetCharacterDto>> AddCharacterSkill(AddCharacterSkillDto newCharacterSkill);

    }
}
