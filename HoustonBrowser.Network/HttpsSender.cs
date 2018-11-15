using System;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.Net.Security;


namespace HoustonBrowser.HttpModule
{
    internal class HttpsSender
    {
        private const int port = 443;
        TcpClient client;

        public string SendHttp(string host, string message)
        {
            try
            {
                client = new TcpClient(getIpByHostname(host), port);
                using (SslStream sslStream = new SslStream(client.GetStream()))
                {
                    sslStream.AuthenticateAsClient(host);
                    byte[] data = Encoding.GetEncoding("ISO-8859-1").GetBytes(message);
                    sslStream.Write(data, 0, data.Length);

                    data = new byte[8192]; // буфер для получаемых данных
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    do
                    {
                        bytes = sslStream.Read(data, 0, data.Length);
                        builder.Append(Encoding.GetEncoding("ISO-8859-1").GetString(data, 0, bytes));
                    }
                    while (bytes == 0);

                    message = builder.ToString();
                    return message;
                }




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

        public string getIpByHostname(string hostname)
        {
            if (hostname.StartsWith("https://"))
                hostname = hostname.Replace("https://", "");
            IPAddress hostEntry = (Dns.GetHostAddresses(hostname))[0];
            return hostEntry.ToString();
        }
    }
}