namespace Conveyor_Defence
{
    class Conveyor : Node
    {
        public Conveyor(float outputCooldown, Node nextNode) : base(outputCooldown, nextNode)
        {}
       
        protected override void Output(NodeData data)
        {
            base.Output(data);
            System.Diagnostics.Debug.WriteLine(string.Format("Conveyour passed projectile number {0}!", outputsCount));
        }

  
    }
}
