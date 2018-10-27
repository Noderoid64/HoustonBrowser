using System;

namespace HoustonBrowser.JS
{
    public class MockJS : IJS
    {
        public string Process(string rawJS)
        {
            return "JS Works";
        }
    }
}