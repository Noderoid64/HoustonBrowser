using System;
using HoustonBrowser.DOM.Interface;

namespace HoustonBrowser.DOM
{
    public class ProcessingInstruction : Node
    {
        public ProcessingInstruction(string target, string data) :
            base(TypeOfNode.PROCESSING_INSTRUCTION_NODE, target, data)
        { }
    }
}