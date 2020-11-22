using System;
using System.Collections.Generic;
using System.Text;
using DTO;

namespace DataAccessLogic
{
    public interface ILocalDatabase
    {

        

        public object SaveMeasurement(string socSecNb, double rawData, DateTime date, int sysData, int diaData,
            int alarmData, int pulse, int powerData);

        public object SavePatientData(int SysHigh, int SysLow, int DiaHigh, int DiaLow, string CprPatient);

        public List<DTO_CalVal> SaveCalVal(List<int> calReference, List<double> calMeasured, double r2, double a, int b, int zv,
            string socSecNB);

        


    }
}
