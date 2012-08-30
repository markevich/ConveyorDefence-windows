using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Conveyor_Defence.Nodes
{
    class Tower:Node
    {
        public Tower(float outputCooldown = 0) : base(outputCooldown)
        {
            this.LeftDownTileID = 15;
            this.RightDownTileID = 15;
        }

        private void Shoot(int x, int y)
        {
            
        }
    }
}
