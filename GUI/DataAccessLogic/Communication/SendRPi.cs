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


        private IPAddress broadcast;
        private Socket socket;
        private Socket datasocket;
        private Socket emergencydatasocket;
        private IPEndPoint epCommand;
        private IPEndPoint epPatientdata;
        private IPEndPoint epEmergencydata;
        LocalDatabase local = new LocalDatabase();

        public void OpenSendPorts()
        {
        broadcast = IPAddress.Parse("172.20.10.11");//ÆNDRE IP HER
         //broadcast = IPAddress.Parse("127.0.0.1");//ÆNDRE IP HER


         
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
         datasocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        

         epCommand = new IPEndPoint(broadcast, 11000);
         
         epPatientdata = new IPEndPoint(broadcast, 11000);
        }


        public string Command(string command)
        {

            byte[] sendbuf = Encoding.ASCII.GetBytes(command);

            socket.SendTo(sendbuf, epCommand);

            return null;
        }

       

        public object sendemergencydata(int SysHigh, int SysLow, int DiaHigh, int DiaLow, int Meanlow, int Meanhigh, string CprPatient, double Calval, double Zeroval)
        {
            emergencydatasocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            epEmergencydata = new IPEndPoint(IPAddress.Parse("172.20.10.11"),  11000);

            
                DTO_PatientData data = new DTO_PatientData(SysLow, SysHigh, DiaLow, DiaHigh, Meanlow, Meanhigh, CprPatient, Calval, Zeroval);

                var json = JsonConvert.SerializeObject(data);
                byte[] sendbuf = Encoding.ASCII.GetBytes(json);

                emergencydatasocket.SendTo(sendbuf, epEmergencydata);


                return null;
            

            


        }

       


        public object sendpatientdata(int SysHigh, int SysLow, int DiaHigh, int DiaLow, int Meanlow, int Meanhigh, string CprPatient, double Calval, double Zeroval)
        {

            DTO_PatientData data = new DTO_PatientData(SysLow, SysHigh, DiaLow, DiaHigh, Meanlow, Meanhigh, CprPatient, Calval, Zeroval);
           

            var json = JsonConvert.SerializeObject(data);
            byte[] sendbuf = Encoding.ASCII.GetBytes(json);


            datasocket.SendTo(sendbuf,epPatientdata);



            return local.SavePatientData(SysHigh, SysLow, DiaHigh, DiaLow, Meanlow, Meanhigh, CprPatient, Calval, Zeroval); 
        } 
        
    }
}