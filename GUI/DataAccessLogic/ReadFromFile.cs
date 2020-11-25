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

            string[] lines = System.IO.File.ReadAllLines(@"..\..\..\Sample.txt");

            foreach (string line in lines)
            {
                string[] splitline = line.Split(',');

                for (int i = 0; i < lines.Length; i++)
                {
                    samples[i].Tid = Convert.ToDateTime(splitline[0]);
                    samples[i].mmHg = Convert.ToDouble(splitline[1]);
                }
            }

            return samples;
        }
    }
}
