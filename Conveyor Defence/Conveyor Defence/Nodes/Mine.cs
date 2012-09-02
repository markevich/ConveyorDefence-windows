using Conveyor_Defence.Missiles;
using Microsoft.Xna.Framework;

namespace Conveyor_Defence.Nodes
{
    class Mine : Node
    {
        public Mine(float outputCooldown) : base(outputCooldown)
        {
            this.LeftDownTileID = 13;
            this.RightDownTileID = 13;
        }

        protected override void Output(Missile missile)
        {
            base.Output(missile);
            missile.Visible = true;
        }

    }
}
