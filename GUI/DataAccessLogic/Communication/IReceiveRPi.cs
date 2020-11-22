using System;
using System.Collections.Generic;
using DTO;

namespace DataAccessLogic
{
    public interface IReceiveRPi
    {
        public DTO_Measurement ReceiveMeasurment(string socSecNb, double rawData, DateTime date, int sysData, int diaData,
            int alarmData, int pulse, int powerData);

        public double ReceiveCalibration();


    }
}