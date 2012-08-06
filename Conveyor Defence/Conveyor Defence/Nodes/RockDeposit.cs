using Conveyor_Defence.NodesData;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Conveyor_Defence.Nodes
{
    internal class RockDeposit : Node
    {
        public RockDeposit(float generationTime) :base(generationTime)
        {
            this.LeftDownTileID = 12;
            this.RightDownTileID = 12;
        }

        protected override void Process()
        {
            TimeSinseLastProcess = 0;
            var data = new Rock {LeftDownTileID = 14, RightDownTileID = 14};
            Output(data);
        }
        protected override void Output(NodeData data)
        {
            base.Output(data);
            System.Diagnostics.Debug.WriteLine("Rock number {0} generated from deposit!", OutputsCount);
        }

        protected override bool HasNodeDatas()
        {
            return true;
        }

        public override void DrawNodeData(SpriteBatch batch, Vector2 nodePosition, float depth)
        {}
    }
}
