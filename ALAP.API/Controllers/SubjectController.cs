using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ALAP.API.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ALAP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        //private readonly IRegisterService _registerRepo;

        //public RegisterController(IRegisterService registerRepo)
        //{
        //    _registerRepo = registerRepo;
        //}
        [HttpPost]
        public async Task<ActionResult<List<Subjects>>> GetAllSubjects()
        {
            await Task.Delay(1000);
        }
    }
}