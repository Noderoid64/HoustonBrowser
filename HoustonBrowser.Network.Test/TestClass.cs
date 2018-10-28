using Xunit;
using System;

using HoustonBrowser.HttpModule.Parsers;
using HoustonBrowser.HttpModule.Builders;
using HoustonBrowser.HttpModule.Model;

namespace HoustonBrowser.HttpModule.Test
{
    public class TestClass
    {
        [Fact]
        public void TestVoid(){
            IHttpClient client = new HttpClient();

            string real = client.GET("http://www.netside.net/boba/webmasters.html");

            Assert.NotEmpty(real);
        }
    }
}