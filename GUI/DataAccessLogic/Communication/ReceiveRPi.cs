using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using DTO;

namespace DataAccessLogic
{
    public class ReceiveRPi : IReceiveRPi
    {
        public static void Main()
        {

        }

        //https://docs.microsoft.com/en-us/dotnet/framework/network-programming/using-udp-services

        private const int listenPort = 11000;
        private const int listenPortCommand = 12000;


        public void ReceiveCalibration()
        {
            UdpClient listener = new UdpClient(listenPortCommand);
            IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, listenPort);
            DTO_CalVal rxCommand;
            string jsonString;
            byte[] bytes;

            try
            {
                while (true)
                {
                    //Console.WriteLine("Waiting for broadcast of a Command");
                    bytes = listener.Receive(ref groupEP);
                    jsonString = Encoding.ASCII.GetString(bytes, 0, bytes.Length);
                    rxCommand = JsonSerializer.Deserialize<DTO_CalVal>(jsonString);

                    //  Console.WriteLine($"Received broadcast command from {groupEP} :");
                    Console.WriteLine(rxCommand);
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                listener.Close();
            }

        }

        



        public DTO_Measurement ReceiveMeasurment()
        {
            UdpClient listener = new UdpClient(listenPortCommand);
            IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, listenPort);
            DTO_Measurement rxCommand;
            string jsonString;
            byte[] bytes;
            
          
            try
            {
                while (true)
                {
                    //Console.WriteLine("Waiting for broadcast of a Command");
                    bytes = listener.Receive(ref groupEP);
                    jsonString = Encoding.ASCII.GetString(bytes, 0, bytes.Length);
                    rxCommand = JsonSerializer.Deserialize<DTO_Measurement>(jsonString);

                    //  Console.WriteLine($"Received broadcast command from {groupEP} :");

                    return rxCommand;
                }
                
            }
            catch (SocketException e)
            {
                return null;
                Console.WriteLine(e);
            }
            finally
            {
                
                listener.Close();
            }

            
        }
    }
        
    
}
