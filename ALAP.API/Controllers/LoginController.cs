using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ALAP.API.DTO;
using ALAP.API.Repository;
using ALAP.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ALAP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginRepo;

        public LoginController(ILoginService loginRepo)
        {
            _loginRepo = loginRepo;
        }

       
        [HttpPost]
        public async Task<ActionResult<LoginODTO>> Login([FromBody]LoginIDTO login)
        {
            return await _loginRepo.Login(login);
        }
    }
}