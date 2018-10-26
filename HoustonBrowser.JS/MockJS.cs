using System;

namespace HoustonBrowser.JS
{
    class MockJS: IJS
    {
        void Process(string rawJS)
        {
            System.Console.WriteLine("JS Works");
        }
    }
}