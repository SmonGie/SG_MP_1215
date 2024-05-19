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
    internal class Ball : AbstractBallApi, INotifyPropertyChanged
    {
        private Vector2 position;
        private int Vx;
        private int Vy;
        private bool _isWorkingSim;
        private readonly int mass;
        private readonly int radius;
        private static readonly SemaphoreSlim velocityLock = new SemaphoreSlim(1);
        private static readonly SemaphoreSlim positionLock = new SemaphoreSlim(1);
        private Stopwatch stopwatch = new Stopwatch();

        public Ball(Vector2 position, int vx, int vy, int mass, int radius, bool _isWorkingSim)
        {
            this.position = position;
            Vx = vx;
            Vy = vy;
            isWorking = _isWorkingSim;
            this.mass = mass;
            this.radius = radius;
            Task.Run(() => Move());
        }

        public override void AddPropertyChangedListener(PropertyChangedEventHandler handler)
        {
            this.PropertyChanged += handler;
        }

        public override Vector2 Position
        {
            get
            {
                positionLock.Wait();
                try
                {
                    return position;
                }
                finally { positionLock.Release(); }
            }
        }

        public override int PositionX => (int)position.X;

        public override int PositionY => (int)position.Y;

        private void setPosition(Vector2 Pos)
        {
            positionLock.Wait();
            try
            {
                position.X = Pos.X;
                position.Y = Pos.Y;
            }
            finally
            {
                positionLock.Release();
            }
            OnPropertyChanged(nameof(PositionX));
            OnPropertyChanged(nameof(PositionY));
        }

        public override int VelocityX
        {
            get
            {
                velocityLock.Wait();
                try
                {
                    return Vx;
                }
                finally { velocityLock.Release(); }
            }
            set => Vx = value;
        }

        public override int VelocityY
        {
            get
            {
                velocityLock.Wait();
                try
                {
                    return Vy;
                }
                finally { velocityLock.Release(); }
            }
            set => Vy = value;
        }
        public override void setVelocity(int Vx, int Vy)
        {
            velocityLock.Wait();
            try
            {
                this.Vx = Vx;
                this.Vy = Vy;
            }
            finally
            {
                velocityLock.Release();
            }
        }

        public override int Mass => mass;
        public override int Radius => radius;

        private async Task Move()
        {
            while (true)
            {
                if (isWorking)
                {
                    stopwatch.Restart();

                    positionLock.Wait();
                    try
                    {
                        position.X += Vx;
                        position.Y += Vy;
                    }
                    finally
                    {
                        positionLock.Release();
                    }
                    OnPropertyChanged(nameof(PositionX));
                    OnPropertyChanged(nameof(PositionY));

                    stopwatch.Stop();
                    
                }
                double velocity = Math.Sqrt(Vx * Vx + Vy * Vy);
                await Task.Delay(TimeSpan.FromMilliseconds(60));
            }
        }

        public override bool isWorking
        {
            get { return _isWorkingSim; }
            set
            {
                _isWorkingSim = value;
            }
        }

        public override int Diameter => radius * 2;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
