using ALAP.API.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ALAP.API.DTO.Enum;

namespace ALAP.API.Services
{
    public interface IRegisterService
    {
        Task<SaveStatus> RegisterUser(RegisterIDTO registerObj); 
    }
}
