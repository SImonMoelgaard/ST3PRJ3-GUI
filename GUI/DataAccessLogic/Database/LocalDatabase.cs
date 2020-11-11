using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using DTO;

namespace DataAccessLogic
{
    public class LocalDatabase : IDatabase
    {
        private List<DTO_Measurement> dataBPressure;
        private List<DTO_CalVal> dataCalVal;
        private List<DTO_PatientData> dataPatientData;
        private LocalDatabase localDatabase;



        public List<DTO_Measurement> GetMeasurement(string socSecNB)
        {
            return dataBPressure;
        }

        public void CreateFile()
        {
            dataBPressure = new List<DTO_Measurement>();
            dataCalVal = new List<DTO_CalVal>();
            localDatabase = new LocalDatabase();


            string path = @"C:\ST3PRJ3FIL\ " + DateTime.Now.ToString("dd-MM-yyyy");

            using (StreamWriter sw = File.AppendText(path))
            {

                sw.WriteLine(("CPR", "RAW", "DIA", "SYS"));
               // for (int i = 0; i < DTO_Pat; i++)
                {
                   // sw.WriteLine((dataPatientData.));

                }
            }

        }

        public void SaveMeasurement()
        {


            


        }
        public void SavePatientData(int sysHigh,int sysLow,int diaHigh, int diaLow, string cprPatient)
        {
            DTO_PatientData patientData = new DTO_PatientData(sysHigh, sysLow, diaLow, diaHigh, cprPatient);

            
            
        }

        public void SaveCalVal()
        {
            throw new System.NotImplementedException();
            

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