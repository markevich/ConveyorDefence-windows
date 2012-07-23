using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Conveyor_Defence.Missiles;
using Microsoft.Xna.Framework;

namespace Conveyor_Defence
{
    class Conveyor
    {
        private Projectile projectile;
        private bool inputReceived = false;
        private Conveyor conveyor;
        private string name;
        public Conveyor(Conveyor conveyor, string name)
        {
            this.conveyor = conveyor;
            this.name = name;
        }

        public void Input(Projectile data)
        {
            inputReceived = true;
            projectile = data;
        }

        float timeSinseLastProcessCall = 0f;
        public void Process()
        {
        }

        private int counter = 0;

        public void Output()
        {
            counter++;
            System.Diagnostics.Debug.WriteLine(string.Format("Conveyour {0} passed projectile {1} times!",name, counter));
            if(conveyor != null)
                conveyor.Input(projectile);
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
