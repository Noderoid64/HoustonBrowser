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
                try
                {
                    index += 6;
                    while (rawJS[index] != '"') index++;
                    int old = ++index;
                    while (rawJS[index] != '"') index++;
                    string cache = rawJS.Substring(old, index - old);
                    while (rawJS[index] != ')') index++;
                    onAlert?.Invoke(this, cache);
                }
                catch (Exception)
                {
                    return "";
                }

            }

            return "JS Works";
        }
    }
}