using System.Threading.Tasks;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("HoustonBrowser.Network.Test")]


namespace HoustonBrowser.HttpModule
{
    public interface INetworkClient
    {
        string GetStatus();
        string Get(string host);
        Task GetTask(string host);
        

    }
}