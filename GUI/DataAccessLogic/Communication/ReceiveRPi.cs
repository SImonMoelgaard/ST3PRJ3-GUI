using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using DTO;
using DataAccessLogic;


using System.Threading.Tasks;

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
        
        public DTO_Measurement measurementdata;

        
        
        

        ILocalDatabase local = new LocalDatabase();

        //private static Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        //private static EndPoint epFrom = new IPEndPoint(IPAddress.Any, 11000);

        


        public double ReceiveCalibration(double calval)
        {
         UdpClient listener = new UdpClient(11004);
         IPEndPoint groupEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 11004);


        byte[] bytes;

            try
            {
                while (true)
                {

                    bytes = listener.Receive(ref groupEP);
                    calval = Convert.ToDouble(Encoding.ASCII.GetString(bytes, 0, bytes.Length));
                    //caldata = JsonSerializer.Deserialize<DTO_CalVal>(jsonString);




                    return calval;
                }
            }
            catch (SocketException e)
            {
                return calval;
            }
            finally
            {

                listener.Close();

            }


        }




        //string socSecNb, double mmhg, DateTime tid, bool highSys, bool lowSys, bool highDia, bool lowDia, bool highMean, bool lowMean, int sys, int dia, int mean, int pulse, int batterystatus
        public List<DTO_Measurement> ReceiveMeasurment()
        {
            UdpClient listener = new UdpClient(11001);
            IPEndPoint groupEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 11001);
            List<DTO_Measurement> measurements = new List<DTO_Measurement>();
            
            string jsonString;
            byte[] bytes;
            
            
          
            try
            {
                while (true)
                {
                    
                    bytes = listener.Receive(ref groupEP);
                    jsonString = Encoding.ASCII.GetString(bytes, 0, bytes.Length);
                    measurementdata = JsonSerializer.Deserialize<DTO_Measurement>(jsonString);
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
