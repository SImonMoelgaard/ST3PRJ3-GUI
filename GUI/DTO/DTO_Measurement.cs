using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTO_Measurement
    {
        //FRA TIDLIGERE
        //public List<double> RawData { get; set; }
        //public DateTime Date { get; set; }
        //public List<int> SysData { get; set; }
        //public List<int> DiaData { get; set; }
        //public List<int> AlarmData { get; set; }
        //public List<int> Pulse { get; set; }
        //public List<int> PowerData { get; set; }

        public double RawData { get; set; }
        public DateTime Date { get; set; }
        public int SysData { get; set; }
        public int DiaData { get; set; }
        public int AlarmData { get; set; }
        public int Pulse { get; set; }
        public int PowerData { get; set; }
        public string SocSecNB { get; set; }

        public DTO_Measurement(string socSecNb,double rawData, DateTime date, int sysData, int diaData,
           int alarmData, int pulse, int powerData)
        { 
           SocSecNB = socSecNb;
           RawData = rawData;
           Date = date;
           SysData = sysData;
           DiaData = diaData;
           AlarmData = alarmData;
           Pulse = pulse;
           PowerData = powerData;
       }
    }
}
