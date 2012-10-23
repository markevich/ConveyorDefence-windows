using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConveyourDefence.Missiles;

namespace ConveyourDefence.Nodes.Strategies.Input
{
    class NoInput:InputStrategy
    {
        public override void Input(ref Missile missile, ref List<Missile> missiles){}
    }
}
