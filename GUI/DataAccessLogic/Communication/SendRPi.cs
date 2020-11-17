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

        public void Start()
        {
            
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPAddress broadcast = IPAddress.Parse("192.168.1.255");
            IPEndPoint ep = new IPEndPoint(broadcast, listenPortCommand);
            DTO_Send sendCommand = new DTO_Send() { sendID = "Laptop", ComNo = 1 };
            byte[] jsonUtf8Bytes;
            JsonSerializerOptions sendopt = new JsonSerializerOptions() { WriteIndented = true };
            

            while (true)
            {
                JsonConvert.SerializeObject(patientdata);   
                // sendCommand.ComNo = IntegerType.FromObject(com);
                jsonUtf8Bytes = JsonSerializer.SerializeToUtf8Bytes(sendCommand, sendopt);
                s.SendTo(jsonUtf8Bytes, ep);

                
            }
        }

        public void Stop()
        {
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            IPAddress broadcast = IPAddress.Parse("192.168.1.255");
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

            IPAddress broadcast = IPAddress.Parse("192.168.1.255");
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

            IPAddress broadcast = IPAddress.Parse("192.168.1.255");
            IPEndPoint ep = new IPEndPoint(broadcast, listenPort);
            string message;

            while (true)
            {

                message = startzero;
                byte[] sendbuf = Encoding.ASCII.GetBytes(message);


                s.SendTo(sendbuf, ep);

            }
        }

        public void MuteRPi()
        {
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            IPAddress broadcast = IPAddress.Parse("192.168.1.255");
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

            IPAddress broadcast = IPAddress.Parse("192.168.1.255");
            IPEndPoint ep = new IPEndPoint(broadcast, listenPort);
            string message;

            while (true)
            {
                
                message = dtocalval.A.ToString();
                byte[] sendbuf = Encoding.ASCII.GetBytes("zero" + message);


                s.SendTo(sendbuf, ep);

            }
        }
    }
}