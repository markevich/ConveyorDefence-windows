using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Conveyor_Defence
{
    interface INode
    {
        void Input(INodeData data);
        void Process();
        void Output();
    }
}
