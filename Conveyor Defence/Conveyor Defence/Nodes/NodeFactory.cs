using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Conveyor_Defence.Nodes.Strategies.Input;
using Conveyor_Defence.Map;

namespace Conveyor_Defence.Nodes
{
    internal static class NodeFactory
    {
        public static Node GetConveyour(NodeDirection direction)
        {
            return new Node(300f, 10, 11, direction, new StandartInput());
        }
    }
}
