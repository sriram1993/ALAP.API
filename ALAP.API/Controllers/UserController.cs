using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ALAP.API.DTO;
using ALAP.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ALAP.API.Controllers
{
    [Route("api/")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userRepo;

        public UserController(IUserService userRepo)
        {
            _userRepo = userRepo;
        }

        [HttpGet]
        [Route("getStudentData/{studentID}")]
        public async Task<IActionResult> GetStudentData(int studentID)
        {
            StudentData studData = await _userRepo.GetStudentData(studentID);
            if(studData != null)
            {
                return Ok(studData);
            }
            else
            {
                return StatusCode(500);
            }
        }
    }
}