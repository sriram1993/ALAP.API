using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ALAP.API.DTO
{
    public class LoginODTO
    {
        public int StudentID { get; set; }

        public string Email { get; set; }

        public bool IsAdmin { get; set; }

        public Guid Token { get; set; }
    }
}
