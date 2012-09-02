using System.Collections.Generic;
using Conveyor_Defence.Misc;
using Conveyor_Defence.Missiles;
using Conveyor_Defence.Nodes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Conveyor_Defence.Map
{
    class NodeMap
    {
        public static NodeMap Instance { get; private set; }
        public static void CreateInstance()
        {
            Instance = new NodeMap();
        }
        public MissilesPool Missiles; 
        private Node[,] Nodes { get; set; }
        private List<Point> _towerIndexes;
        public NodeMap()
        {
            Nodes = new Node[TileMap.MapWidth,TileMap.MapHeight];
            _towerIndexes = new List<Point>();
            Missiles = new MissilesPool(100);
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
            System.Diagnostics.Debug.WriteLine(Missiles.Count);
        }

        public void Draw(SpriteBatch batch)
        {
            DrawNodes(batch);
            DrawMissiles(batch);
        }

        private void DrawMissiles(SpriteBatch batch)
        {
            foreach (Missile missile in Missiles)
            {
                missile.Draw(batch);
            }
        }

        private void DrawNodes(SpriteBatch batch)
        {
            var firstVisibleTile = Camera.FirstVisibleTileIndex;

            var firstX = firstVisibleTile.X;
            var firstY = firstVisibleTile.Y;

            for (int x = 0; x < TileMap.MapWidth; x++)
            {
                for (int y = 0; y < TileMap.MapHeight; y++)
                {
                    var index = new Point(firstX + x, firstY + y);

                    if (Camera.IsTileOutsideOfMap(index)) continue;

                    var node = Nodes[index.X, index.Y];
                    if (node == null) continue;

                    node.Draw(batch);

                    //DrawTileIndexes(batch, tileIndex, x, y); //helper method
                }
            }
        }

        public void SetNode(Node node, int x, int y)
        {
            Nodes[x, y] = node;
            node.Index = new Point(x,y);
        }

        public void AddTower(NodeDirection direction, int x, int y)
        {
            _towerIndexes.Add(new Point(x, y));
            SetNode(new Tower(){Direction = direction},x, y);
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
                    if(nextNodes.Count == 0) continue;
                    node.NextNode = nextNodes[0];
                }
        }
        public List<Tower> GetTowers()
        {
            var towers = new List<Tower>();
            foreach (var towerIndex in _towerIndexes)
            {
                towers.Add((Tower)this[towerIndex.X, towerIndex.Y]);
            }
            return towers;
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