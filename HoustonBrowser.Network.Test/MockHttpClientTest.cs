using Xunit;
using HoustonBrowser.HttpModule;
using System.Threading.Tasks;
using System.Threading;
using System;
namespace HoustonBrowser.Test.HttpModule
{
    public class MockHttpClientTest
    {

        string mockValue;
        public MockHttpClientTest()
        {
            mockValue = @"<html>
<head>
HoustonBrowser
</head>
<body>
<script>alert('Ok')</script>

<button onclick='myFunction()'>Click Me</button>

<div id='myDIV'>
  This is my DIV element.
</div>
</body>
</html>";
        }

        [Fact]
        public void GET_showString_HttpModuleIsWorking()
        {
            //Given
            INetworkClient httpClient = new MockNetworkClient();
            string host = "any";
            string expected = mockValue;
            //When
            string real = httpClient.Get(host);
            //Then
            Assert.Equal(real, mockValue);
        }
        [Theory]
        [InlineData()]
        public void GetTask_getTask_String()
        {
            //Given
            INetworkClient client = new MockNetworkClient();
            string host = "any";
            string expected = mockValue;
            //When
            Task<string> task = client.GetTask(host);
            string real = task.Result;
            //Then
            Assert.Equal(real, expected);
        }

        [Theory]
        [InlineData()]
        public void GetTask_getTaskWithToken_String()
        {
            //Given
            INetworkClient client = new MockNetworkClient();
            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;
            string host = "any";
            string expected = mockValue;
            //When
            Task<string> task = client.GetTask(host, token);
            string real = task.Result;
            //Then
            Assert.Equal(real, expected);
        }

        [Theory]
        [InlineData()]
        public void GetTask_getTaskWithToken_Exception()
        {
            //Given
            INetworkClient client = new MockNetworkClient();
            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;
            string host = "any";
            string expected = mockValue;
            //When
            Action act = () =>
            {
                Task<string> task = client.GetTask(host, token);
                cts.Cancel();
                string real = task.Result;
            };

            //Then
            Assert.Throws(typeof(AggregateException),act);
        }

    }
}