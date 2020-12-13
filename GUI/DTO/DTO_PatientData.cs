using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class DTO_PatientData
    {
        /// <summary>
        /// Limit value - high systolic
        /// </summary>
        public int HighSys
        {
            get;
            set;
        }

        /// <summary>
        /// Limit value - low systolic
        /// </summary>
        public int LowSys
        {
            get;
            set;
        }

        /// <summary>
        /// Limit value - high diastolic
        /// </summary>
        public int HighDia
        {
            get;
            set;
        }

        /// <summary>
        /// Limit value - low diastolic
        /// </summary>
        public int LowDia
        {
            get;
            set;
        }

        /// <summary>
        /// Limit value - high mean
        /// </summary>
        public int HighMean
        {
            get;
            set;
        }

        /// <summary>
        /// Limit value - low mean
        /// </summary>
        public int LowMean
        {
            get;
            set;
        }

        /// <summary>
        /// Zero value
        /// </summary>
        public double ZeroVal
        {
            get;
            set;
        }

        /// <summary>
        /// CPR
        /// </summary>
        public string SocSecNB { get; set; }

        /// <summary>
        /// Calibration value
        /// </summary>
        public double CalVal { get; set; }


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="sysLow"></param>
        /// <param name="sysHigh"></param>
        /// <param name="diaLow"></param>
        /// <param name="diaHigh"></param>
        /// <param name="lowMean"></param>
        /// <param name="highMean"></param>
        /// <param name="socSecNB"></param>
        /// <param name="calVal"></param>
        /// <param name="zeroVal"></param>
        public DTO_PatientData(int sysLow, int sysHigh, int diaLow, int diaHigh, int lowMean, int highMean, string socSecNB, double calVal, double zeroVal)
        {
            HighSys = sysHigh;
            LowSys = sysLow;
            HighDia = diaHigh;
            LowDia = diaLow;
            HighMean = highMean;
            LowMean = lowMean;
            ZeroVal = zeroVal;
            CalVal = calVal;
            SocSecNB = socSecNB;
        }
    }
}
