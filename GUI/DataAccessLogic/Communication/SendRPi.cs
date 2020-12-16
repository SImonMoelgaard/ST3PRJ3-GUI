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
        
        /// <summary>
        /// OpenSendPorts åbner de to mest brugte ports og sætter IP på raspberry pi. Denne opretter også de sockets, som dataen vil blive sendt over.
        /// </summary>
        public void OpenSendPorts()
        {
           // broadcast = IPAddress.Parse("172.20.10.7");//Marie RPi IP
            broadcast = IPAddress.Parse("172.20.10.5");//Annesofie RPi IP

            //broadcast = IPAddress.Parse("127.0.0.1");//ÆNDRE IP HER



            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
         datasocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);



            epCommand = new IPEndPoint(broadcast, 11000);
            epPatientdata = new IPEndPoint(broadcast, 11004);
        }

        /// <summary>
        /// Modtager en string fra buisness logic. Her bliver denne encoded med ASCII og sendt til Raspberry Pi.
        /// </summary>
        /// <param name="command">
        /// String der modtages fra buissnes logic.
        /// </param>
        /// <returns></returns>
        public string Command(string command)
        {


            byte[] sendbuf = Encoding.ASCII.GetBytes(command);

            socket.SendTo(sendbuf, epCommand);

            return null;
        }

        /// <summary>
        /// Denne metode sender data fra en målingfil videre til RPi'en. Dette inkluderer grænseværdier, Calval og Zeroval. bliver filen hentet fra databasen og sendt til RPi.
        /// </summary>
        /// <param name="SysHigh"></param>
        /// <param name="SysLow"></param>
        /// <param name="DiaHigh"></param>
        /// <param name="DiaLow"></param>
        /// <param name="Meanlow"></param>
        /// <param name="Meanhigh"></param>
        /// <param name="CprPatient"></param>
        /// <param name="Calval"></param>
        /// <param name="Zeroval"></param>
        /// <returns></returns>
        public object sendemergencydata(int SysHigh, int SysLow, int DiaHigh, int DiaLow, int Meanlow, int Meanhigh, string CprPatient, double Calval, double Zeroval)
        {
            emergencydatasocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            epEmergencydata = new IPEndPoint(IPAddress.Parse("172.20.10.7"),  11000);

            
                DTO_PatientData data = new DTO_PatientData(SysLow, SysHigh, DiaLow, DiaHigh, Meanlow, Meanhigh, CprPatient, Calval, Zeroval);

                var json = JsonConvert.SerializeObject(data);
                byte[] sendbuf = Encoding.ASCII.GetBytes(json);

                emergencydatasocket.SendTo(sendbuf, epEmergencydata);


                return null;
            

            


        }

        /// <summary>
        /// Her sendes patientdataen til Raspberry Pi'en og gemmer disse data til databasen.
        /// </summary>
        /// <param name="SysHigh"></param>
        /// <param name="SysLow"></param>
        /// <param name="DiaHigh"></param>
        /// <param name="DiaLow"></param>
        /// <param name="Meanlow"></param>
        /// <param name="Meanhigh"></param>
        /// <param name="CprPatient"></param>
        /// <param name="Calval"></param>
        /// <param name="Zeroval"></param>
        /// <returns></returns>
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