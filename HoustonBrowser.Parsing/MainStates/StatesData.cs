﻿using System;
using HoustonBrowser.DOM;
using System.Collections.Generic;
using HoustonBrowser.Parsing.Enums;

namespace HoustonBrowser.Parsing
{
    public static class StatesData
    {
        static internal int currentState;
        private static bool isLast;
        static public Stack<Node> openedTags;
        static public Node root;
        static public InitialState initialState;
        static public InHead inHead;
        static public InBody inBody;

        public static bool IsLast { get => isLast;}

        static StatesData()
        {
            initialState = new InitialState();
            inHead = new InHead();
            inBody = new InBody();
            currentState = (int)InsertionModes.Initial;
            isLast = false;
        }
        static public void SetData(Stack<Node> openTags, Node rootNode)
        {
            root = rootNode;
            openedTags = openTags;
        }
        static internal void FinishParsing()
        {
            isLast = true;
        }
    }
}