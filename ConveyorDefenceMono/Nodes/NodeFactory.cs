using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConveyourDefence.Nodes.Strategies.Input;
using ConveyourDefence.Map;
using ConveyourDefence.Nodes.Strategies.Output;
using ConveyourDefence.Nodes.Strategies.Process;

namespace ConveyourDefence.Nodes
{
    internal static class NodeFactory
    {
        public static Node GetConveyour(NodeDirection direction)
        {
            return new Node(600f, 10, 11, direction, new StandartInput(), new StandartProcess(), new StandartOutput());
        }
        public static Node GetRockMine(NodeDirection direction)
        {
            float outputCD = new Random().Next(1000, 1500);
            return new Node(outputCD, 12, 12, direction, new NoInput(), new GenerateRock(), new StandartOutput());
        }
        public static Node Tower(NodeDirection direction)
        {
            return new Node(200, 15, 15, direction, new StandartInput(), new StandartProcess(), new NoOutput());
        }
    }
}
