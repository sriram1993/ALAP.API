using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ALAP.API.DTO;
using ALAP.API.Repository;
using ALAP.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static ALAP.API.DTO.Enum;

namespace ALAP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IRegisterService _registerRepo;

        public RegisterController(IRegisterService registerRepo)
        {
            _registerRepo = registerRepo;
        }


        [HttpPost]
        public async Task<IActionResult> Register([FromBody]RegisterIDTO registerInput)
        {
            SaveStatus status = await _registerRepo.RegisterUser(registerInput);

            switch (status)
            {
                case SaveStatus.Success:
                    return Ok();

                case SaveStatus.AlreadyExists:
                    return Conflict();

                case SaveStatus.Failure:
                    return StatusCode(500);

                default:
                    return StatusCode(500);

            }
        }
    }
}