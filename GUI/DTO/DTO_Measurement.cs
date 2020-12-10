using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace DTO
{
    public class DTO_Measurement
    {
        /// <summary>
        /// mmHg
        /// </summary>
        public double mmHg { get; set; }

        /// <summary>
        /// Tid
        /// </summary>
        public DateTime Tid { get; set; }

        /// <summary>
        /// Calculated systolic
        /// </summary>
        public int CalculatedSys { get; set; }

        /// <summary>
        /// Calculated diastolic
        /// </summary>
        public int CalculatedDia { get; set; }

        /// <summary>
        /// Calculated mean
        /// </summary>
        public int CalculatedMean { get; set; }

        /// <summary>
        /// Calculated pulse
        /// </summary>
        public int CalculatedPulse { get; set; }

        /// <summary>
        /// Limit value - high systolic
        /// </summary>
        public bool HighSys { get; set; }

        /// <summary>
        /// Limit value - Low systolic
        /// </summary>
        public bool LowSys { get; set; }

        /// <summary>
        /// Limit value - High Diastolic
        /// </summary>
        public bool HighDia { get; set; }

        /// <summary>
        /// Limit value - Low diastolic
        /// </summary>
        public bool LowDia { get; set; }

        /// <summary>
        /// Limit value - High Mean
        /// </summary>
        public bool HighMean { get; set; }

        /// <summary>
        /// Limit value - Low Mean
        /// </summary>
        public bool LowMean { get; set; }

        /// <summary>
        /// Battery status
        /// </summary>
        public int BatteryStatus { get; set; }

        /// <summary>
        /// CPR
        /// </summary>
        public string SocSecNB { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="socSecNb"></param>
        /// <param name="mmhg"></param>
        /// <param name="tid"></param>
        /// <param name="highSys"></param>
        /// <param name="lowSys"></param>
        /// <param name="highDia"></param>
        /// <param name="lowDia"></param>
        /// <param name="highMean"></param>
        /// <param name="lowMean"></param>
        /// <param name="sys"></param>
        /// <param name="dia"></param>
        /// <param name="mean"></param>
        /// <param name="pulse"></param>
        /// <param name="batteryStatus"></param>
        public DTO_Measurement(string socSecNb,double mmhg, DateTime tid, bool highSys, bool lowSys, bool highDia, bool lowDia, bool highMean, bool lowMean, int sys, int dia, int mean, int pulse, int batteryStatus)
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
           BatteryStatus = batteryStatus;
        }
    }
}
