using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ALAP.API.DTO
{
    public class Subjects
    {
        public int SubjectID { get; set; }

        public int SubjectMappingID { get; set; }

        public string SubjectName { get; set; }

        public string Module { get; set; }

        public bool isSelected { get; set; }

    }
}
