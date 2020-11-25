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
        

        public object SaveMeasurement(string socSecNb, double rawData, DateTime date, int sysData, int diaData,
            int alarmData, int pulse, int powerData)
        {
            string path = @"C:\ST3PRJ3FIL\ " + socSecNb.ToString() + DateTime.Now.ToString("dd-MM-yyyy");
            DTO_Measurement measurement = new DTO_Measurement(socSecNb, rawData, date, sysData, diaData, alarmData, pulse, powerData);


            using (StreamWriter file = File.AppendText(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                serializer.Serialize(file, measurement);
                
            }

            return null;
        }
        public object SavePatientData(int SysHigh, int SysLow, int DiaHigh, int DiaLow, int Meanlow, int Meanhigh, string CprPatient)
        {
            
            DTO_PatientData patientData = new DTO_PatientData(SysHigh, SysLow, DiaLow, DiaHigh, Meanlow, Meanhigh, CprPatient);
            string path = @"C:\ST3PRJ3FIL\ " + CprPatient.ToString() +" "+ DateTime.Now.ToString("dd-MM-yyyy");

            using (StreamWriter file = File.AppendText(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                serializer.Serialize(file, patientData);

            }   

            return null;
        }

        public List<DTO_CalVal> SaveCalVal(List<int> calReference, List<double> calMeasured, double r2, double a, int b, int zv, string socSecNB)
        {
            
           


            string path = @"C:\ST3PRJ3FIL\ " + socSecNB.ToString()+ DateTime.Now.ToString("dd-MM-yyyy");
            DTO_CalVal calval = new DTO_CalVal(calReference, calMeasured, r2, a, b, zv, socSecNB);

            using (StreamWriter file = File.AppendText(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                serializer.Serialize(file, calval);

            }


            return new List<DTO_CalVal>();
        }

        
    }
}