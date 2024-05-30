using System.Numerics;

namespace Data
{
    //Definicja klasy Data jako wewnetrzenj i podklasy AbstractDataApi
    internal class Data : AbstractDataApi
    {
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

        public override void SpawnBalls(int amount)
        {


            Random random = new Random();
            for (int i = 0; i < amount; i++)
            {
                // Tworzenie nowej piłki z losową pozycją w obrębie planszy
                Ball ball = new Ball(random.Next(100,Width-100), random.Next(Height-100));
                int ballIndex = _balls.Count;
                ball.PositionChange += (sender, args) => BallEvent?.Invoke(this, new BallEventArgs(ballIndex));
                _balls.Add(ball);
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
