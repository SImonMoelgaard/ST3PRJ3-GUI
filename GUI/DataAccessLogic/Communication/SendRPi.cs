using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using Microsoft.VisualBasic.CompilerServices;
using DTO;
using Newtonsoft.Json;
using ST3Prj3DomaineCore.Models.DTO;
using JsonSerializer = System.Text.Json.JsonSerializer;


namespace DataAccessLogic
{
    

    public class SendRPi : ISendRPi
    {
        


        private const int listenPort = 11000;
        private const int listenPortCommand = 12000;
        private string startmeasurment = "Startmeasurment";
        private string startzero = "Startzeroing";
        private string startCal = "Startcalibration";
        private string MuteAlarm = "Mutealarm";
        private string stop = "Stop";
        public DTO_CalVal dtocalval;
        public DTO_PatientData patientdata;
        IPAddress broadcast = IPAddress.Parse("127.0.0.1");//ÆNDRE IP HER

        private static Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        private static EndPoint epFrom = new IPEndPoint(IPAddress.Any, 0);

        public void Start()
        {

            {
                Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);


                IPEndPoint ep = new IPEndPoint(broadcast, listenPort);


                while (true)
                {

                    byte[] sendbuf = Encoding.ASCII.GetBytes(startmeasurment);


                    s.SendTo(sendbuf, ep);

                }
            }
        }

        public void Stop()
        {
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            
            IPEndPoint ep = new IPEndPoint(broadcast, listenPort);
            

            while (true)
            {

                byte[] sendbuf = Encoding.ASCII.GetBytes(stop);


                s.SendTo(sendbuf, ep);

            }
        }

        public void StartCalibration()
        {
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            
            IPEndPoint ep = new IPEndPoint(broadcast, listenPort);
          

            while (true)
            {

                
                byte[] sendbuf = Encoding.ASCII.GetBytes(startCal);


                s.SendTo(sendbuf, ep);

            }
        }

        public void StartZeroing()
        {
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

           
            IPEndPoint ep = new IPEndPoint(broadcast, listenPort);
            

            while (true)
            {

                
                byte[] sendbuf = Encoding.ASCII.GetBytes(startzero);


                s.SendTo(sendbuf, ep);

            }
        }

        public void MuteRPi()
        {
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

           
            IPEndPoint ep = new IPEndPoint(broadcast, listenPort);
            

            while (true)
            {

                
                byte[] sendbuf = Encoding.ASCII.GetBytes(MuteAlarm);


                s.SendTo(sendbuf, ep);

            }



        }

        public void sendA()
        {
           
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            
            IPEndPoint ep = new IPEndPoint(broadcast, listenPort);
            string message;
            

            while (true)
            {
                
                message = dtocalval.A.ToString();
                
                byte[] data = Encoding.ASCII.GetBytes(message);

                

                //s.SendTo(message, ep);

            }


        }

        public bool sendpatientdata(int SysHigh, int SysLow, int DiaHigh, int DiaLow, string CprPatient)
        {
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPEndPoint ep = new IPEndPoint(broadcast, listenPort);

            DTO_PatientData data = new DTO_PatientData(SysHigh, SysLow, DiaHigh, DiaLow, CprPatient);
           

            var json = JsonConvert.SerializeObject(data);

            
            


                byte[] sendbuf = Encoding.ASCII.GetBytes(json);


                s.SendTo(sendbuf, ep);

            


            return true;
        } 
        
    }
}