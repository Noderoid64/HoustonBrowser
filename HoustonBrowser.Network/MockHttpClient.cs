namespace HoustonBrowser.HttpModule{
    public class MockHttpClient : IHttpClient
    {
        public string GET(string host)
        {
            return "HttpModule is working";
        }
    }
}