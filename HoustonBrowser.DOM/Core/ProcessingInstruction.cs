using System;
using HoustonBrowser.DOM.Core.Interface;

namespace HoustonBrowser.DOM.Core
{
    public class ProcessingInstruction : Node
    {
        public ProcessingInstruction(string target, string data) :
            base(TypeOfNode.PROCESSING_INSTRUCTION_NODE, target, data)
        { }
    }
}