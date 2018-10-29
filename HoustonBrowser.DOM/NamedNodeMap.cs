using HoustonBrowser.DOM.Interface;
using System.Collections.Generic;
using System.Collections;

namespace HoustonBrowser.DOM
{
    public class NamedNodeMap: INamedNodeMap
    {
        private Dictionary<string, Node> nodeMap;

        public int Length { get => nodeMap.Count; }
        
        public NamedNodeMap()
        {
            nodeMap = new Dictionary<string, Node>();
        }

        public Node GetNamedItem(string name)
        {
            Node node;
            if (nodeMap.TryGetValue(name, out node))
                return node;
            else
                return null;
        }

        public Node SetNamedItem(Node arg)
        {
            Node node = null;
            if (nodeMap.TryGetValue(arg.NodeName, out node))
                nodeMap.Remove(node.NodeName);

            nodeMap.Add(arg.NodeName, arg);
            return node;
        }

        public Node RemoveNamedItem(string name)
        {
            Node node = null;
            if (nodeMap.TryGetValue(name, out node))
                nodeMap.Remove(name);
            
            return node;
        }

        public IEnumerator GetEnumerator()
        {
            return nodeMap.GetEnumerator();
        }
    }
}