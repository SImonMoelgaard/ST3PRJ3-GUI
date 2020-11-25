using System;
using System.Collections.Generic;
using DTO;

namespace DataAccessLogic
{
    public class ReadFromFile
    {
        private List<DTO_Measurement> samples;
        public List<DTO_Measurement> Read()
        {
            samples = new List<DTO_Measurement>();

            string[] lines = System.IO.File.ReadAllLines(@"..\..\..\..\DataAccessLogic\Samples.txt");

            foreach (string line in lines)
            {
                string[] splitline = line.Split(',');

                    string tid = splitline[0];
                    DateTime dateTime =
                        DateTime.ParseExact(tid, "s.fff", System.Globalization.CultureInfo.InvariantCulture);

                    DTO_Measurement measurement = new DTO_Measurement("", Convert.ToDouble(splitline[1])/1000, dateTime,
                        false, false, false, false, false, false, 0, 0, 0, 0, 0);

                    samples.Add(measurement);
            }

            return samples;
        }
    }
}
