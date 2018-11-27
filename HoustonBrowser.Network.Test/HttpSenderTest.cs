using Xunit;
using System;
using HoustonBrowser.HttpModule.Senders;

namespace HoustonBrowser.Test.HttpModule
{
    public class HttpSenderTest
    {
        [Theory]
        [InlineData("http://www.netside.net", "206.156.132.2")]
        [InlineData("msdn.microsoft.com", "104.111.245.160")]
        public void GetIpByHostname(string hostName, string expected)
        {
             ISender sender = new HttpSender();

             string real = sender.GetIp(hostName);

             Assert.Equal(real, expected);
        }
    }
}