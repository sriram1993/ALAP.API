using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ALAP.API.DTO
{
    public class RegisterIDTO
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int MatriculationNumber { get; set; }

        public LoginIDTO loginDetails { get; set; }
    }
}
