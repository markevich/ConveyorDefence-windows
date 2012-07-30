namespace Conveyor_Defence
{
    class Conveyor : Node
    {
        public Conveyor(float outputCooldown) : base(outputCooldown)
        {}
       
        protected override void Output(NodeData data)
        {
            base.Output(data);
            System.Diagnostics.Debug.WriteLine(string.Format("Conveyour passed projectile number {0}!", outputsCount));
        }

  
    }
}
