using Conveyor_Defence.Missiles;

namespace Conveyor_Defence.Deposits
{
    internal class RockDeposit : Node
    {
        public RockDeposit(float generationTime) :base(generationTime)
        {}

        protected override void Process()
        {
            this.timeSinseLastProcess = 0;
            var data = new Rock();
            Output(data);
        }
        protected override void Output(NodeData data)
        {
            base.Output(data);
            System.Diagnostics.Debug.WriteLine("Rock number {0} generated from deposit!", outputsCount);
        }

        protected override bool HasNodeDatas()
        {
            return true;
        }

    }
}
