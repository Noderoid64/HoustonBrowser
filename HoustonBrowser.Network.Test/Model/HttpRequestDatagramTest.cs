using Xunit;
using HoustonBrowser.HttpModule.Model;
using HoustonBrowser.HttpModule;

namespace HoustonBrowser.Test.HttpModule.Model
{
    public class HttpRequestDatagramTest
    {
        [Theory]
        [InlineData(HttpMethods.GET, "/api/login", "GET /api/login HTTP/1.1\r\n\r\n")]
        [InlineData(HttpMethods.POST, "/", "POST / HTTP/1.1\r\n\r\n")]
        [InlineData(HttpMethods.PUT, "/login/create", "PUT /login/create HTTP/1.1\r\n\r\n")]
        internal void GetString_getStartString_string(HttpMethods method, string url, string expected)
        {
            HttpDatagram datagram = new HttpRequestDatagram(method, url, HttpVersion.Get11());
            string real = datagram.GetString();
            Assert.Equal(real, expected);
        }
        [Theory]
        [InlineData("GET /home HTTP/1.1\r\nfirstHeaderField: true\r\nsecondHeader: false\r\n\r\nbody", HttpMethods.GET)]
        internal void FromString_getRequestMethod_HttpMethod(string datagram, HttpMethods expected)
        {
            //Given
            HttpRequestDatagram hrd = new HttpRequestDatagram(HttpMethods.CONNECT, null, null);
            //When
            hrd.FromString(datagram);
            //Then
            Assert.Equal(hrd.Method, expected);
        }

        [Theory]
        [InlineData("GET /home HTTP/1.1\r\nfirstHeaderField: true\r\nsecondHeader: false\r\n\r\nbody", "/home")]
        internal void FromString_getRequestUrl_stringUrl(string datagram, string expected)
        {
            //Given
            HttpRequestDatagram hrd = new HttpRequestDatagram(HttpMethods.CONNECT, null, null);
            //When
            hrd.FromString(datagram);
            //Then
            Assert.Equal(hrd.Url, expected);
        }

        [Theory]
        [InlineData("GET /home HTTP/1.1\r\nfirstHeaderField: true\r\nsecondHeader: false\r\n\r\nbody", "HTTP/1.1")]
        internal void FromString_getRequestHttpVersion_string(string datagram, string expected)
        {
            //Given
            HttpRequestDatagram hrd = new HttpRequestDatagram(HttpMethods.CONNECT, null, null);
            //When
            hrd.FromString(datagram);
            //Then
            Assert.Equal(hrd.Version.GetString(), expected);
        }
    }
}