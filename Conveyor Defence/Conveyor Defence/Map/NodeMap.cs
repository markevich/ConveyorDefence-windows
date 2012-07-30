using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Conveyor_Defence
{
    class NodeMap
    {
        private Node[,] Nodes { get; set; }
        public NodeMap()
        {
            Nodes = new Node[100,100];
        }

        public List<Node> NextNodes(int x, int y)
        {
            var nodes = new List<Node>();
            if (Nodes[x, y].Direction == NodeDirection.Empty)
                return nodes;

            var siblings = Siblings(x, y);
            var direction = Nodes[x, y].Direction;
            if (siblings.Count != 0)
                nodes.Add(siblings[direction]);

            return nodes;
        }

        private Dictionary<NodeDirection, Node> Siblings(int x, int y)
        {
            var siblings = new Dictionary<NodeDirection, Node>();
            if(x > 9 || y > 9)
                return siblings;
            if(y % 2 == 0)
            {
                siblings.Add(NodeDirection.LeftUp, Nodes[x - 1, y - 1]);
                siblings.Add(NodeDirection.LeftDown, Nodes[x - 1, y + 1]);
                siblings.Add(NodeDirection.RightUp, Nodes[x, y - 1]);
                siblings.Add(NodeDirection.RightDown, Nodes[x, y + 1]);
            }
            else
            {
                siblings.Add(NodeDirection.LeftUp, Nodes[x, y - 1]);
                siblings.Add(NodeDirection.LeftDown, Nodes[x, y + 1]);
                siblings.Add(NodeDirection.RightUp, Nodes[x + 1, y - 1]);
                siblings.Add(NodeDirection.RightDown, Nodes[x + 1, y + 1]);
            }
            return siblings;
        }
    }

    enum NodeDirection
    {
        Empty,
        LeftUp,
        LeftDown,
        RightUp,
        RightDown
    }
}