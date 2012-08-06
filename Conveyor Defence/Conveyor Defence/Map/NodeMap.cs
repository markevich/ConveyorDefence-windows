using System.Collections.Generic;
using Conveyor_Defence.Nodes;
using Microsoft.Xna.Framework;

namespace Conveyor_Defence.Map
{
    class NodeMap
    {
        public static NodeMap Instance { get; private set; }
        public static void CreateInstance()
        {
            Instance = new NodeMap();
        }
        private readonly TileMap _map;
        private Node[,] Nodes { get; set; }
        public NodeMap()
        {
            Nodes = new Node[100,100];
        }

        public Node this[int x, int y]
        {
            get { return Nodes[x, y]; }
        }

        public void Update(GameTime gameTime)
        {
            foreach (var node in Nodes)
            {
                if(node != null)
                    node.Update(gameTime);
            }
        }
        public void SetNode(Node node, int x, int y)
        {
            Nodes[x, y] = node;
        }

        private List<Node> NextNodes(int x, int y)
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
        public void UpdateSiblings()
        {
            for (int x = 0; x < Nodes.GetLength(0); x++)
                for (int y = 0; y < Nodes.GetLength(1); y++)
                {
                    var node = Nodes[x, y];
                    if(node == null)
                        continue;

                    if (node.Direction == NodeDirection.Empty) continue;

                    var nextNodes = NextNodes(x, y);
                    node.NextNode = nextNodes[0];
                }
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