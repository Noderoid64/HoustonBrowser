using System;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Sockets;
using System.Net;


namespace HoustonBrowser.HttpModule.Senders
{
    internal class HttpSender : ISender
    {
        private const int port = 80;
        TcpClient client;

        public string Send(string host, string message)
        {
            try
            {

                client = new TcpClient(GetIp(host), port);

                NetworkStream stream = client.GetStream();

                byte[] data = Encoding.GetEncoding("ISO-8859-1").GetBytes(message);
                stream.Write(data, 0, data.Length);

                data = new byte[4096]; // буфер для получаемых данных
                StringBuilder builder = new StringBuilder();
                int bytes = 0;
                do
                {
                    bytes = stream.Read(data, 0, data.Length);
                    builder.Append(Encoding.GetEncoding("ISO-8859-1").GetString(data, 0, bytes));
                }
                while (stream.DataAvailable);

                message = builder.ToString();
                return message;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                client?.Close();

            }
            return null;
        }

        public string GetIp(string hostname)
        {
            if(hostname.StartsWith("http://"))
            hostname = hostname.Replace("http://","");
            IPAddress hostEntry = (Dns.GetHostAddresses(hostname))[0];
            return hostEntry.ToString();
        }
    }
}