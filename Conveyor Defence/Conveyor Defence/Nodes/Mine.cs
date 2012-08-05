namespace Conveyor_Defence.Nodes
{
    class Mine : Node
    {
        public Mine(float outputCooldown) : base(outputCooldown)
        {}

        protected override void Output(NodeData data)
        {
            base.Output(data);
            System.Diagnostics.Debug.WriteLine("Mine produce projectile number {0} !", OutputsCount);
        }

    }
}
