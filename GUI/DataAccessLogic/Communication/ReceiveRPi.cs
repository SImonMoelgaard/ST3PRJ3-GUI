using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
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
         IPEndPoint groupEP = new IPEndPoint(IPAddress.Parse("172.20.10.7"), 11004);

         //IPEndPoint groupEP = new IPEndPoint(IPAddress.Parse("172.20.10.5"), 11004);

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



        private static State state = new State();
        private static EndPoint epFrom = new IPEndPoint(IPAddress.Any, 0);
        public const int bufSize = 8 * 1024;
        static Socket socket;
        private static AsyncCallback recv = null;
        private List<DTO_Measurement> measurements = new List<DTO_Measurement>();
        
        public void openrecieveports()
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.ReuseAddress, true);
            socket.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 11001));
            
        }
        
        public List<DTO_Measurement> ReceiveMeasurment()
        {
            //UdpClient listener = new UdpClient(11001);
            //IPEndPoint groupEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 11001);
            //IPEndPoint groupEP = new IPEndPoint(IPAddress.Parse("172.20.10.7"), 11001);//AK
            //IPEndPoint groupEP = new IPEndPoint(IPAddress.Parse("172.20.10.5"), 11001);//RPI

            string jsonString;
            byte[] bytes;
            
            
            
            
                
                   //listener.Client.ReceiveBufferSize = 2*1024;
                   
                    
            //bytes = listener.Receive(ref groupEP);

            
                socket.BeginReceiveFrom(state.buffer, 0, bufSize, SocketFlags.None, ref epFrom, recv = (ar) =>
                {
                    
                    var measurementdata = new DTO_Measurement("", 0, new DateTime(2000,01,01), false, false, false, false, false, false, 0 , 0 ,0 ,0 ,0);
                    State so = (State)ar.AsyncState;
                    int bytes = socket.EndReceiveFrom(ar, ref epFrom);
                    jsonString = Encoding.ASCII.GetString(so.buffer, 0, bytes);
                    measurementdata = JsonConvert.DeserializeObject<DTO_Measurement>(jsonString);
                    measurements.Add(measurementdata);
                    socket.BeginReceiveFrom(so.buffer, 0, bufSize, SocketFlags.None, ref epFrom, recv, so);


                    
                }, state);
            
            
            



            return measurements;













        }

        public class State
        {
            public byte[] buffer = new byte[ReceiveRPi.bufSize];
        }



    }
        
    
}
