using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
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

        public DTO_CalVal dtocal;

        private int port;

        
        
        

        ILocalDatabase local = new LocalDatabase();

        //private static Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        //private static EndPoint epFrom = new IPEndPoint(IPAddress.Any, 11000);

        


        


        public double Recievedouble()
        {
            double data;
            port = 11004;
         UdpClient listener = new UdpClient(port);
         IPEndPoint groupEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 11004);


        byte[] bytes;

            try
            {
                while (true)
                {

                    bytes = listener.Receive(ref groupEP);
                    data = Convert.ToDouble(Encoding.ASCII.GetString(bytes, 0, bytes.Length));


                    listener.Close();
                    
                    
                    
                }
            }
            catch (SocketException e)
            {
                return 0;
            }
            finally
            {

                listener.Close();

            }


        }




       
        public List<DTO_Measurement> ReceiveMeasurment()
        {
            UdpClient listener = new UdpClient(11001);
            IPEndPoint groupEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 11001);
            
            
            string jsonString;
            byte[] bytes;
            

            try
            {
                while (true)
                {
                    List<DTO_Measurement> measurements = new List<DTO_Measurement>();
                    var measurementdata = new DTO_Measurement("", 0, DateTime.Now, false, false, false, false, false, false, 0 , 0 ,0 ,0 ,0);
                    bytes = listener.Receive(ref groupEP);

                    jsonString = Encoding.ASCII.GetString(bytes, 0, bytes.Length);

                    measurementdata = JsonConvert.DeserializeObject<DTO_Measurement>(jsonString);

                    measurements.Add(measurementdata);


                

                    local.SaveMeasurement(measurementdata.SocSecNB, measurementdata.mmHg, measurementdata.Tid,
                        measurementdata.HighSys, measurementdata.LowSys, measurementdata.HighDia,
                        measurementdata.LowDia, measurementdata.HighMean, measurementdata.LowMean,
                        measurementdata.CalculatedSys, measurementdata.CalculatedDia, measurementdata.CalculatedMean,
                        measurementdata.CalculatedPulse, measurementdata.Batterystatus);
                    return measurements;
                }
                
            }
            catch (SocketException e)
            {
                return null;
                
            }
            finally
            {
                
                listener.Close();
            }

            
        }

        
        
    }
        
    
}
