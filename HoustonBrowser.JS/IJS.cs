using System;

namespace HoustonBrowser.JS
{
    public interface IJS
    {
        string Process(string rawJS);
        event EventHandler<string> onAlert;
    }
}