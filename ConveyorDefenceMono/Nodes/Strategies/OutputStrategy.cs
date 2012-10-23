using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConveyourDefence.Missiles;

namespace ConveyourDefence.Nodes.Strategies
{
    abstract class OutputStrategy
    {
        public abstract void Output(ref List<Missiles.Missile> missiles, ref Node nextNode);
    }
}
