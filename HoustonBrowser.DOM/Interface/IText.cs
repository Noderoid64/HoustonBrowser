using System;

namespace HoustonBrowser.DOM.Interface
{
    public interface IText: ICharacterData
    {
        Text splitText(long offset);
    }
}