using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("HoustonBrowser.Network.Test")]

namespace HoustonBrowser.HttpModule{
    public interface IHttpClient
    {
        string GetStatus();
        string GetHtml(string host);
        string GetCss(string host);

    }
}