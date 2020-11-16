using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using BuissnessLogic;
using DataAccessLogic;
using DTO;
using Newtonsoft.Json;

namespace Test_gem_fil
{
    class TEST1
    {
        private List<DTO_Measurement> dataBPressure;
        private List<DTO_CalVal> dataCalVal;
        private List<DTO_PatientData> dataPatientData;
        private LocalDatabase localDatabase;
        private Controller controller;



        public void testfil(string path)
        {
            dataBPressure = new List<DTO_Measurement>();
            controller = new Controller();
            string cpr = "123456-7890";
            dataBPressure = controller.GetMeasurement(cpr);

            dataBPressure = new List<DTO_Measurement>();
            dataCalVal = new List<DTO_CalVal>();
            localDatabase = new LocalDatabase();
            dataPatientData = new List<DTO_PatientData>();




            //Skriver til fil
            using (StreamWriter sw = File.AppendText(path))
            {

                sw.WriteLine(("CPR", "RAW", "DIA", "SYS"));
                for (int i = 0; i < dataBPressure.Count; i++)
                {
                    sw.WriteLine((cpr, dataBPressure[i].RawData, dataBPressure[i].SysData, dataBPressure[i].DiaData));

                }
            }

        }

        public void CreateFile()
            {
                dataBPressure = new List<DTO_Measurement>();
                dataCalVal = new List<DTO_CalVal>();
                localDatabase = new LocalDatabase();


                string path = @"C:\ST3PRJ3FIL\ "  + DateTime.Now.ToString("dd-MM-yyyy");

                using (StreamWriter sw = File.AppendText(path))
                {

                    //sw.WriteLine(("CPR", "RAW", "DIA", "SYS"));
                    // for (int i = 0; i < DTO_Pat; i++)
                    {
                        // sw.WriteLine((dataPatientData.));

                    }
                }

            }

            public void SaveMeasurement(int syslow, int syshigh, int dialow, int diahigh, string socSecNb)
            {
                string path = @"C:\ST3PRJ3FIL\haj" + DateTime.Now.ToString("dd-MM-yyyy");
            


                
            
            //DTO_Measurement measurement = new DTO_Measurement(socSecNb, rawData, date, sysData, diaData, alarmData, pulse, powerData);
            DTO_PatientData data = new DTO_PatientData(syslow, syshigh, dialow, diahigh, socSecNb);
            //File.WriteAllText(@"c:\movie.json", JsonConvert.SerializeObject(data));


            
            using (StreamWriter file = File.AppendText(path))
            {
                JsonSerializer serializer = new JsonSerializer() ;
                serializer.Formatting = Formatting.Indented;
                serializer.Serialize(file, data);
                
            }
            //File.WriteAllText(path, JsonConvert.SerializeObject(data)+ Environment.NewLine);
            
        }
        


    }
}

