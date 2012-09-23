using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Conveyor_Defence.Map;
using Conveyor_Defence.Missiles;
using Conveyor_Defence.Missiles.Properties;

namespace Conveyor_Defence.Nodes.Strategies.Process
{
    class GenerateRock:ProcessStrategy
    {
        public override void Process(ref List<Missile> missiles)
        {
            var rock = NodeMap.Instance.Missiles.GetFreeMissile().WithProperty(new Stony());
            rock.Visible = true;
            missiles.Add(rock);
        }
    }
}
