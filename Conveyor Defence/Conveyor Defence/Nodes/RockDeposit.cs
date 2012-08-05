using Conveyor_Defence.Missiles;

namespace Conveyor_Defence.Nodes
{
    internal class RockDeposit : Node
    {
        public RockDeposit(float generationTime) :base(generationTime)
        {}

        protected override void Process()
        {
            TimeSinseLastProcess = 0;
            var data = new Rock();
            Output(data);
        }
        protected override void Output(NodeData data)
        {
            base.Output(data);
            System.Diagnostics.Debug.WriteLine("Rock number {0} generated from deposit!", OutputsCount);
        }

        protected override bool HasNodeDatas()
        {
            return true;
        }

    }
}
