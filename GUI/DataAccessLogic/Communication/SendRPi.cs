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

using JsonSerializer = System.Text.Json.JsonSerializer;
using DataAccessLogic;


namespace DataAccessLogic
{
    

    public class SendRPi : ISendRPi
    {
        


        private const int listenPort = 11000;
       
        private string startmeasurment = "Startmeasurment";
        private string startzero = "Startzeroing";
        private string startCal = "Startcalibration";
        private string MuteAlarm = "Mutealarm";
        private string stop = "Stop";
        public DTO_CalVal dtocalval;
        public DTO_PatientData patientdata;
        IPAddress broadcast = IPAddress.Parse("127.0.0.1");//ÆNDRE IP HER
        LocalDatabase local = new LocalDatabase();
        IReceiveRPi recieve = new ReceiveRPi();
       

        public void Start()
        {


            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);


            IPEndPoint ep = new IPEndPoint(broadcast, listenPort);


                while (true)
                {

                    byte[] sendbuf = Encoding.ASCII.GetBytes(startmeasurment);


                    s.SendTo(sendbuf, ep);

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

        public string StartCalibration()
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

        


        public object sendpatientdata(int SysHigh, int SysLow, int DiaHigh, int DiaLow, int Meanlow, int Meanhigh, string CprPatient,
            double Calval, double Zeroval)
        {
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPEndPoint ep = new IPEndPoint(broadcast, listenPort);

            DTO_PatientData data = new DTO_PatientData(SysLow, SysHigh, DiaLow, DiaHigh, Meanlow, Meanhigh, CprPatient, Calval, Zeroval);
           

            var json = JsonConvert.SerializeObject(data);

            

                byte[] sendbuf = Encoding.ASCII.GetBytes(json);


                s.SendTo(sendbuf,ep);




            return local.SavePatientData(SysLow, SysHigh, DiaHigh, DiaLow, Meanlow, Meanhigh, CprPatient, Calval, Zeroval); 
        } 
        
    }
}