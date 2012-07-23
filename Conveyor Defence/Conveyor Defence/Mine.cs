using Microsoft.Xna.Framework;
using Conveyor_Defence.Missiles;

namespace Conveyor_Defence
{
    class Mine
    {
        private Projectile projectile;
        private bool inputReceived = false;

        public void Input(Projectile data)
        {
            inputReceived = true;
            projectile = data;
        }

        float timeSinseLastProcessCall = 0f;
        public void Process()
        {
        }

        private int counter =0;
        public void Output()
        {
            counter++;
            System.Diagnostics.Debug.WriteLine(string.Format("projectile {0} ouputed!",counter));
        }

        public void Update(GameTime gameTime)
        {
            if (inputReceived)
            {
                timeSinseLastProcessCall += gameTime.ElapsedGameTime.Milliseconds;
                if (timeSinseLastProcessCall > 1000f)
                {
                    Output();
                    timeSinseLastProcessCall = 0;
                    inputReceived = false;
                }
            }

        }
    }
}
