using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text.RegularExpressions;
using DTO;
using DataAccessLogic;



namespace BuissnessLogic
{
    public class Calibration : ICalibration
    {
        /// <summary>
        /// References
        /// </summary>
        readonly IReceiveRPi receive=new ReceiveRPi();
        readonly ISendRPi send=new SendRPi();
        readonly ILocalDatabase localDatabase = new LocalDatabase();

        /// <summary>
        /// Values
        /// </summary>
        private double rVal;
        private double _r2;
        private double zeroval;

        /// <summary>
        /// Lists
        /// </summary>
        private readonly List<DTO_CalVal> LinearRegression = new List<DTO_CalVal>();

        

        /// <summary>
        /// This method calculates the a and b in the linear equation for the calibration
        /// </summary>
        /// <param name="calReference"></param>
        /// <param name="calMeasured"></param>
        /// <param name="r2"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="zv"></param>
        /// <returns></returns>
        public List<DTO_CalVal> CalculateAAndB(List<int> calReference, List<double> calMeasured, double r2, double a, double b, double zv)
        {
            double xAvg = 0;
            double yAvg = 0;

            for (int i = 0; i < calReference.Count; i++)
            {
                xAvg += calReference[i];
                yAvg += calMeasured[i];
            }

            xAvg = xAvg / calReference.Count;
            yAvg = yAvg / calMeasured.Count;

            double v1 = 0;
            double v2 = 0;

            for (int i = 0; i < calReference.Count; i++)
            {
                v1 += (calReference[i] - xAvg) * (calMeasured[i] - yAvg);
                v2 += Math.Pow(calReference[i] - xAvg, 2);
            }

            a = v1 / v2;
            b = Convert.ToInt32(yAvg - a * xAvg);

            LinearRegression.Add(new DTO_CalVal(calReference,calMeasured,_r2,a,b,zv,DateTime.Now));

            return LinearRegression;
        }

        /// <summary>
        /// This method calculates the R2 value
        /// </summary>
        /// <param name="calReference"></param>
        /// <param name="calMeasured"></param>
        /// <param name="r2"></param>
        /// <returns></returns>
        public double CalculateR2Val(List<int> calReference, List<double> calMeasured,double r2)
        {
            double zX = 0;
            double zY = 0;
            double zXY = 0;
            double zX2 = 0;
            double zY2 = 0;

            for (int i = 0; i < calReference.Count; i++)
            {
                zX += calReference[i];
                zY += calMeasured[i];
            }

            for (int i = 0; i < calReference.Count; i++)
            {
                zXY += calReference[i] * calMeasured[i];
            }

            for (int i = 0; i < calReference.Count; i++)
            {
                zX2 += Math.Pow(calReference[i], 2);
                zY2 += Math.Pow(calMeasured[i], 2);
            }

            rVal = ((calReference.Count * zXY) - (zX * zY)) / Math.Sqrt(((calReference.Count * zX2 -
                                                                          (Math.Pow(zX, 2))) * (calReference.Count * zY2 -
                                                                         (Math.Pow(zY, 2)))));

            _r2 = Math.Pow(rVal, 2);
            r2 = _r2;
            
            return r2;
        }

        /// <summary>
        /// This method saves the calibration in a file
        /// </summary>
        /// <param name="calReference"></param>
        /// <param name="calMeasured"></param>
        /// <param name="r2"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="zv"></param>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public List<DTO_CalVal> SaveCalval(List<int> calReference, List<double> calMeasured, double r2, double a, double b, double zv, DateTime datetime)
        {
            foreach (var data in LinearRegression)
            {
                calReference = data.CalReference;
                calMeasured = data.CalMeasured;
                r2 = _r2;
                a = data.A;
                b = data.B;
                zv = zeroval;
                datetime = data.Datetime;
            }

            return localDatabase.SaveCalVal(calReference, calMeasured, r2,a,b,zv,datetime);
        }
    }
}