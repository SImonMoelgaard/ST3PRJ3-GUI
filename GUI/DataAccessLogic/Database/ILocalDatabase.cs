using System;
using System.Collections.Generic;
using System.Text;
using DTO;

namespace DataAccessLogic
{
    public interface ILocalDatabase
    {

        

        public object SaveMeasurement(string socSecNb, double mmhg, DateTime tid, bool highSys, bool lowSys, bool highDia, bool lowDia, bool highMean, bool lowMean, int sys, int dia, int mean, int pulse, int batterystatus);

        public object SavePatientData(int SysHigh, int SysLow, int DiaHigh, int DiaLow, int Meanlow, int Meanhigh, string CprPatient,
            double Calval, double Zeroval);

        public List<DTO_CalVal> SaveCalVal(List<int> calReference, List<double> calMeasured, double r2, double a, double b, double zv,
            DateTime datetime);

        public object ReadPatientData(int SysHigh, int SysLow, int DiaHigh, int DiaLow, int Meanlow, int Meanhigh,
            string CprPatient,
            double Calval, double Zeroval);

        public List<DTO_CalVal> GetCalVal();



    }
}
