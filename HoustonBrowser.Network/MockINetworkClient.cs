namespace HoustonBrowser.HttpModule{
    public class MockNetworkClient : INetworkClient
    {
        public string Get(string host)
        {
            return "Html";
        }

        public string GetStatus()
        {
            return "HttpModule is working";
        }
    }
}