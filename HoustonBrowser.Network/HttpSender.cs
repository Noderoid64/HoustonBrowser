using System;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Sockets;


namespace HoustonBrowser.HttpModule
{
    internal class HttpSender
    {
        private const int port = 80;
        TcpClient client;

       public string SendHttp(string host, string message)
        {
            try
            {
                client = new TcpClient(host, port);
                NetworkStream stream = client.GetStream();

                byte[] data = Encoding.Unicode.GetBytes(message);
                stream.Write(data, 0, data.Length);

                data = new byte[1024]; // буфер для получаемых данных
                StringBuilder builder = new StringBuilder();
                int bytes = 0;
                do
                {
                    bytes = stream.Read(data, 0, data.Length);
                    builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
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
                client.Close();

            }
            return null;
        }
    }
}