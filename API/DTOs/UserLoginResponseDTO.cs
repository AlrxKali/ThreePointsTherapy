using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class UserLoginResponseDTO
    {
        public User User { get; set; }
        public string Token { get; set; }
    }
}
