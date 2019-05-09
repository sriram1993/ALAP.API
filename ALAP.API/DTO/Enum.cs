using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ALAP.API.DTO
{
    public class Enum
    {
        public enum SaveStatus
        {
            Success = 0,
            Failure = -1,
            AlreadyExists = -2
        };
    }
}
