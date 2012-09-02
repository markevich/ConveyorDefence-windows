namespace Conveyor_Defence.Missiles
{
    class Missile
    {
        public int LeftDownTileID;
        public int RightDownTileID;
        public bool Active { get; set; }
        public bool Visible { get; set; }

        public Missile()
        {
            Visible = Active = false;
        }

    }

}
