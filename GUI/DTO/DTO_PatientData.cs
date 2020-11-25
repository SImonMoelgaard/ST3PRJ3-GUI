using System;
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
        public int Lowmean { get; set; }
        public int Highmean { get; set; }
        public string SocSecNB { get; set; }
        public double Calval { get; set; }
        public double Zeroval { get; set; }

        public DTO_PatientData(int syslow, int syshigh, int dialow, int diahigh, int lowmean, int highmean, string socSecNB, double calval, double zeroval)
        {
            Syslow = syslow;
            Syshigh = syshigh;
            Dialow = dialow;
            Diahigh = diahigh;
            Lowmean = lowmean;
            Highmean = highmean;
            SocSecNB = socSecNB;
            Calval = calval;
            Zeroval = zeroval;
            
        }
    }
}
