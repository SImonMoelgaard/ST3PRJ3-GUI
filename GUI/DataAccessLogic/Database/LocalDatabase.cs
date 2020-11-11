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
        
        

        public List<DTO_Measurement> GetMeasurement(string socSecNB)
        {
            return dataBPressure;
        }

        public void CreateFile()
        {
            dataBPressure = new List<DTO_Measurement>();
            dataCalVal = new List<DTO_CalVal>();
            dataPatientData=new List<DTO_PatientData>();

            
            string path = @"C:\ST3PRJ3FIL\ " + DateTime.Now.ToString("dd-MM-yyyy");

            for (int i = 0; i < dataPatientData.Count; i++)
            {
                //sw.Writeline((dataPatientData[i].SocSecNB));
            }

            using (FileStream fs = File.Create(path));

        }

        public void SaveMeasurement()
        {


            


        }
        public DTO_PatientData SavePatientData(int sysHigh,int sysLow,int diaHigh, int diaLow, string cprPatient)
        {
            
            return patientData;

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