using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using Conveyor_Defence.Misc;

namespace Conveyor_Defence.Missiles
{
    class MissilesPool
    {
        private ObjectPool<Missile> _missiles; 
        public MissilesPool(int size)
        {
            _missiles = new ObjectPool<Missile>(size);
        }
        public Missile GetFreeMissile()
        {
            foreach (Missile missile in _missiles)
            {
                if (missile.Active) continue;

                missile.Active = true;
                missile.RemoveProperties();
                return missile;
            }
            return _missiles.AddNewObject();
        }
        public int Count{
            get { 
                int count = 0;
                foreach (Missile missile in _missiles)
                {
                    if (missile.Active)
                    {
                        count++;
                    }
                }
                return count;
            }}
    }
}
