﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ALAP.API.DTO
{
    public class Transcript
    {
        public string FileData { get; set; }

        public string FileName { get; set; }

        public string OcrJson { get; set; }
    }
}
