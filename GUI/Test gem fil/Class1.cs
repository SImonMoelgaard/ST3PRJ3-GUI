using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using BuissnessLogic;
using DTO;

namespace Test_gem_fil
{
    class TEST1
    {
        private List<DTO_Measurement> dataBPressure;

        private Controller controller;

        string path = @"C:\Users\simon\source\repos\ST3PRJ3SimonOgAK\GUI\Test gem fil\bin\Mokai4";

        public void testfil()
        {
            dataBPressure = new List<DTO_Measurement>();
            controller = new Controller();
            string cpr = "123456-7890";
            dataBPressure = controller.GetMeasurement(cpr);





           
            //Skriver til fil
            using (StreamWriter sw = File.AppendText(path))
            {

                for (int i = 0; i < dataBPressure.Count; i++)
                {
                    sw.WriteLine((dataBPressure[i].RawData,dataBPressure[i].RawData));
                }
            }

        }


    }
}

