using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ALAP.API.DTO;
using ALAP.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static ALAP.API.DTO.Enum;

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

        [Route("addSubject")]
        [HttpPost]
        public async Task<IActionResult> AddSubject([FromBody]Subjects subject)
        {
            SaveStatus status = await _subjectRepo.AddSubject(subject);

            switch (status)
            {
                case SaveStatus.Success:
                    return Ok();

                case SaveStatus.AlreadyExists:
                    return Conflict();

                default:
                    return StatusCode(500);
            }
        }

        [Route("updateSubject")]
        [HttpPut]
        public async Task<IActionResult> UpdateSubject([FromBody]Subjects subject)
        {
            SaveStatus status = await _subjectRepo.UpdateSubject(subject);

            switch (status)
            {
                case SaveStatus.Success:
                    return Ok();

                default:
                    return StatusCode(500);
            }
        }
    }
}