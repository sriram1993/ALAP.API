﻿using ALAP.API.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ALAP.API.Services
{
    public interface ILoginService
    {
        Task<LoginODTO> Login(LoginIDTO loginInput);
    }
}
