using Xunit;
using System;

namespace HoustonBrowser.HttpModule.Test
{
    public class HttpSenderTest
    {
        [Theory]
        [InlineData("http://www.netside.net", "206.156.132.2")]
        [InlineData("msdn.microsoft.com", "104.111.245.160")]
        public void GetIpByHostname(string hostName, string expected)
        {
            HttpSender sender = new HttpSender();

            string real = sender.getIpByHostname(hostName);

            Assert.Equal(real, expected);
        }
    }
}