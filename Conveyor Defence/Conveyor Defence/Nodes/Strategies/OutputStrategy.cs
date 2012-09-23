using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Conveyor_Defence.Missiles;

namespace Conveyor_Defence.Nodes.Strategies
{
    abstract class OutputStrategy
    {
        public abstract void Output(ref List<Missiles.Missile> missiles, ref Node nextNode);
    }
}
