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
{
    public class ReceiveRPi : IReceiveRPi
    {
        public static void Main()
        {

        }

        public DTO_CalVal dtocal;
        
        public DTO_Measurement measurementdata;

        
        private static UdpClient listener = new UdpClient(11000);
        private static IPEndPoint groupEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 11000);
        

        ILocalDatabase local = new LocalDatabase();

        //private static Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        //private static EndPoint epFrom = new IPEndPoint(IPAddress.Any, 11000);

        


        public double ReceiveCalibration(double calval)
        {



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





        public DTO_Measurement ReceiveMeasurment(string socSecNb, double rawData, DateTime date, int sysData, int diaData,
            int alarmData, int pulse, int powerData)
        {
            
            measurementdata = new DTO_Measurement(socSecNb, rawData, date, sysData, diaData, alarmData, pulse, powerData);
            string jsonString;
            byte[] bytes;
            
          
            try
            {
                while (true)
                {
                    
                    bytes = listener.Receive(ref groupEP);
                    jsonString = Encoding.ASCII.GetString(bytes, 0, bytes.Length);
                    measurementdata = JsonSerializer.Deserialize<DTO_Measurement>(jsonString);



                    return (DTO_Measurement) local.SaveMeasurement(socSecNb, rawData, date, sysData, diaData, alarmData, pulse,
                        powerData);
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
