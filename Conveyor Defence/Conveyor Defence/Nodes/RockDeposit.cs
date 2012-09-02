using Conveyor_Defence.Missiles;
using Conveyor_Defence.Missiles.Properties;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Conveyor_Defence.Map;

namespace Conveyor_Defence.Nodes
{
    internal class RockDeposit : Node
    {
        public RockDeposit(float generationTime) :base(generationTime)
        {
            this.LeftDownTileID = 12;
            this.RightDownTileID = 12;
        }
        //protected override void Output(NodeData data)
        //{
        //    base.Output(data);
        //    System.Diagnostics.Debug.WriteLine("Rock number {0} generated from deposit!", OutputsCount);
        //}

        protected override bool HasNodeDatas()
        {
            var rock = new Missile().WithProperty(new Stony());
            //NodeMap.Instance.Missiles.Put(rock);;
            _missiles.Add(rock);
            return true;
        }
        protected override void DrawNodeData(SpriteBatch batch, Vector2 nodePosition, float depth)
        {}
    }
}
