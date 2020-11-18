using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLogic
{
    public interface ILocalDatabase
    {

        public void CreateFile();

        public void SaveMeasurement(string socSecNb, double rawData, DateTime date, int sysData, int diaData,
            int alarmData, int pulse, int powerData);

        public void SavePatientData(int SysHigh, int SysLow, int DiaHigh, int DiaLow, string CprPatient);

        public void SaveCalVal(List<int> calReference, List<double> calMeasured, double r2, double a, int b, int zv,
            string socSecNB);

        public bool isUserRegistered(string socSecNb, string pw);

        public bool getSocSecNB(string SocSecNB);


    }
}
