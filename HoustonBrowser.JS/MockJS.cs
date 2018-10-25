using System;

namespace HoustonBrowser.JS
{
    interface MockJS: IJS
    {
        void Process(string rawJS)
        {
            System.Console.WriteLine("JS Works");
        }

    }
}