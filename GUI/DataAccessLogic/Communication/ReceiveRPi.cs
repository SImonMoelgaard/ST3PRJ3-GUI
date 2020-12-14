using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading;
using DTO;
using DataAccessLogic;


using System.Threading.Tasks;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace DataAccessLogic
{/// <summary>
/// /
/// </summary>
    public class ReceiveRPi : IReceiveRPi
    {
        ILocalDatabase local = new LocalDatabase();
        private List<DTO_Measurement> measurements = new List<DTO_Measurement>();
        private UdpClient listener;
        private UdpClient ListenerDouble;
        private IPEndPoint groupEP;
        private IPEndPoint groupEPDouble;

        /// <summary>
        /// Opretter UDPclient og destination for de to mest brugte modtager metoder.
        /// </summary>
        public void OpenRecievePorts()
        {
            listener =new UdpClient(11001);
            groupEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 11001);

            groupEPDouble = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 11004);
            ListenerDouble = new UdpClient(11004);
        }

       /// <summary>
       /// Lytter 1000 gange i sekundet om der kommer data ind. Kommer der data ind, returneres en liste til præstationslaget. Her modtages i bytes, det bliver decoded med ASCII og herefter deserialiseret.
       /// </summary>
       /// <returns>
       /// Liste bliver retuneret.
       /// </returns>
        public List<DTO_Measurement> RecieveDataPoints()
        {
            string data;
            byte[] bytes;
            measurements = new List<DTO_Measurement>();
            
            
            while (true)
            {
                //Thread.Sleep(1);
                try
                {
                    bytes = listener.Receive(ref groupEP);
                    data = Encoding.ASCII.GetString(bytes, 0, bytes.Length);

                    //var measurementdata = new DTO_Measurement("", 0, new DateTime(2000, 01, 01), false, false, false, false, false, false, 0, 0, 0, 0, 0);

                   // measurementdata = JsonConvert.DeserializeObject<DTO_Measurement>(data);
                    var measurementdata = JsonConvert.DeserializeObject<List<DTO_Measurement>>(data);
                    
                   
                    
                    
                        local.SaveMeasurement(measurementdata[0].SocSecNB, measurementdata[0].mmHg, measurementdata[0].Tid, measurementdata[0].HighSys,
                            measurementdata[0].LowSys, measurementdata[0].HighDia, measurementdata[0].LowDia,
                            measurementdata[0].HighMean, measurementdata[0].LowMean, measurementdata[0].CalculatedSys,
                            measurementdata[0].CalculatedDia, measurementdata[0].CalculatedMean, measurementdata[0].CalculatedPulse,
                            measurementdata[0].Batterystatus);
                        return measurementdata;

                    
                }
                catch (InvalidOperationException)
                {
                    return null;
                }

            }

        }

        /// <summary>
        /// Modtager data, decoder det med ASCII 
        /// </summary>
        /// <returns>
        /// Returnerer en double tilbage. Denne metode kan bruges til at returnere alle doubles.
        /// </returns>
        public double RecieveCalculatedValues()
        {
            double data;
            byte[] bytes;

            try
            {
                while (true)
                {
                    bytes = ListenerDouble.Receive(ref groupEPDouble);
                    data = Convert.ToDouble(Encoding.ASCII.GetString(bytes, 0, bytes.Length));
                    return data;
                }
            }
            catch (SocketException e)
            {
                return 0;

            }
        }
    }
}
