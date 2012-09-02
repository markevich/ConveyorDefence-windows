using System.Collections.Generic;
using Conveyor_Defence.Map;
using Conveyor_Defence.Missiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Conveyor_Defence.Nodes
{
    abstract class Node
    {
        private readonly float _processCooldown;
        protected float TimeSinseLastProcess;
        public Node NextNode { get; set; }
        protected List<Missile> _missiles;
        public NodeDirection Direction { get; set; }
        public Point Index { get; set; }
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
            _missiles = new List<Missile>();
        }

        protected virtual void Input(Missile missile)
        {
            missile.NodeIndex = Index;
            _missiles.Add(missile);
        }
        protected virtual void Process(Missile missile)
        {

        }

        protected int OutputsCount;
        protected virtual void Output(Missile missile)
        {
            _missiles.RemoveAt(0);
            OutputsCount++;
            if (NextNodeExists())
                NextNode.Input(missile);
            else
                missile.Deactivate();

        }

        public void Update(GameTime gameTime)
        {
            if (!HasNodeDatas()) return;
            
            TimeSinseLastProcess += gameTime.ElapsedGameTime.Milliseconds;
            if (TimeSinseLastProcess > _processCooldown)
            {
                TimeSinseLastProcess = 0;
                var data = GetCurrentMissile();
                Process(data);
                Output(data);
            }
        }

        protected virtual bool HasNodeDatas()
        {
            return _missiles.Count > 0;
        }

        private Missile GetCurrentMissile()
        {
            return _missiles[0];
        }

        private bool NextNodeExists()
        {
            return NextNode != null;
        }

        public virtual void Draw(SpriteBatch batch)
        {
            if (TileID == 0) return;
            var depth = DepthCalculator.CalculateDepth(Index.X, Index.Y);

            batch.Draw(
                Tile.TileSetTexture,
                Position,
                Tile.GetSourceRectangle(TileID),
                Color.White,
                0.0f,
                Vector2.Zero,
                1.0f,
                SpriteEffects.None,
                depth
                );
        
        }

        protected Vector2 Position
        {
            get
            {
                int rowOffset = Index.Y%2 == 1 ? Tile.OddRowXOffset : 0;

                var position = Camera.WorldToScreen(
                    new Vector2((Index.X*Tile.TileStepX) + rowOffset, Index.Y*Tile.TileStepY));
                return position;
            }
        }
    }
}
