using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using Microsoft.VisualBasic.CompilerServices;
using DTO;



namespace DataAccessLogic
{
    

    class SendRPi : ISendRPi
    {
        private const int listenPort = 11000;
        private const int listenPortCommand = 12000;
        private string startmeasurment = "Startmeasurment";
        private string startzero = "Startzeroing";
        private string startCal = "Startcalibration";
        private string MuteAlarm = "Mutealarm";
        private string stop = "Stop";


        public void Start()
        {
            
        }

        public void Stop()
        {
            throw new System.NotImplementedException();
        }

        public void StartCalibration()
        {
            throw new System.NotImplementedException();
        }

        public void StartZeroing()
        {
            throw new System.NotImplementedException();
        }

        public void MuteRPi()
        {
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            IPAddress broadcast = IPAddress.Parse("192.168.1.255");
            IPEndPoint ep = new IPEndPoint(broadcast, listenPort);
            string message;

            while (true)
            {

                message = MuteAlarm;
                byte[] sendbuf = Encoding.ASCII.GetBytes(message);


                s.SendTo(sendbuf, ep);

                
            }
        }
    }
}