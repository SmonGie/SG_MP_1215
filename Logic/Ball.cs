using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static Logic.AbstractLogicApi;

namespace Logic
{
    internal class Ball : IBall, IEquatable<Ball>
    {
        public Ball(double xVelocity, double yVelocity, int x, int y, int diameter)
            : this(new Vector2(xVelocity, yVelocity), new Vector2(x, y), diameter) { }
        public Ball(Vector2 velocity, Vector2 position, int diameter)
        {
            this.velocity=velocity;
            this.position=position;
            this.diameter=diameter;
            this.radius = diameter/2 ;


        }
        private Vector2 velocity;
        private Vector2 position;
        private int diameter;
        private int radius;

        #region IBall
        public Vector2 Velocity
        {
            get { return this.velocity; }
            private set { this.velocity = value; }
        }

        public Vector2 Position
        {
            get { return this.position; }
            private set { this.position = value; }
        }

        public int Radius
        {
            get { return this.radius; }
            private set { this.radius = value; }
        }

        public int Diameter
        {
            get { return this.diameter; }
            private set { this.diameter = value; }
        }
        #endregion

        private Random Random = new Random();


        public void Move(Vector2 xBorder, Vector2 yBorder ,float force = 0.5f)
        {
            if (velocity.VectorEqualsZero())
                return;
            position += velocity * force;

            var (x, y) = position;
            
            if(!x.CheckBoundry(xBorder.X, xBorder.Y, radius))
            {
                velocity = new Vector2(-velocity.X, velocity.Y);
            }
            if(!y.CheckBoundry(yBorder.X, yBorder.X, radius))
            {
                velocity = new Vector2(velocity.X, -velocity.Y);
            }
        }

        public bool Equals(Object? other)
        {
            return other is Ball ball
                && Equals(ball);
        }

        public bool Equals(Ball? other)
        {
            return other is not null
                && Velocity == other.Velocity
                && Position == other.Position
                && Diameter == other.Diameter;
        }
    }

    public static class Boundry
    {
        public static bool CheckBoundry(this double value, double min, double max, double padding = 0f)
        {
            return (value - padding >= min) && (value + padding <= max);
        }

        public static bool CheckBoundry(this int value, int min, int max)
        {
            return (value >= min) && (value <= max);
        }
    }
}
