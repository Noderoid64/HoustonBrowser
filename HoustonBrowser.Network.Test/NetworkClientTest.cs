using Xunit;
using System.Threading;
using System.Threading.Tasks;
using System;

using HoustonBrowser.HttpModule;
namespace HoustonBrowser.Test.HttpModule
{
    public class NetworkClientTest
    {
        [Fact]
        public void Get_returnResponse_string()
        {
            //Given
            INetworkClient client = new NetworkClient();

            //When
            string result = client.Get("http://msdn.microsoft.com/en-us/");

            //Then
            Assert.NotEmpty(result);
        }
        [Fact]
        public void GetTask_returnTask_string()
        {
            //Given
            INetworkClient client = new NetworkClient();
            //When
            string result = client.GetTask("http://msdn.microsoft.com/en-us/").Result;
            //Then
            Assert.NotEmpty(result);
        }
        [Theory]
        [InlineData("http://msdn.microsoft.com/en-us/")]
        public void GetTask_returnTaskWithToken_string(string given)
        {
            //Given
            INetworkClient client = new NetworkClient();
            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;
            //When
            Task<string> t = client.GetTask(given, token);
            string real = t.Result;

            //Then
            Assert.NotEmpty(real);
        }

        [Theory]
        [InlineData("http://msdn.microsoft.com/en-us/")]
        public void GetTask_returnTaskWithToken_AggregateException(string given)
        {
            //Given
            INetworkClient client = new NetworkClient();
            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;
            //When
            Action act = () =>
            {
                Task<string> t = client.GetTask(given, token);
                cts.Cancel();
                string real = t.Result;
            };
            //Then
            Assert.Throws(typeof(AggregateException), act);
        }
    }
}