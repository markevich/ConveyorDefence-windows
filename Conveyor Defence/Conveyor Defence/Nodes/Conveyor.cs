namespace Conveyor_Defence.Nodes
{
    class Conveyor : Node
    {
        public Conveyor(float outputCooldown) : base(outputCooldown)
        {
            this.LeftDownTileID = 10;
            this.RightDownTileID = 11;
        }
       
        //protected override void Output(NodeData data)
        //{
        //    base.Output(data);
        //    System.Diagnostics.Debug.WriteLine("Conveyour passed projectile number {0}!", OutputsCount);
        //}

    }
}
