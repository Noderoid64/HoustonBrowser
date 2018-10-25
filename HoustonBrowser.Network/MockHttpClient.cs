namespace HoustonBrowser.HttpModule{
    public class MockHttpClient : IHttpClient
    {
        public string SendGet(string host)
        {
            return "HttpModule is working";
        }
    }
}