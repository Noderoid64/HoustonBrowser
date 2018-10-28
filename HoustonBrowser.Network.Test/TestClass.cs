using Xunit;
using System;

using HoustonBrowser.HttpModule.Parsers;
using HoustonBrowser.HttpModule.Builders;
using HoustonBrowser.HttpModule.Model;

namespace HoustonBrowser.HttpModule.Test
{
    public class TestClass
    {
<<<<<<< HEAD
        //http://www.netside.net/boba/webmasters.html
        //192.168.0.110
        //houstonbrowsertest.ddns.net
=======
>>>>>>> [Update] add general test
        [Fact]
        public void TestVoid(){
            IHttpClient client = new HttpClient();

<<<<<<< HEAD
            string real = client.GetHtml("http://www.netside.net/boba/webmasters.html");
=======
            string real = client.GET("http://www.netside.net/boba/webmasters.html");
>>>>>>> [Update] add general test

            Assert.NotEmpty(real);
        }
    }
}