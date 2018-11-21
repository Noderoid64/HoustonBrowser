using HoustonBrowser.HttpModule.Senders;
using HoustonBrowser.HttpModule.Model;
using HoustonBrowser.HttpModule.Model.Headers;
using HoustonBrowser.HttpModule.Builders;
using System.Text;
using System;

namespace HoustonBrowser.HttpModule
{
    public class NetworkClient : INetworkClient
    {
        public string Get(string host)
        {
            ISender sender;
            if (host.StartsWith("https"))
                sender = new HttpsSender();
            else
                sender = new HttpSender();


            HttpRequestDatagram datagram = HttpDatagramBuilder.GetRequestDatagram(host); // Получаем базовую модель датаграммы (без алгоритмов сжатия)

            string response = sender.Send(UrlBuilder.GetHost(host), datagram.GetString());
            HttpResponseDatagram dat = new HttpResponseDatagram(response);

            string coding = "ISO-8859-1";
            Encoding encoderIn = Encoding.GetEncoding(coding);
            if (dat.header.fields.Find(x => x.name == HeaderFieldContentType.FieldName) is HeaderFieldContentType a)
            {
                for (int i = 0; i < a.values.Length; i++)
                {
                    if (a.values[i].StartsWith("charset="))
                    {
                        coding = a.values[i].Substring("charset=".Length);
                        break;
                    }
                }
            }
            Encoding encoderOut = Encoding.GetEncoding(coding);
            byte[] data = encoderIn.GetBytes(dat.body.GetString());
            //byte [] codingData = Encoding.Convert(encoderIn,encoderOut,data);

            Console.WriteLine(data.Length);
            Console.WriteLine(encoderOut.GetString(data, 0, data.Length).Length);

            return encoderOut.GetString(data, 0, data.Length);
        }

        public string GetStatus()
        {
            return "HttpModule is working";
        }
    }
}