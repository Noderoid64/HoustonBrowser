using Xunit;
namespace HoustonBrowser.HttpModule.Test{
    public class NetworkClientTest{
        [Fact]
        public void Get_returnResponse_string()
        {
        //Given
        INetworkClient client = new NetworkClient();
        
        //When
        string result = client.Get("https://msdn.microsoft.com/en-us/");
        
        //Then
        Assert.NotEmpty(result);
        }
    }
}