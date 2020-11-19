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

        //https://docs.microsoft.com/en-us/dotnet/framework/network-programming/using-udp-services

        private static Socket socket;
        private const int listenPort = 11000;
        private const int listenPortCommand = 12000;
        public DTO_Measurement mdata;


        UdpClient listener = new UdpClient(listenPort);
        IPEndPoint groupEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 11000);

        ILocalDatabase local = new LocalDatabase();

        public object ReceiveCalibration(int calReference, double  calMeasured, double r2, double a, int b, int zv, string socSecNB)
        {
            
            DTO_CalVal caldata;
            string jsonString;
            byte[] bytes;

            try
            {
                while (true)
                {
                    
                    bytes = listener.Receive(ref groupEP);
                    jsonString = Encoding.ASCII.GetString(bytes, 0, bytes.Length);
                    caldata = JsonSerializer.Deserialize<DTO_CalVal>(jsonString);


                    return local.SaveCalVal(calReference, calMeasured, r2, a, b, zv, socSecNB);
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

            return null;
        }

        



        public DTO_Measurement ReceiveMeasurment()
        {
            
            DTO_Measurement mdata;
            string jsonString;
            byte[] bytes;
            
          
            try
            {
                while (true)
                {
                    
                    bytes = listener.Receive(ref groupEP);
                    jsonString = Encoding.ASCII.GetString(bytes, 0, bytes.Length);
                    mdata = JsonSerializer.Deserialize<DTO_Measurement>(jsonString);



                    return mdata;
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
