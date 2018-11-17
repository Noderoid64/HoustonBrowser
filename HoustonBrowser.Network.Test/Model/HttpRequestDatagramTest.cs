using Xunit;
using HoustonBrowser.HttpModule.Model;
using HoustonBrowser.HttpModule;

namespace HoustonBrowser.HttpModule.Model.Test
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
        [Fact]
        internal void FromString()
        {
            HttpRequestDatagram hrd = new HttpRequestDatagram(HttpMethods.CONNECT, null, null);
            
            hrd.FromString("start\r\nfirst\r\nsecond\r\n\r\nbody");

            Assert.True(false);
        }
    }
}