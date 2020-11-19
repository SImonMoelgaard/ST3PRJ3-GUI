using System.Collections.Generic;
using DTO;

namespace DataAccessLogic
{
    public interface IReceiveRPi
    {
        public DTO_Measurement ReceiveMeasurment();

        public object ReceiveCalibration(List<int> calReference, List<double> calMeasured, double r2, double a, int b, int zv, string socSecNB);


    }
}