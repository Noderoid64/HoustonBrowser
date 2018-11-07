using System;
using System.Collections.Generic;
using System.Text;

namespace HoustonBrowser.Parsing.Interfaces
{
    public interface ILexAnalyser<T>
    {
         T Tokenize(string value);
    }
}
