﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ALAP.API.DTO
{
    public class StudentData
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int MatriculationNumber { get; set; }

        public List<Subjects> subjects { get; set; }
    }
}
