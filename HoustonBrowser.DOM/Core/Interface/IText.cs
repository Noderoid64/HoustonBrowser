using System;

namespace HoustonBrowser.DOM.Interface
{
    public interface IText: ICharacterData
    {
        Text SplitText(int offset);
    }
}