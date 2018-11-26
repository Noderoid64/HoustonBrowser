using HoustonBrowser.DOM;
using System;

namespace HoustonBrowser.Parsing
{
    public interface IState
    {
        void ProcessToken(Token token);
    }
}
