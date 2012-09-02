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

        protected override bool HasNodeDatas()
        {
            if(_missiles.Count == 0)
            {
                var rock = CreateRock();
                _missiles.Add(rock);
            }
            return true;
        }

        private Missile CreateRock()
        {
            return NodeMap.Instance.Missiles.GetFreeMissile().WithProperty(new Stony());
        }
    }
}
