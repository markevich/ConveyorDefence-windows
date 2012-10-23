using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConveyourDefence.Missiles;

namespace ConveyourDefence.Nodes.Strategies
{
    abstract class ProcessStrategy
    {
        public abstract void Process(ref List<Missile> missiles);
    }
}
