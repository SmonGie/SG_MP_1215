using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Data
{
    public interface IBall
    {

        Vector2 Position { get; }
        Vector2 Velocity { get; set; }
       
    }
    internal class Ball : IBall
    {

        
        private Vector2 _position;
        private Vector2 _velocity;
        private int MovingTime;



        private Stopwatch stopwatch = new Stopwatch();

        public Ball(int x, int y,int Radius)
        {
            Radius = 5;
            Random random = new Random();
            Position = new Vector2(x, y)
            {
                X = x,
                Y = y

            };

            Velocity = new Vector2(x, y)
            {
                X = random.Next()*2,
                Y = random.Next() * 2

            };

            MovingTime = 1000;

            MoveBall();
        }




        public event EventHandler PositionChange;

        internal void OnPositionChange()
        {
            PositionChange?.Invoke(this, EventArgs.Empty);
        }
       

        public Vector2 Position
        {
            get => _position;
            private set { _position = value; }
        }

        public Vector2 Velocity
        {
            get { return _velocity; }
            set
            {
                _velocity = value;
            }
        }

        

        private void move(int MovingTime)
        {
            Vector2 newPosition = new Vector2((Velocity.X * MovingTime) + Position.X, (Velocity.Y * MovingTime) + Position.Y);
            Position = newPosition;
            OnPositionChange();
        }

        private readonly object movelock = new object();

        private void MoveBall()
        {
            Task.Run(async () =>
            {
                int wait = 0;
                while (true)
                {
                    stopwatch.Restart();
                    stopwatch.Start();
                 
                    lock (movelock)
                    {
                        move(MovingTime - (int)stopwatch.ElapsedMilliseconds);
                    }
                    stopwatch.Stop();
                    if (MovingTime - stopwatch.ElapsedMilliseconds < 0)
                    {
                        wait = 0;
                    }
                    else
                    {
                        wait = MovingTime - (int)stopwatch.ElapsedMilliseconds;
                    }

                    await Task.Delay(wait);
                }
            });
        }

        
    }
}
