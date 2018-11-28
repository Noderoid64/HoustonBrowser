using System;
using HoustonBrowser.DOM;
using System.Collections.Generic;
using HoustonBrowser.Parsing.Enums;

namespace HoustonBrowser.Parsing
{
    public static class StatesData
    {
        static internal int currentState;
        private static bool isLast;
        static public Stack<Node> OpenedTags;
        static public Node Root;
        static public InitialState InitialState;
        static public InHead InHead;
        static public InBody InBody;

        public static bool IsLast { get => isLast;}

        static StatesData()
        {
            InitialState = new InitialState();
            InHead = new InHead();
            InBody = new InBody();
            currentState = (int)InsertionModes.Initial;
            isLast = false;
        }
        static public void SetData(Stack<Node> openTags, Node rootNode)
        {
            Root = rootNode;
            OpenedTags = openTags;
        }
        static internal void FinishParsing()
        {
            isLast = true;
        }
    }
}
