namespace HoustonBrowser.HttpModule{
    public class MockHttpClient : IHttpClient
    {
        public string GetCss(string host)
        {
            return "Css";
        }

        public string GetHtml(string host)
        {
            return "Html";
        }

        public string GetStatus()
        {
            return "HttpModule is working";
        }
    }
}