using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DTO;

namespace DataAccessLogic
{
    public class LocalDatabase : IDatabase
    {
        private string[] patientMeasurement = {"rawData", "date", "sysData", "diaData", "alarmData", "pulse", "socSecNB"};
        private string[] calVal = {"calReference", "calMeasured", "r2","a", "b", "zv","socSecNB"};
        private string[] patientData = {"syslow", "syshigh", "dialow", "diahigh", "socSecNB"};

        public List<DTO_Measurement> GetMeasurement(string socSecNB)
        {
            throw new System.NotImplementedException();
        }

        public void SaveMeasurement()
        {
            System.IO.File.WriteAllLines(@"C:\Users\aneka\source\repos\ST3PRJ3-GUI\GUI\DataAccessLogic\bin\Measurement.txt", patientMeasurement);


        }
        public void SavePatientData()
        {
            throw new System.NotImplementedException();
            System.IO.File.WriteAllLines(@"C:\Users\aneka\source\repos\ST3PRJ3-GUI\GUI\DataAccessLogic\bin\Patientdata.txt", patientData);

        }

        public void SaveCalVal()
        {
            throw new System.NotImplementedException();
            System.IO.File.WriteAllLines(@"C:\Users\aneka\source\repos\ST3PRJ3-GUI\GUI\DataAccessLogic\bin\Calval.txt", calVal);

        }

        public bool isUserRegistered(string socSecNb, string pw)
        {
            throw new System.NotImplementedException();
        }

        public bool getSocSecNB(string SocSecNB)
        {
            throw new System.NotImplementedException();
        }
    }
}