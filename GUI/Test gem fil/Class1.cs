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


                string path = @"C:\ST3PRJ3FIL\ " + getSocSecNB(ToString()) + DateTime.Now.ToString("dd-MM-yyyy");

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
                DTO_Measurement measurement = new DTO_Measurement(socSecNb, rawData, date, sysData, diaData, alarmData, pulse, powerData);

                File.WriteAllText(@"c:\movie.json", JsonConvert.SerializeObject(measurement));

                File.WriteAllText("path", JsonConvert.SerializeObject(measurement));

            }
        


    }
}

