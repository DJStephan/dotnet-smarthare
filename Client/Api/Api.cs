using System;
using System.Runtime.ConstrainedExecution;
using Client.Dtos;
using System.IO;
using System.Net.Sockets;
using System.Xml.Serialization;
using System.Net;

namespace Client.Api
{
    public class Api
    {
        private static IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 3000);
        private const int PORT = 3000;


        private Api() { }

        private static ReturnDto SendRequest(FileDto file)
        {
            TcpClient client = new TcpClient();
            client.Connect(endPoint);
            XmlSerializer serializer = new XmlSerializer(typeof(FileDto));
            XmlSerializer reader = new XmlSerializer(typeof(ReturnDto));
            ReturnDto returnDto;

            using (NetworkStream stream = new NetworkStream(client.Client))
            {
                serializer.Serialize(stream, file);
                client.Client.Shutdown(SocketShutdown.Send);
                returnDto = (ReturnDto)reader.Deserialize(stream);
            }

            return returnDto;
        }

        /// <summary>
        /// Send download request
        /// </summary>
        /// <param name="">TODO</param>
        /// <returns>true if request was successful and false if unsuccessful</returns>
        public static ReturnDto Download(string filename, string password)
        {
            FileDto file = new FileDto(filename, password);
            ReturnDto returnDto = SendRequest(file);
            return returnDto;
        }

        /// <summary>
        /// Send upload request
        /// </summary>
        /// <param name="">TODO</param>
        /// <returns>true if request was successful and false if unsuccessful</returns>
        public static ReturnDto Upload(string fullName, string password, double expiration, int maxDownloads)
        {
            FileDto file = new FileDto(fullName, password, expiration, maxDownloads);
            ReturnDto returnDto = SendRequest(file);
            return returnDto;
        }
    }
}