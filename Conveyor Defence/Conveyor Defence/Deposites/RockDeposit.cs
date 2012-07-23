using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Conveyor_Defence.Missiles;
using Microsoft.Xna.Framework;

namespace Conveyor_Defence.Deposits
{
    internal class RockDeposit : Deposit
    {
        private float generationTime;
        public Mine mine;
        public RockDeposit(float generationTime)
        {
            this.generationTime = generationTime;
        }

        private float timeSinseLastGeneration = 0;
        public void Update(GameTime gameTime)
        {
            timeSinseLastGeneration += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinseLastGeneration > generationTime)
            {
                timeSinseLastGeneration = 0;
                Output();
            }
        }

        private int rocksCount = 0;
        public void Output()
        {
            rocksCount++;
            System.Diagnostics.Debug.WriteLine(String.Format("Rock deposit generate rock {0} !", rocksCount));
            var rock = new Projectile();
            if (mine != null)
                mine.Input(rock);
        }
    }
}
