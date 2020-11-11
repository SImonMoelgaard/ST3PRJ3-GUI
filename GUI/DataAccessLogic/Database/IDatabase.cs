using System;
using System.Collections.Generic;
using DTO;

namespace DataAccessLogic
{
    public interface IDatabase
    {
        public List<DTO_Measurement> GetMeasurement(string socSecNB);
        public void SaveMeasurement();

        public bool isUserRegistered(String socSecNb, String pw);
        public bool getSocSecNB(string SocSecNB);

        public DTO_PatientData SavePatientData(int sysHigh, int sysLow, int diaHigh, int diaLow, string cprPatient);


    }
}