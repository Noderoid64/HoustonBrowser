using Xunit;
using HoustonBrowser.HttpModule;
using HoustonBrowser.HttpModule.Model;

namespace HoustonBrowser.HttpModule.Test{
    public class HeaderFieldHostTest{
        [Fact]
        public void GetString_showString_HostString()
        {
            string TestData = "DefaultHost";
            HttpHeaderField host = new HeaderFieldHost(TestData);

            Assert.Equal(host.GetString(), "Host: " + TestData);
        }
        
    }
}