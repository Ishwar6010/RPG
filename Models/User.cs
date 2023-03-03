using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RPGgame.Models
{
    public class User
    {
        public int Id { get; set; }

        public string userName { get; set; }

        public byte[] passwordHash { get; set; }

        public byte[] passwordSalt { get; set; }

        public List<Character> Characters { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
