using System.Numerics;
using Data;

namespace Logic
{
    internal class logicBall : AbstractLogicApi
    {   
        private AbstractDataApi dataApi;

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
            Vector2 newSpeed = ball.Velocity;
            if (ball.Position.X <= 0)
            {
                newSpeed.X = Math.Abs(ball.Velocity.X);
            }
            if (ball.Position.Y <= 0)
            {
                newSpeed.Y = Math.Abs(ball.Velocity.Y);
            }

            if (ball.Position.X + IBall.Radius >= dataApi.Width)
            {
                newSpeed.X = -Math.Abs(ball.Velocity.X);
            }
            if (ball.Position.Y + IBall.Radius > dataApi.Height)
            {
                newSpeed.Y = -Math.Abs(ball.Velocity.Y);
            }
            ball.Velocity = newSpeed;
        }



        private void BallsCollision(IBall ball)
        {
            for (int i = 0; i < dataApi.GetNumberOfBalls(); i++)
            {
                IBall ball2 = dataApi.GetBall(i);
                if (ball2 != ball)
                {
                    double distancex = ball2.Position.X - ball.Position.X;
                    double distancey = ball2.Position.Y - ball.Position.Y;
                    double distance = Math.Sqrt(distancex * distancex + distancey * distancey);
                    if (distance <= IBall.Radius+IBall.Radius)
                    {
                        Vector2 collisionNormal = Vector2.Normalize(ball.Position -ball2.Position);
                        Vector2 relativeVelocity = ball.Velocity - ball2.Velocity;

                        float velocityAlongNormal = Vector2.Dot(relativeVelocity, collisionNormal);

                        if (velocityAlongNormal > 0)
                            return;

                        float impulseScalar = -(2 * velocityAlongNormal) / (IBall.Mass + IBall.Mass);
                        Vector2 impulse = impulseScalar * collisionNormal;

                        ball.Velocity -= impulse * (IBall.Mass / (IBall.Mass + IBall.Mass));
                        ball2.Velocity += impulse * (IBall.Mass / (IBall.Mass + IBall.Mass));
                    }
                }
            }
        }


        public override event EventHandler LogicEvent;

        private readonly object collisionLock = new object();

        private void CheckCollisions(Object sender, EventArgs e)
        {
            lock (collisionLock)
            {
                IBall ball = (IBall)sender;


                if (sender != null)
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
        }


    }
}

