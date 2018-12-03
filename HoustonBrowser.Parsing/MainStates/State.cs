using System;
using HoustonBrowser.DOM;
using System.Collections.Generic;
using HoustonBrowser.Parsing.Enums;

namespace HoustonBrowser.Parsing
{
    public class State:IState
    {
        
        protected delegate void TagProcessing();


        public State() {}
        protected void AddingStructureTag(string name)//For all processing tags except html,meta,script,link,img,hr
        {
            var item = new Element(name);
            StatesData.OpenedTags.Peek().AppendChild(item);
            StatesData.OpenedTags.Push(item);
        }

        public void ProcessToken(Token token)
        {
            switch (StatesData.currentState)
            {
                case (int)InsertionModes.Initial:
                    {
                        StatesData.InitialState.ProcessToken(token);
                        break;
                    }
                case (int)InsertionModes.InHead:
                    {
                        StatesData.InHead.ProcessToken(token);
                        break;
                    }
                case (int)InsertionModes.InBody:
                    {
                        StatesData.InBody.ProcessToken(token);
                        break;
                    }
            }
        }
        
    }
}