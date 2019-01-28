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
                XmlSerializer reader = new XmlSerializer(typeof(FileDto));
                FileDto file = (FileDto)reader.Deserialize(stream);

                using (SmartShareContext db = new SmartShareContext())
                {
                    Dao dao = new Dao(db, file);

                    if (file.Data != null)
                    {
                        returnDto = dao.Upload();
                    }
                    else if(file.View)
                    {
                        returnDto = dao.View();
                    }
                    else
                    {
                        returnDto = dao.Download();
                    }
                }

                XmlSerializer serializer = new XmlSerializer(typeof(ReturnDto));
                serializer.Serialize(stream, returnDto);
                client.Client.Shutdown(SocketShutdown.Both);
            }
        }
    }
}
