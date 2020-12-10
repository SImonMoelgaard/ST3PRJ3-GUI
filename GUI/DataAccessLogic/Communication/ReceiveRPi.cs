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
        public static void Main()
        {

        }


        ILocalDatabase local = new LocalDatabase();
        private List<DTO_Measurement> measurements = new List<DTO_Measurement>();
       
        private UdpClient listener;
        private UdpClient ListenerDouble;
        private IPEndPoint groupEP;
        private IPEndPoint groupEPDouble;
        public void OpenRecievePorts()
        {

            listener =new UdpClient(11001);
            groupEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 11001);

            groupEPDouble = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 11004);
            ListenerDouble = new UdpClient(11004);
        }

       
        public List<DTO_Measurement> test()
        {
            string data;
            byte[] bytes;
            measurements = new List<DTO_Measurement>();
            
            
            while (true)
            {
                Thread.Sleep(1);
                try
                {
                    bytes = listener.Receive(ref groupEP);
                    data = Encoding.ASCII.GetString(bytes, 0, bytes.Length);

                    var measurementdata = new DTO_Measurement("", 0, new DateTime(2000, 01, 01), false, false, false, false, false, false, 0, 0, 0, 0, 0);

                    measurementdata = JsonConvert.DeserializeObject<DTO_Measurement>(data);

                    measurements.Add(measurementdata);


                    if (measurementdata.mmHg>0 || measurementdata.CalculatedDia>0)
                    {
                        local.SaveMeasurement(measurementdata.SocSecNB, measurementdata.mmHg, measurementdata.Tid, measurementdata.HighSys,
                            measurementdata.LowSys, measurementdata.HighDia, measurementdata.LowDia,
                            measurementdata.HighMean, measurementdata.LowMean, measurementdata.CalculatedSys,
                            measurementdata.CalculatedDia, measurementdata.CalculatedMean, measurementdata.CalculatedPulse,
                            measurementdata.Batterystatus);
                        return measurements;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (InvalidOperationException)
                {
                    return null;
                }

            }

        }

        


        
        public double Recievedouble()
        {
            double data;
            byte[] bytes;

            try
            {
                while (true)
                {
                    bytes = ListenerDouble.Receive(ref groupEPDouble);
                    data = Convert.ToDouble(Encoding.ASCII.GetString(bytes, 0, bytes.Length));
                }
            }
            catch (SocketException e)
            {
                return 0;

            }
        }



        
        
       
        
        

        
        

       


    }
        
    
}
