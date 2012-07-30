using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Conveyor_Defence
{
    abstract class Node
    {
        protected float processCooldown;
        protected float timeSinseLastProcess = 0;
        public Node NextNode { get; set; }
        protected List<NodeData> nodeDatas;
        public NodeDirection Direction { get; set; }
        protected Node(float outputCooldown)
        {
            this.processCooldown = outputCooldown;
            nodeDatas = new List<NodeData>();
        }

        public virtual void Input(NodeData data)
        {
            nodeDatas.Add(data);
        }
        protected virtual void Process()
        {
            timeSinseLastProcess = 0;
            var data = nodeDatas[0];
            nodeDatas.RemoveAt(0);
            Output(data);
        }

        protected int outputsCount = 0;
        protected virtual void Output(NodeData data)
        {
            outputsCount++;
            if(nextNodeExists())
                NextNode.Input(data);
        }

        public virtual void Update(GameTime gameTime)
        {
            if (HasNodeDatas())
            {
                timeSinseLastProcess += gameTime.ElapsedGameTime.Milliseconds;
                if (timeSinseLastProcess > processCooldown)
                {
                    Process();
                }
            }
        }

        protected virtual bool HasNodeDatas()
        {
            return nodeDatas.Count > 0;
        }
        protected bool nextNodeExists()
        {
            return NextNode != null;
        }
    }
}
