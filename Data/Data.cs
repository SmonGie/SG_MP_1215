using System.Diagnostics;
using System.Numerics;

namespace Data
{
    //Definicja klasy Data jako wewnetrzenj i podklasy AbstractDataApi
    internal class Data : AbstractDataApi
    {
        private int Height;
        private int Width;
        private List<IBall> Balls { get; }

        public override int getWidthOfWindow()
        {
            return Width;
        }

        public override int getHeightOfWindow()
        {
            return Height;
        }
        public Data(int HeightOfWindow, int WidthOfWindow)
        {
            Balls = new List<IBall>();
            Height = HeightOfWindow;
            Width = WidthOfWindow;
        }

        public override event EventHandler BallEvent;

        private void BallPositionChanged(object sender, EventArgs e)
        {
            if (sender != null)
            {
                BallEvent?.Invoke(sender, EventArgs.Empty);
            }
        }

        public override void SpawnBalls(int amount)
        {

            int ballnumber = Balls.Count;


            Random random = new Random();

            int x;
            int y;
            for (int i = 0; i < amount; i++)
            {
                x = random.Next(10, Width - 10);
                y = random.Next(10, Height - 10);

                Balls.Add(new Ball(x, y));

                Ball ball = new Ball(random.Next(10, Width - 10), random.Next(10, Height - 10));
                Balls.Add(ball);
                ball.PositionChange += BallPositionChanged;

            }





        //  return AbstractBallApi.CreateInstance(position, velocityX, velocityY, mass, radius, isWorking);
    }

        public override int GetNumberOfBalls()
        {
            return Balls.Count;
        }

        public override IBall GetBall(int number)
        {
            return Balls[number];
        }
    }
}
