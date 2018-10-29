using System;
using HoustonBrowser.DOM.Interface;

namespace HoustonBrowser.DOM
{
    public class Text: CharacterData, IText
    {
        public Text(): base(TypeOfNode.ELEMENT_NODE) { }
        
        public Text splitText(long offset)
        {
            return null;
        }
    }
}