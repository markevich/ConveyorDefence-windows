﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Conveyor_Defence.Missiles;

namespace Conveyor_Defence.Nodes.Strategies.Input
{
    class StandartInput:InputStrategy
    {
        public override Missile Input(Missile missile)
        {
            return missile;
        }
    }
}
