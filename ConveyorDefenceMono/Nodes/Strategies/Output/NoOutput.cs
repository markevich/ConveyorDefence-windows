using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConveyourDefence.Missiles;

namespace ConveyourDefence.Nodes.Strategies.Output
{
    class NoOutput:OutputStrategy
    {
        public override void Output(ref List<Missile> missiles, ref Node nextNode)
        {
            missiles.Last().Visible = false;
        }
    }
}
