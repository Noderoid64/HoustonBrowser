using Xunit;
using System.Text;
using HoustonBrowser.HttpModule.Middleware;
using HoustonBrowser.HttpModule.Model;
using HoustonBrowser.HttpModule.Model.Headers;

namespace HoustonBrowser.HttpModule.Model.Test
{
    public class ContentTypeLayerTest
    {
        [Theory]
        [InlineData(new byte [2] {194, 165}, "¥")] //194 165 is simbol ¥ in utf-8
        public void Handle_ChangeEncoding(byte[] given, string expected)
        {
            //Given
            HttpResponseDatagram datagram = new HttpResponseDatagram(HttpStatusCode.OK, null, null);
            datagram.header.AddHeaderField(new HeaderFieldContentType("Content-Type: text/html; charset=utf-8"));
            datagram.body.FromString(Encoding.GetEncoding("ISO-8859-1").GetString(given,0, given.Length));
            MiddlewareLayer layer = new ContentTypeLayer();
            //When
            layer.Handle(datagram);
            //Then
            Assert.Equal(datagram.body.GetString(), expected);
        }
    }
}