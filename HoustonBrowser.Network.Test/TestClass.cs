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
<<<<<<< HEAD
        //http://www.netside.net/boba/webmasters.html
        //192.168.0.110
        //houstonbrowsertest.ddns.net
=======
>>>>>>> [Update] add general test
=======
        //http://www.netside.net/boba/webmasters.html
        //192.168.0.110
        //houstonbrowsertest.ddns.net
>>>>>>> [Fix] httpPageLoader
        [Fact]
        public void TestVoid(){
           // IHttpClient client = new HttpsClient();

<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
            string real = client.GetHtml("http://www.netside.net/boba/webmasters.html");
=======
            string real = client.GET("http://www.netside.net/boba/webmasters.html");
>>>>>>> [Update] add general test
=======
            string real = client.GetHtml("http://www.netside.net/boba/webmasters.html");
>>>>>>> [Fix] httpPageLoader
=======
            string real = client.GetHtml("127.0.0.1");
>>>>>>> Packet size fix
=======
            string real = client.GetHtml("127.0.0.1");
>>>>>>> Packet size fix
=======
            string real = client.GetHtml("http://www.tim.org/");
>>>>>>> [update] add https
=======
         //   string real = client.GetHtml("http://www.tim.org/");
>>>>>>> [Refactor] fix dns and add client

         //   Assert.NotEmpty(real);
        }
    }
}