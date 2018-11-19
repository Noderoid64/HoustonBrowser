namespace HoustonBrowser.HttpModule{
    public class MockNetworkClient : INetworkClient
    {
        public string Get(string host)
        {
            return @"<html>
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

        public string GetStatus()
        {
            return "HttpModule is working";
        }
    }
}