using System;

namespace HoustonBrowser.JS
{
    public class MockJS: IJS
    {
        void Process(string rawJS)
        {
            System.Console.WriteLine("JS Works");
        }
    }
}