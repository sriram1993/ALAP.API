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
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService _subjectRepo;

        public SubjectController(ISubjectService subjectRepo)
        {
            _subjectRepo = subjectRepo;
        }


        [Route("getSubjects")]
        [HttpGet]
        public async Task<ActionResult<List<Subjects>>> GetAllSubjects()
        {
            return await _subjectRepo.GetAllSubjects();
        }
    }
}