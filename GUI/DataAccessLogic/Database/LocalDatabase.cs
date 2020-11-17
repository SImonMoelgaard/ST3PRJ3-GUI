using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using DTO;
using Newtonsoft.Json;

namespace DataAccessLogic
{
    public class LocalDatabase : ILocalDatabase
    {
        private List<DTO_Measurement> dataBPressure;
        private List<DTO_CalVal> dataCalVal;
        private List<DTO_PatientData> dataPatientData;
        private LocalDatabase localDatabase;
        
        
        

        public void CreateFile()
        {
            dataBPressure = new List<DTO_Measurement>();
            dataCalVal = new List<DTO_CalVal>();
            localDatabase = new LocalDatabase();


            string path = @"C:\ST3PRJ3FIL\ " + getSocSecNB(ToString()) +" "+ DateTime.Now.ToString("dd-MM-yyyy");

            using (StreamWriter sw = File.AppendText(path))
            {

                //sw.WriteLine(("CPR", "RAW", "DIA", "SYS"));
               // for (int i = 0; i < DTO_Pat; i++)
                {
                   // sw.WriteLine((dataPatientData.));

                }
            }

        }

        public void SaveMeasurement(string socSecNb, double rawData, DateTime date, int sysData, int diaData,
            int alarmData, int pulse, int powerData)
        {
            string path = @"C:\ST3PRJ3FIL\ " + getSocSecNB(ToString()) + DateTime.Now.ToString("dd-MM-yyyy");
            DTO_Measurement measurement = new DTO_Measurement(socSecNb, rawData, date, sysData, diaData, alarmData, pulse, powerData);


            using (StreamWriter file = File.AppendText(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                serializer.Serialize(file, measurement);

            }

        }
        public void SavePatientData(int sysHigh,int sysLow,int diaHigh, int diaLow, string cprPatient)
        {
            DTO_PatientData patientData = new DTO_PatientData(sysHigh, sysLow, diaLow, diaHigh, cprPatient);
            string path = @"C:\ST3PRJ3FIL\ " + getSocSecNB(ToString()) + DateTime.Now.ToString("dd-MM-yyyy");

            using (StreamWriter file = File.AppendText(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                serializer.Serialize(file, patientData);

            }

        }

        public void SaveCalVal(List<int> calReference, List<double> calMeasured, double r2, double a, int b, int zv, string socSecNB)
        {
            string path = @"C:\ST3PRJ3FIL\ " + getSocSecNB(ToString()) + DateTime.Now.ToString("dd-MM-yyyy");
            DTO_CalVal calval = new DTO_CalVal(calReference, calMeasured, r2, a, b, zv,socSecNB);

            using (StreamWriter file = File.AppendText(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                serializer.Serialize(file, calval);

            }



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