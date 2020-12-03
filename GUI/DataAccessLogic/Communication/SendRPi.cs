using System;
using System.Collections.Generic;
using System.IO;
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


        
        private static IPAddress broadcast = IPAddress.Parse("127.0.0.1");//ÆNDRE IP HER
        LocalDatabase local = new LocalDatabase();
        


        public string Command(string command)
        {
            int listenPort = 11000;

            Socket a = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);


            IPEndPoint ep = new IPEndPoint(broadcast, listenPort);

            
                
            byte[] sendbuf = Encoding.ASCII.GetBytes(command);


            a.SendTo(sendbuf, ep);


            return null;

        }

       

        public object sendemergencydata(int SysHigh, int SysLow, int DiaHigh, int DiaLow, int Meanlow, int Meanhigh, string CprPatient,
            double Calval, double Zeroval)
        {


            





            int PatiendataPort = 11002;
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPEndPoint ep = new IPEndPoint(broadcast, PatiendataPort);


            DTO_PatientData data = new DTO_PatientData(SysLow, SysHigh, DiaLow, DiaHigh, Meanlow, Meanhigh, CprPatient, Calval, Zeroval);

            
            
                var json = JsonConvert.SerializeObject(data);



                byte[] sendbuf = Encoding.ASCII.GetBytes(json);


                s.SendTo(sendbuf, ep);
            
            

            return false;
        }

       


        public object sendpatientdata(int SysHigh, int SysLow, int DiaHigh, int DiaLow, int Meanlow, int Meanhigh, string CprPatient,
            double Calval, double Zeroval)
        {
         int PatiendataPort = 11003;
        Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPEndPoint ep = new IPEndPoint(broadcast, PatiendataPort);

            DTO_PatientData data = new DTO_PatientData(SysLow, SysHigh, DiaLow, DiaHigh, Meanlow, Meanhigh, CprPatient, Calval, Zeroval);
           

            var json = JsonConvert.SerializeObject(data);

            

                byte[] sendbuf = Encoding.ASCII.GetBytes(json);


                s.SendTo(sendbuf,ep);




            return local.SavePatientData(SysHigh, SysLow, DiaHigh, DiaLow, Meanlow, Meanhigh, CprPatient, Calval, Zeroval); 
        } 
        
    }
}