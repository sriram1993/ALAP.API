using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ALAP.API.DTO
{
    public class User
    {
        public string id { get; set; }

        public string email { get; set; }

        public string name { get; set; }

        public Role role { get; set; } 

    }

    public class Role
    {
        public string name { get; set; }
    }
}
