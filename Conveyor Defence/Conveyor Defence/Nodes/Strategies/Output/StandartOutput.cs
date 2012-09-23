using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Conveyor_Defence.Missiles;

namespace Conveyor_Defence.Nodes.Strategies.Output
{
    class StandartOutput:OutputStrategy
    {
        public override void Output(ref List<Missile> missiles,ref Node nextNode)
        {
            var missile = missiles[0];
            missiles.RemoveAt(0);
            if(nextNode == null)
               missile.Deactivate(); 
            else
            nextNode.Input(missile);
                
        }
    }
}
