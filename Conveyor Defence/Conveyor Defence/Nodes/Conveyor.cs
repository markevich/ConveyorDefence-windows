namespace Conveyor_Defence.Nodes
{
    class Conveyor : Node
    {
        public Conveyor(float outputCooldown) : base(outputCooldown)
        {}
       
        protected override void Output(NodeData data)
        {
            base.Output(data);
            System.Diagnostics.Debug.WriteLine("Conveyour passed projectile number {0}!", OutputsCount);
        }

    }
}
