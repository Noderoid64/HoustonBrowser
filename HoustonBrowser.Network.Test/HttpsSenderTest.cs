using Xunit;
using System;
using HoustonBrowser.HttpModule.Senders;

namespace HoustonBrowser.HttpModule.Test
{
    public class HttpsSenderTest
    {
        [Theory]
        [InlineData("habr.com", "178.248.237.68")]
        public void GetIpByHostname(string hostName, string expected)
        {
             ISender sender = new HttpsSender();

             string real = sender.GetIp(hostName);

              Assert.Equal(real, expected);
        }
    }
}