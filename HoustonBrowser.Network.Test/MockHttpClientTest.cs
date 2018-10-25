using Xunit;
using HoustonBrowser.HttpModule;

namespace HoustonBrowser.HttpModule.Test{
    public class MockHttpClientTest{
        [Fact]
        public void SendGet_showString_HttpModuleIsWorking()
        {
            IHttpClient httpClient = new MockHttpClient();

        
        Assert.Equal(httpClient.SendGet(""), "HttpModule is working");

        }
    }
}