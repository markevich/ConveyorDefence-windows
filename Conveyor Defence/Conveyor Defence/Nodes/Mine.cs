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

        //protected override void Output(NodeData data)
        //{
        //    base.Output(data);
        //    System.Diagnostics.Debug.WriteLine("Mine produce projectile number {0} !", OutputsCount);
        //}

    }
}
