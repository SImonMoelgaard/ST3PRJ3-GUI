using System;
using System.Collections.Generic;
using DTO;

namespace DataAccessLogic
{
    public interface IReceiveRPi
    {
        public DTO_Measurement ReceiveMeasurment(string socSecNb, double mmhg, DateTime tid, bool highSys, bool lowSys, bool highDia, bool lowDia, bool highMean, bool lowMean, int sys, int dia, int mean, int pulse, int batterystatus);

        public double ReceiveCalibration(double calval);


    }
}