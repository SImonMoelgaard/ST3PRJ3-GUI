﻿using System;
using System.Collections.Generic;

namespace DTO
{
    public class DTO_Measurement
    {
       public List<double> RawData { get; set; }
       public DateTime Date { get; set; }
       public List<int> SysData { get; set; }
       public List<int> DiaData { get; set; }
       public List<int> AlarmData { get; set; }
       public List<int> Pulse { get; set; }
       public List<int> PowerData { get; set; }

       public DTO_Measurement(List<double> rawData, DateTime date, List<int> sysData, List<int> diaData,
           List<int> alarmData, List<int> pulse, List<int> powerData)
       {
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