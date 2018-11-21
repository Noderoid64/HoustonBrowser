using HoustonBrowser.HttpModule.Model;
using HoustonBrowser.HttpModule.Model.Headers;
using HoustonBrowser.HttpModule.Senders;
using HoustonBrowser.HttpModule.Builders;
using System.Text;
using System;

namespace HoustonBrowser.HttpModule.Middleware
{

    internal class LocationLayer : MiddlewareLayer
    {
        public override HttpResponseDatagram Handle(HttpResponseDatagram datagram)
        {
            if (datagram.StatusCode == HttpStatusCode.MovedPermanently)
            {
                if (datagram.header.fields.Find(x => x.name == HeaderFieldLocation.FieldName) is HeaderFieldLocation fieldLocation)
                {
                    string host = fieldLocation.value;

                    NetworkClient client = new NetworkClient();
                    return client.GetDatagram(host);
                }
                else
                    throw new Exception("Location field not found");
            }
            else
            {
                return datagram;
            }

        }
    }
}