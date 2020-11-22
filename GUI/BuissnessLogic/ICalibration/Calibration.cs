using System;
using System.Collections.Generic;
using DTO;
using DataAccessLogic;


namespace BuissnessLogic
{
    public class Calibration : ICalibration
    {
        IReceiveRPi receive=new ReceiveRPi();
        ISendRPi send=new SendRPi();
        ILocalDatabase localDatabase = new LocalDatabase();
        private double r2Val;
        private List<DTO_CalVal> calValList;
        private double calval;
        private List<DTO_CalVal> linearRegression;

        public List<DTO_CalVal> GetCalVal()
        {
            
            calValList=new List<DTO_CalVal>();

            //this is a test
            //calValList.Add(new DTO_CalVal(7,7.8,7.7,7.7,8,7,"bøf"));
            
            //This is the right one
            //calVal.Add(receive.ReceiveCalibration());

            return calValList;
        }

        public string StartCalibration()
        {
            return send.StartCalibration();
        }

        

        public double getCalibration(double CalVal)
        {
           
            return receive.ReceiveCalibration(CalVal);
        }

        

        public List<DTO_CalVal> CalculateAAndB(List<int> calReference, List<double> calMeasured, double r2, double a, int b, int zv)
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

            List<DTO_CalVal> linearRegression=new List<DTO_CalVal>();
            linearRegression.Add(new DTO_CalVal(calReference,calMeasured,r2,a,b,zv,""));

            return linearRegression;
        }

        public double CalculateR2Val(List<int> calReference, List<double> calMeasured)
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

            r2Val = ((calReference.Count * zXY) - (zX * zY)) / Math.Sqrt((calReference.Count * zX2) -
                                                                         (Math.Pow(zX, 2)) * calReference.Count * zY2 -
                                                                         (Math.Pow(zY, 2)));

            return r2Val;
        }

        public List<DTO_CalVal> SaveCalval(List<int> calReference, List<double> calMeasured, double r2, double a, int b, int zv, string socSecNB)
        {

           DTO_CalVal caldata =new DTO_CalVal(calReference, calMeasured, r2, a, b, zv, socSecNB);


            return localDatabase.SaveCalVal(calReference, calMeasured, r2, a, b, zv, socSecNB);
        }

        

    }
}