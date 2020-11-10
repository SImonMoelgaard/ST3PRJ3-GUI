using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using DTO;

namespace Testprogram
{
    class Program
    {
        private List<DTO_Measurement> dto;
        static void Main(string[] args)
        {
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            IPAddress broadcast = IPAddress.Parse("192.168.1.255");

            

            int i = 0;

            while (i<10)
            {
                byte[] sendbuf = Encoding.ASCII.GetBytes(args[0]);
                IPEndPoint ep = new IPEndPoint(broadcast, 11000);

                s.SendTo(sendbuf, ep);

                Console.WriteLine("Message sent to the broadcast address");
                dto
                i++;
            }

            

                
            
        }
    }
}
