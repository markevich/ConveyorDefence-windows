using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConveyourDefence.Map;
using ConveyourDefence.Missiles;
using ConveyourDefence.Missiles.Properties;

namespace ConveyourDefence.Nodes.Strategies.Process
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
