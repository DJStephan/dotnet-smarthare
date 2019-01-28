using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Xml.Serialization;
using Client.Dtos;
using CsharpAssessmentSmartShare;
using Microsoft.EntityFrameworkCore;
using Server.DAO;

namespace Server.ClientHandler
{
    public class ClientHandler
    {
        private ClientHandler() { }

        public static void HandleIt(TcpClient client)
        {
            Console.WriteLine("Connected to a client");
            ReturnDto returnDto = new ReturnDto(false, "Something went really wrong");
            using (NetworkStream stream = client.GetStream())
            {
                Console.WriteLine("1");
                XmlSerializer reader = new XmlSerializer(typeof(FileDto));
                Console.WriteLine("2");

                FileDto file = (FileDto)reader.Deserialize(stream);
                Console.WriteLine("3");

                using (SmartShareContext db = new SmartShareContext())
                {
                    Console.WriteLine("4");

                    Dao dao = new Dao(db, file);
                    Console.WriteLine("5");


                    if (file.Data != null)
                    {
                        Console.WriteLine("6");

                        returnDto = dao.Upload();
                    }
                    else
                    {
                        //returnDto = dao.Download();
                    }

                }

                XmlSerializer serializer = new XmlSerializer(typeof(ReturnDto));
                serializer.Serialize(stream, returnDto);
                client.Client.Shutdown(SocketShutdown.Both);
            }
        }
    }
}
