using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public abstract class AbstractBallApi
    {
        public abstract Vector2 Position { get; }
        public abstract int X { get; }
        public abstract int Y { get; }
        public abstract int VelocityX { get; set; }
        public abstract int VelocityY { get; set; }
        public abstract int Mass { get; }
        public abstract int Radius { get; }
        public abstract int Diameter { get; }
        public abstract bool isWorking { get; set; }
        public abstract void setVelocity(int vX, int vY);
        public abstract void AddPropertyChangedListener(PropertyChangedEventHandler handler);
        public static AbstractBallApi CreateInstance(Vector2 position, int velocityX, int velocityY, int mass, int radius, bool isWorking)
        {
            return new Ball(position, velocityX, velocityY, mass, radius, isWorking);
        }

    }
}
