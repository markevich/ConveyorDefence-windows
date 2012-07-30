namespace Conveyor_Defence
{
    class Mine : Node
    {
        public Mine(float outputCooldown) : base(outputCooldown)
        {}

        protected override void Output(NodeData data)
        {
            base.Output(data);
            System.Diagnostics.Debug.WriteLine(string.Format("Mine produce projectile number {0} !", outputsCount));
        }

    }
}
