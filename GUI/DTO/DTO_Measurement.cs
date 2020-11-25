using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace DTO
{
    public class DTO_Measurement
    {
        public double mmHg { get; set; }
        public DateTime Tid { get; set; }
        public int CalculatedSys { get; set; }
        public int CalculatedDia { get; set; }
        public int CalculatedMean { get; set; }
        public int CalculatedPulse { get; set; }
        public bool HighSys { get; set; }
        public bool LowSys { get; set; }
        public bool HighDia { get; set; }
        public bool LowDia { get; set; }
        public bool HighMean { get; set; }
        public bool LowMean { get; set; }
        public int Batterystatus { get; set; }
        public string SocSecNB { get; set; }

        public DTO_Measurement(string socSecNb,double mmhg, DateTime tid, bool highSys, bool lowSys, bool highDia, bool lowDia, bool highMean, bool lowMean, int sys, int dia, int mean, int pulse, int batterystatus)
        { 
           SocSecNB = socSecNb;
           mmHg = mmhg;
           Tid = tid;
           CalculatedSys = sys;
           CalculatedDia = dia;
           CalculatedMean = mean;
           HighSys = highSys;
           LowSys = lowSys;
           HighDia = highDia;
           LowDia = lowDia;
           HighMean = highMean;
           LowMean = lowMean;




            CalculatedSys = sys;
           CalculatedDia = dia;
           CalculatedMean = mean;
           CalculatedPulse = pulse;
           Batterystatus = batterystatus;
        }
    }
}
