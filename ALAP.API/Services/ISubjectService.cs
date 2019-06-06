using ALAP.API.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ALAP.API.DTO.Enum;

namespace ALAP.API.Services
{
    public interface ISubjectService
    {
        Task<List<Subjects>> GetAllSubjects();

        Task<SaveStatus> AddSubject(Subjects subject);

        Task<SaveStatus> UpdateSubject(Subjects subject);
    }
}
