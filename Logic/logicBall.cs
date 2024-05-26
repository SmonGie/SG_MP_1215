using System;
using System.Numerics;
using Data;

namespace Logic
{
    internal class logicBall : AbstractLogicApi
    {
        private readonly AbstractDataApi dataApi;

        public logicBall(AbstractDataApi dataApi)
        {
            dataApi.BallEvent += CheckCollisions;
            this.dataApi = dataApi;
        }

        public override int GetNumberOfBalls()
        {
            return dataApi.GetNumberOfBalls();
        }

        public override Vector2 GetBallPosition(int number)
        {
            return dataApi.GetBallPosition(number);
        }

        private void BoardCollision(IBall ball)
        {
            Vector2 newVelocity = ball.Velocity;

            if (ball.Position.X <= 0 || ball.Position.X + IBall.Radius >= dataApi.Width)
            {
                newVelocity.X = -newVelocity.X;
            }

            if (ball.Position.Y <= 0 || ball.Position.Y + IBall.Radius >= dataApi.Height)
            {
                newVelocity.Y = -newVelocity.Y;
            }

            ball.Velocity = newVelocity;
        }

        private void BallsCollision(IBall ball)
        {
            for (int i = 0; i < dataApi.GetNumberOfBalls(); i++)
            {
                IBall ball2 = dataApi.GetBall(i);
                if (ball2 != ball)
                {
                    double d = Vector2.Distance(ball.Position, ball2.Position);
                    if (d - (IBall.Radius) <= 0)
                    {
                        Vector2 firstBallVelocity = CountCollisionSpeed(ball, ball2);
                        Vector2 secondBallVelocity = CountCollisionSpeed(ball2, ball);
                        if (Vector2.Distance(ball.Position, ball2.Position) > Vector2.Distance(ball.Position + firstBallVelocity, ball2.Position + secondBallVelocity))
                        {
                            return;
                        }
                        ball.Velocity = firstBallVelocity;
                        ball2.Velocity = secondBallVelocity;
                    }
                }
            }
        }
        private Vector2 CountCollisionSpeed(IBall ball, IBall ball2)
        {
            return ball.Velocity -
                   (2 * IBall.Mass / (IBall.Mass + IBall.Mass) * (Vector2.Dot(ball.Velocity - ball2.Velocity, ball.Position - ball2.Position) * (ball.Position - ball2.Position))
                    / (float)Math.Pow(Vector2.Distance(ball2.Position, ball.Position), 2));
        }

        public override event EventHandler LogicEvent;

        private readonly object collisionLock = new object();

        private void CheckCollisions(object sender, EventArgs e)
        {
            lock (collisionLock)
            {
                if (sender is IBall ball)
                {
                    BoardCollision(ball);
                    BallsCollision(ball);
                    LogicEvent?.Invoke(sender, EventArgs.Empty);
                }
            }
        }

        public override void SpawnBalls(int amount)
        {
            dataApi.SpawnBalls(amount);
            for (int i = 0; i < amount; i++)
            {
                var ball = dataApi.GetBall(i);
                Console.WriteLine($"Ball {i} initialized at position: {ball.Position} with velocity: {ball.Velocity}");
            }
        }
    }
}
