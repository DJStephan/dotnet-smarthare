using Server.Models;
using System;
using System.Net;
using System.Net.Sockets;
using System.Xml.Serialization;
using System.Threading;
using Client.Dtos;
using CsharpAssessmentSmartShare;
using Server.DAO;
//using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            var port = 3000;
            var localAddr = IPAddress.Parse("127.0.0.1");
            TcpListener server = new TcpListener(localAddr, port);

            server.Start();

            while (true)
            {
                Console.WriteLine("Waiting for connection...");
                try
                {
                    TcpClient client = server.AcceptTcpClient();
                    new Thread(() => ClientHandler.ClientHandler.HandleIt(client)).Start();
                }
                catch(SocketException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        public static void HandleIt(TcpClient client)
        {

        }
    }
}