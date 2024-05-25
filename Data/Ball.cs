using System.Diagnostics;
using System.Numerics;

namespace Data
{
    public interface IBall
    {

        Vector2 Position { get; }
        Vector2 Velocity { get; set; }
        int Radius { get; }
        int Mass { get; }

    }
    internal class Ball : IBall
    {

        
        private Vector2 _position;
        private Vector2 _velocity;
        private int MovingTime;

        private const int _mass = 15;
        private const int _radius = 40;

        private Stopwatch stopwatch = new Stopwatch();

        public Ball(int x, int y)
        {
            Random random = new Random();
            _position = new Vector2(x, y);

            // Initialize velocity with distinct random values
            _velocity = new Vector2(random.Next(-3, 3), random.Next(-3, 3));
            while (_velocity.X == 0 || _velocity.Y == 0) // Ensure non-zero velocity
            {
                _velocity = new Vector2(random.Next(-3, 3), random.Next(-3, 3));
            }

            MovingTime = 200;

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
            private set
            {
                _position = value;
            }
        }

        public Vector2 Velocity
        {
            get { return _velocity; }
            set
            {
                _velocity = value;
            }
        }

        public int Radius
        {
            get => _radius;
        }

        public int Mass
        {
            get => _mass;
        }

        private readonly object movelock = new object();

        public void move()
        {
            lock (movelock)
            {
                Position += Velocity * MovingTime*0.1f;
                OnPositionChange();
            }
        }

        private void MoveBall()
        {
            Task.Run(async () =>
            {
                int wait = 0;
                while (true)
                {
                    stopwatch.Restart();
                    stopwatch.Start();
                 
                    move();
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
