using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Conveyor_Defence.Missiles;

namespace Conveyor_Defence.Nodes.Strategies
{
    abstract class ProcessStrategy
    {
        public abstract void Process(Missile missile);
    }
}
