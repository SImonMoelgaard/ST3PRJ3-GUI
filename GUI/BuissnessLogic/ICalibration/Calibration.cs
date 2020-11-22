using System.Collections.Generic;
using DTO;
using DataAccessLogic;

namespace BuissnessLogic
{
    public class Calibration : ICalibration
    {
        IReceiveRPi receive=new ReceiveRPi();
        ISendRPi send=new SendRPi();
        private double r2Val;
        private List<DTO_CalVal> calValList;
        
        public List<DTO_CalVal> GetCalVal()
        {
            calValList=new List<DTO_CalVal>();

            //this is a test
            calValList.Add(new DTO_CalVal(7,7.8,7.7,7.7,8,7,"bøf"));
            
            //This is the right one
            //calVal.Add(receive.ReceiveCalibration());

            return calValList;
        }

        public void StartCalibration()
        {
            send.StartCalibration();
        }

        public string GetCalibration()
        {
            string calibration = receive.ReceiveCalibration();
            return calibration;
        }

        public void calculateR2Val()
        {
            throw new System.NotImplementedException();
        }
    }
}