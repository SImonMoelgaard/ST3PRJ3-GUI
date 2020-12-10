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
        private static State state = new State();
        private static EndPoint epFrom = new IPEndPoint(IPAddress.Any, 0);
        private const int bufSize = 300 * 1024;
   
        static Socket RevieveMeasurementsocket;
        private static Socket RevieveDoublesocket;
        private Socket s;
        private static AsyncCallback recv = null;
        private List<DTO_Measurement> measurements = new List<DTO_Measurement>();
        private List<DTO_Measurement> Fivemeasurement=new List<DTO_Measurement>();
        //private static Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        //private static EndPoint epFrom = new IPEndPoint(IPAddress.Any, 11000);
        private UdpClient listener;
        private IPEndPoint groupEP;
        public void OpenRecievePorts()
        {

           RevieveMeasurementsocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            RevieveMeasurementsocket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.ReuseAddress, true);
            RevieveMeasurementsocket.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 11002));
          //  RevieveMeasurementsocket.Bind(new IPEndPoint(IPAddress.Parse("192.168.87.122"), 11002));
          //RevieveDoublesocket.Bind(new  IPEndPoint(IPAddress.Parse("127.0.0.1"),11002));

            listener =new UdpClient(11001);
            //groupEP=new IPEndPoint(IPAddress.Parse("127.0.0.1"),11001);

            groupEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 11001);
        }

       
        public List<DTO_Measurement> test()
        {
            string data;
            //UdpClient listener = new UdpClient(11001);
           // IPEndPoint groupEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 11001);
            measurements = new List<DTO_Measurement>();
            



           //var measurementdata = new DTO_Measurement("", 0, new DateTime(2000, 01, 01), false, false,
                //false, false, false, false, 0, 0, 0, 0, 0);
            byte[] bytes;


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
            UdpClient listener = new UdpClient(11004);
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



        
        
       
        
        public List<DTO_Measurement> ReceiveMeasurment()
        {
            //UdpClient listener = new UdpClient(11001);
            IPEndPoint groupEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 11001);
            //IPEndPoint groupEP = new IPEndPoint(IPAddress.Parse("172.20.10.7"), 11001);//AK
            //IPEndPoint groupEP = new IPEndPoint(IPAddress.Parse("172.20.10.5"), 11001);//RPI
            string jsonString;
            byte[] bytes;

         
            //listener.Client.ReceiveBufferSize = 2*1024;
            //bytes = listener.Receive(ref groupEP);
            try
            {


                
                
                    RevieveMeasurementsocket.BeginReceiveFrom(state.buffer, 0, bufSize, SocketFlags.None, ref epFrom, recv =
                        (ar) =>

                        {

                            measurements = new List<DTO_Measurement>();
                            int bytes = new int();
                            bytes.ToString("");

                            var measurementdata = new DTO_Measurement("", 0, new DateTime(2000, 01, 01), false, false,
                                false, false, false, false, 0, 0, 0, 0, 0);
                            State so = (State)ar.AsyncState;

                            bytes = RevieveMeasurementsocket.EndReceiveFrom(ar, ref epFrom);
                            jsonString = Encoding.ASCII.GetString(so.buffer, 0, bytes);
                            

                                Thread.Sleep(1);
                                //socket.SendBufferSize = 1000;
                                //so.buffer.SetValue(512,0);

                                measurementdata = JsonConvert.DeserializeObject<DTO_Measurement>(jsonString);
                                Thread.Sleep(4);
                                bytes.ToString("");
                                measurements.Add(measurementdata);
                               // Socket.BeginReceiveFrom(so.buffer, 0, bufSize, SocketFlags.None, ref epFrom, recv, so);


                               


                        }, state);
                    return measurements;


            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine("");
            }

            return null;

        }

        public class State
        {
            public byte[] buffer = new byte[ReceiveRPi.bufSize];
            
           
        }
        

        
        

       


    }
        
    
}
