using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ALAP.API.DTO;
using ALAP.API.Services;
using ALAP.API.Utilities;
using IronOcr;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static ALAP.API.DTO.Enum;

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

        [HttpGet]
        [Route("getStudentRequestData")]
        public async Task<IActionResult> GetStudentRequestData(string type="all")
        {

            //OcrResult result = OCRExtraction.ExtractText();
            List<StudentData> studData = await _userRepo.GetStudentRequestData(type);
            if (studData != null)
            {
                return Ok(studData);
            }
            else
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Route("saveStudentData")]
        public async Task<IActionResult> SaveStudentData([FromBody]StudentData studentData)
        {
            SaveStatus status = await _userRepo.SaveStudentData(studentData);

            switch (status)
            {
                case SaveStatus.Success:
                    return Ok();

                default:
                    return StatusCode(500);

            }
        }


        [HttpPost]
        [Route("approveLearningAgreement")]
        public async Task<IActionResult> ApproveRequest([FromBody]StudentData studentData)
        {
            SaveStatus status = await _userRepo.ApproveRequest(studentData);

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