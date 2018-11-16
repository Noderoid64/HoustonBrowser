namespace HoustonBrowser.HttpModule.Senders
{
    internal interface ISender
    {
        string Send(string host, string message);
        string GetIp(string host);
    }
}