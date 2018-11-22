using System;
using HoustonBrowser.DOM.Core;
using System.Collections.Generic;
using HoustonBrowser.Parsing.Enums;
using HoustonBrowser.DOM.HTML;

namespace HoustonBrowser.Parsing
{
    public class State:IState
    {
        
        protected delegate void TagProcessing();
        

        public State() {}
        protected void AddingStructureTag(string name)//For all processing tags except html,meta,script,link,img,hr
        {
            var item = new Element(name);
            StatesData.openedTags.Peek().AppendChild(item);
            StatesData.openedTags.Push(item);
        }

        public void ProcessToken(Token token)
        {
            switch (StatesData.currentState)
            {
                case (int)InsertionModes.Initial:
                    {
                        StatesData.initialState.ProcessToken(token);
                        break;
                    }
                case (int)InsertionModes.InHead:
                    {
                        StatesData.inHead.ProcessToken(token);
                        break;
                    }
                case (int)InsertionModes.InBody:
                    {
                        StatesData.inBody.ProcessToken(token);
                        break;
                    }
            }
        }
        
    }
}