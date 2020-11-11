﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class DTO_PatientData
    {
        public int Syslow { get; set; }
        public int Syshigh { get; set; }
        public int Dialow { get; set; }
        public int Diahigh { get; set; }
        public string SocSecNB { get; set; }

        public DTO_PatientData(int syslow, int syshigh, int dialow, int diahigh, string socSecNB)
        {
            Syslow = syslow;
            Syshigh = syshigh;
            Dialow = dialow;
            Diahigh = diahigh;
            SocSecNB = socSecNB;
        }
    }
}