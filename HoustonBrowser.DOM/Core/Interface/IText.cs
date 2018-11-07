using System;

namespace HoustonBrowser.DOM.Core.Interface
{
    public interface IText: ICharacterData
    {
        Text SplitText(int offset);
    }
}