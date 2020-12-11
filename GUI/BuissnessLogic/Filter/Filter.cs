using System.Collections.Generic;
using DataAccessLogic;
using DTO;

namespace BuissnessLogic
{
    public class Filter : IFilter
    {
        IReceiveRPi recieveRPi = new ReceiveRPi();
        private List<DTO_Measurement> measurementData;

        public List<DTO_Measurement> GetMeasurementDataFilter()
        {
            //Receive measurement data
            measurementData = recieveRPi.RecieveDataPoints();

            int b = 1;

            //Keep every seventh measurement
            for (int i = 0; i < measurementData.Count; i++)
            {
                if (i / 7 == b)
                {
                    b++;
                }
                else
                {
                    measurementData.RemoveAt(i);
                }
            }

            return measurementData;
        }
    }
}