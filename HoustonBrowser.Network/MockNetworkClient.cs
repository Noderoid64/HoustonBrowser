using System.Threading;
using System.Threading.Tasks;

namespace HoustonBrowser.HttpModule
{
    public class MockNetworkClient : INetworkClient
    {
        string mockValue = @"<html>
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
        public string Get(string host)
        {
            return mockValue;
        }

        public string GetStatus()
        {
            return "HttpModule is working";
        }

        public Task<string> GetTask(string host)
        {
            Task<string> a = new Task<string>(() => mockValue);
            a.Start();
            return a;
        }

        public Task<string> GetTask(string host, CancellationToken token)
        {
            Task<string> a = new Task<string>(() =>
            {
                Thread.Sleep(1000); // эмуляция долгой работы
                token.ThrowIfCancellationRequested(); // выбросит исключение AggregateException(OperationCanceledException), что говорит о не завершении задания
                return mockValue;
            }, token);
            a.Start();
            return a;


        }
    }
}