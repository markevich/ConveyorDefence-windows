using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Conveyor_Defence.Map;
using Conveyor_Defence.Missiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Conveyor_Defence.Nodes
{
    class Tower:Node
    {
        public Tower(float outputCooldown = 0) : base(outputCooldown)
        {
            this.LeftDownTileID = 15;
            this.RightDownTileID = 15;
        }
        protected override void Output(Missile data){}
        protected override void Input(Missile missile)
        {
            base.Input(missile);
            missile.Visible = false;
        }
        public override void Draw(SpriteBatch batch)
        {
            base.Draw(batch);
            var position = new Vector2(Position.X + Tile.TileWidth / 2 - 4, Position.Y + Tile.TileHeight / 3);
            batch.DrawString(Game.CountFont, _missiles.Count.ToString(), position, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }

        public void Shoot(int x, int y)
        {
            
        }
    }
}
