using Xunit;
using HoustonBrowser.HttpModule.Model;
using HoustonBrowser.HttpModule;

namespace HoustonBrowser.HttpModule.Model.Test
{
    public class HttpResponseDatagramTest
    {
        [Theory]
        [InlineData("HTTP/1.1 200 Yeah\r\nfirst: true\r\nsecond: false\r\n\r\nbody", "HTTP/1.1")]
        internal void FromString_getResponseHttpVersion_string(string datagram, string expected)
        {
            //Given
            HttpResponseDatagram hrd = new HttpResponseDatagram(HttpStatusCode.OK, "OK", HttpVersion.Get11());
            //When
            hrd.FromString(datagram);
            //Then
            Assert.Equal(hrd.Version.GetString(), expected);
        }
        [Theory]
        [InlineData("HTTP/1.1 200 Yeah\r\nfirst: true\r\nsecond: false\r\n\r\nbody", 200)]
        internal void FromString_getResponseStatusCode_ushort(string datagram, ushort expected)
        {
            //Given
            HttpResponseDatagram hrd = new HttpResponseDatagram(HttpStatusCode.OK, "OK", HttpVersion.Get11());
            //When
            hrd.FromString(datagram);
            //Then
            Assert.Equal(hrd.StatusCode, expected);
        }
        [Theory]
        [InlineData("HTTP/1.1 200 Yeah\r\nfirst: true\r\nsecond: false\r\n\r\nbody", "Yeah")]
        internal void FromString_getResponseReasonPhrase_string(string datagram, string expected)
        {
            //Given
            HttpResponseDatagram hrd = new HttpResponseDatagram(HttpStatusCode.OK, "OK", HttpVersion.Get11());
            //When
            hrd.FromString(datagram);
            //Then
            Assert.Equal(hrd.ReasonPhrase, expected);
        }
    }
}