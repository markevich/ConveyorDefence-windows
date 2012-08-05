using System.Collections.Generic;
using Conveyor_Defence.Map;
using Microsoft.Xna.Framework;

namespace Conveyor_Defence.Nodes
{
    abstract class Node
    {
        private readonly float _processCooldown;
        protected float TimeSinseLastProcess;
        public Node NextNode { get; set; }
        private readonly List<NodeData> _nodeDatas;
        public NodeDirection Direction { get; set; }
        protected Node(float outputCooldown)
        {
            _processCooldown = outputCooldown;
            _nodeDatas = new List<NodeData>();
        }

        private void Input(NodeData data)
        {
            _nodeDatas.Add(data);
        }
        protected virtual void Process()
        {
            TimeSinseLastProcess = 0;
            var data = _nodeDatas[0];
            _nodeDatas.RemoveAt(0);
            Output(data);
        }

        protected int OutputsCount;
        protected virtual void Output(NodeData data)
        {
            OutputsCount++;
            if(NextNodeExists())
                NextNode.Input(data);
        }

        public void Update(GameTime gameTime)
        {
            if (!HasNodeDatas()) return;
            
            TimeSinseLastProcess += gameTime.ElapsedGameTime.Milliseconds;
            if (TimeSinseLastProcess > _processCooldown)
            {
                Process();
            }
        }

        protected virtual bool HasNodeDatas()
        {
            return _nodeDatas.Count > 0;
        }

        private bool NextNodeExists()
        {
            return NextNode != null;
        }
    }
}
