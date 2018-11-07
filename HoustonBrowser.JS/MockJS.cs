using System;

namespace HoustonBrowser.JS
{
    public class MockJS : IJS
    {
        public event EventHandler<string> onAlert;

        public string Process(string rawJS)
        {
            int index = rawJS.IndexOf("alert(");
            if (index != -1)
            {
                index += 6;
                int old = index;
                while (rawJS[index] != ')') index++;
                string cache = rawJS.Substring(old, index - old);
                onAlert?.Invoke(this, cache);
            }

            return "JS Works";
        }
    }
}