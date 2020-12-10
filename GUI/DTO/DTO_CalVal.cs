using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class DTO_CalVal
    {
        /// <summary>
        /// List of calibration reference values
        /// </summary>
        public List<int> CalReference { get; set; }

        /// <summary>
        /// List of calibration values
        /// </summary>
        public List<double> CalMeasured { get; set; }

        /// <summary>
        /// R2 value
        /// </summary>
        public double R2 { get; set; }

        /// <summary>
        /// Slope value also known as calibration value
        /// </summary>
        public double A { get; set; } 

        /// <summary>
        /// Y-intercept value
        /// </summary>
        public double B { get; set; }

        /// <summary>
        /// Zero value
        /// </summary>
        public double Zv { get; set; }
       
        /// <summary>
        /// DateTime
        /// </summary>
        public DateTime Datetime { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="calReference"></param>
        /// <param name="calMeasured"></param>
        /// <param name="r2"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="zv"></param>
        /// <param name="datetime"></param>
        public DTO_CalVal(List<int> calReference, List<double> calMeasured, double r2,double a, double b, double zv,DateTime datetime)
        {
            //Reference values in mmHg
            CalReference = calReference;

            //Calibration values in voltage
            CalMeasured = calMeasured;

            //R2 value
            R2 = r2;

            //Slope aka. calibration value from linear regression
            A = a;

            //Y-intercept value from linear regression
            B = b;

            //Zero value
            Zv = zv;

            //DateTime
            Datetime = datetime;
        }
    }
}
