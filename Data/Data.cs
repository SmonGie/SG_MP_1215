using System.Numerics;

namespace Data
{
    //Definicja klasy Data jako wewnetrzenj i podklasy AbstractDataApi
    internal class Data : AbstractDataApi
    {
        private Logger _logger;
        public override int Width => _width;
        public override int Height => _height;
  
        private readonly List<IBall> _balls = new List<IBall>();
        private readonly int _width = 500;
        private readonly int _height = 500;
        // Zdarzenie wywoływane przy zmianie pozycji piłki
        public override event EventHandler<BallEventArgs> BallEvent;

        // Metoda zwracająca pozycję piłki na podstawie numeru w liście
        public override Vector2 GetBallPosition(int number)
        {
            return _balls[number].Position;
        }
        public Data()
        {
            _logger = new Logger();
        }
        public override void SpawnBalls(int amount)
        {

            int number = _balls.Count;
            Random random = new Random();
            for (int i = 0; i < amount; i++)
            {
                // Tworzenie nowej piłki z losową pozycją w obrębie planszy
                Ball ball = new Ball(random.Next(100,Width-100), random.Next(Height-100), i + number);
                int ballIndex = _balls.Count;
                ball.PositionChange += BallLogger_PositionChange;
                _balls.Add(ball);
            }

    }
        private void BallLogger_PositionChange(object sender, Tuple<Vector2, int, DateTime> e)
        {
            if (sender != null)
            {
                int ballIndex = _balls.IndexOf((IBall)sender);
                BallEvent?.Invoke(sender, new BallEventArgs(ballIndex));
                _logger.AddObjectToQueue((IBall)sender, e.Item3);
            }
        }

        // Metoda zwracająca liczbę piłek w liście
        public override int GetNumberOfBalls()
        {
            return _balls.Count;
        }
        // Metoda zwracająca piłkę na podstawie numeru w liście
        public override IBall GetBall(int number)
        {
            return _balls[number];
        }
    }
}
