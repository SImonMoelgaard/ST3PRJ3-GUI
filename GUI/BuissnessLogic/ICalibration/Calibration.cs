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
        private double a;
        
        public List<DTO_CalVal> GetCalVal()
        {
            
            calValList=new List<DTO_CalVal>();

            //this is a test
            //calValList.Add(new DTO_CalVal(7,7.8,7.7,7.7,8,7,"bøf"));
            
            //This is the right one
            //calVal.Add(receive.ReceiveCalibration());

            return calValList;
        }

        public void StartCalibration()
        {
            send.StartCalibration();
        }

        public double GetCalibration()
        {
            double calibration = receive.ReceiveCalibration();
            return calibration;
        }

        public List<DTO_CalVal> CalculateAAndB()
        {

            
        }

        public void calculateR2Val()
        {
            throw new System.NotImplementedException();
        }

        public List<DTO_CalVal> SaveCalval(List<int> calReference, List<double> calMeasured, double r2, double a, int b, int zv, string socSecNB)
        {

           DTO_CalVal caldata =new DTO_CalVal(calReference, calMeasured, r2, a, b, zv, socSecNB);


            return localDatabase.SaveCalVal(calReference, calMeasured, r2, a, b, zv, socSecNB);
        }

        

    }
}