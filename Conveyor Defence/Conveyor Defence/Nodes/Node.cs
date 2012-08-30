using System.Collections.Generic;
using Conveyor_Defence.Map;
using Conveyor_Defence.NodesData;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Conveyor_Defence.Nodes
{
    abstract class Node
    {
        private readonly float _processCooldown;
        protected float TimeSinseLastProcess;
        public Node NextNode { get; set; }
        private readonly List<NodeData> _nodeDatas;
        public NodeDirection Direction { get; set; }
        public int LeftDownTileID;
        public int RightDownTileID;
        private int TileID{
            get
            {
                switch (Direction)
                {
                        case NodeDirection.LeftDown:
                        {
                            return LeftDownTileID;

                        }
                        case NodeDirection.RightDown:
                        {
                            return RightDownTileID;
                        }
                }
                return 0;
            }
        }
        protected Node(float outputCooldown)
        {
            _processCooldown = outputCooldown;
            _nodeDatas = new List<NodeData>();
        }

        private void Input(NodeData data)
        {
            _nodeDatas.Add(data);
        }
        protected virtual void Process()
        {
            TimeSinseLastProcess = 0;
            var data = GetCurrentNodeData();
            _nodeDatas.RemoveAt(0);
            Output(data);
        }

        protected int OutputsCount;
        protected virtual void Output(NodeData data)
        {
            OutputsCount++;
            if(NextNodeExists())
                NextNode.Input(data);
        }

        public void Update(GameTime gameTime)
        {
            if (!HasNodeDatas()) return;
            
            TimeSinseLastProcess += gameTime.ElapsedGameTime.Milliseconds;
            if (TimeSinseLastProcess > _processCooldown)
            {
                Process();
            }
        }

        protected virtual bool HasNodeDatas()
        {
            return _nodeDatas.Count > 0;
        }

        private NodeData GetCurrentNodeData()
        {
            return _nodeDatas[0];
        }

        private bool NextNodeExists()
        {
            return NextNode != null;
        }

        public virtual void Draw(SpriteBatch batch, Vector2 position, float depth)
        {
            if (TileID == 0) return;
            batch.Draw(
                Tile.TileSetTexture,
                position,
                Tile.GetSourceRectangle(TileID),
                Color.White,
                0.0f,
                Vector2.Zero,
                1.0f,
                SpriteEffects.None,
                depth
                );
            
            DrawNodeData(batch, position, depth);
           
        }
        public virtual void DrawNodeData(SpriteBatch batch, Vector2 nodePosition, float depth)
        {
            if (!HasNodeDatas()) return;
            var nodeDataID = GetCurrentNodeData().LeftDownTileID;
            nodePosition = new Vector2(nodePosition.X , nodePosition.Y - Tile.TileHeight/4+2);
            batch.Draw(
               Tile.TileSetTexture,
               nodePosition,
               Tile.GetSourceRectangle(nodeDataID),
               Color.White,
               0.0f,
               Vector2.Zero,
               1.0f,
               SpriteEffects.None,
               depth - Tile.DepthModifier
               );
        }
    }
}
