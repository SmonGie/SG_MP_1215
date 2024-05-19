using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Data;

namespace Logic
{
    internal class logicBall : AbstractLogicApi
    {   
        // private static readonly SemaphoreSlim _lock = new SemaphoreSlim(1);
        private static readonly ReaderWriterLockSlim readerWriterLockSlim = new ReaderWriterLockSlim();
        public override int BoardHeight { get; }
        public override int BoardWidth { get; }
        public override List<AbstractBallApi> logicBalls { get; }
        private AbstractDataApi dataApi;

        public logicBall(AbstractDataApi dataApi)
        {
            logicBalls = new List<AbstractBallApi>();
            this.dataApi = dataApi;
            this.BoardHeight = dataApi.getHeightOfWindow();
            this.BoardWidth = dataApi.getWidthOfWindow();
        }

        public override void StartSimulation()
        {
            foreach (var ball in logicBalls)
            {
                ball.isWorking = true;
            }
        }

        public override void StopSimulation()
        {
            foreach (var ball in logicBalls)
            {
                ball.isWorking = false;
            }
        }

        public override void SpawnBalls()
        {
            AbstractBallApi ball = dataApi.SpawnBalls(true);
            if (logicBalls.Count <= 0)
            {
                ball.isWorking = true;
            }
            else
            {
                ball.isWorking = logicBalls[0].isWorking;
            }
            logicBalls.Add(ball);
            ball.AddPropertyChangedListener(CheckCollisions);
        }

        public override int GetPositionX(int id)
        {
            if (id >= 0 && id < logicBalls.Count)
            {
                return (int)logicBalls[id].Position.Y;
            }
            else
            {
                return 0;
            }
        }

        public override int GetPositionY(int id)
        {
            if (id >= 0 && id < logicBalls.Count)
            {
                return (int)logicBalls[id].Position.X;
            }
            else
            {
                return 0;
            }
        }

        private void BoardCollision(AbstractBallApi ball)
        {
            readerWriterLockSlim.EnterWriteLock();
            try
            {
                int VelocityX = ball.VelocityX;
                int VelocityY = ball.VelocityY;
                Vector2 position = ball.Position;

                // Perform collision detection
                bool positionChanged = false;
                if (position.X + ball.VelocityX < 0 || position.X + ball.VelocityX >= BoardWidth)
                {
                    VelocityX = -ball.VelocityX;
                    positionChanged = true;
                }

                if (position.Y + ball.VelocityY < 0 || position.Y + ball.VelocityY >= BoardHeight)
                {
                    VelocityY = -ball.VelocityY;
                    positionChanged = true;
                }

                // Update velocity if position changed
                if (positionChanged)
                {
                    ball.setVelocity(VelocityX, VelocityY);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred in BoardCollision: {ex.Message}");
                // Handle the exception or log it as needed
            }
            finally
            {
                readerWriterLockSlim.ExitWriteLock();
            }
        }



        private void BallsCollision(AbstractBallApi ball1, AbstractBallApi ball2)
        {
            readerWriterLockSlim.EnterWriteLock();
            try
            {
                // Calculate the distance between the centers of the balls
                float distance = Vector2.Distance(ball1.Position, ball2.Position);

                // Check if the balls are colliding
                if (distance <= ball1.Radius + ball2.Radius)
                {
                    int v1x = ball1.VelocityX;
                    int v1y = ball1.VelocityY;
                    int v2x = ball2.VelocityX;
                    int v2y = ball2.VelocityY;

                    int newV1X = (ball1.Mass * ball1.VelocityX + ball2.Mass * ball2.VelocityX - ball2.Mass * (ball1.VelocityX - ball2.VelocityX)) / (ball1.Mass + ball2.Mass);
                    int newV1Y = (ball1.Mass * ball1.VelocityY + ball2.Mass * ball2.VelocityY - ball2.Mass * (ball1.VelocityY - ball2.VelocityY)) / (ball1.Mass + ball2.Mass);
                    int newV2X = (ball1.Mass * ball1.VelocityX + ball2.Mass * ball2.VelocityX - ball1.Mass * (ball2.VelocityX - ball1.VelocityY)) / (ball1.Mass + ball2.Mass);
                    int newV2Y = (ball1.Mass * ball1.VelocityY + ball2.Mass * ball2.VelocityY - ball2.Mass * (ball2.VelocityY - ball1.VelocityY)) / (ball1.Mass + ball2.Mass);

                    ball1.setVelocity(newV1X, newV1Y);
                    ball2.setVelocity(newV2X, newV2Y);

                }
            }
            finally
            {
                readerWriterLockSlim.ExitWriteLock();
            }
        }





        private void CheckCollisions(object sender, PropertyChangedEventArgs e)
        {
            AbstractBallApi ball = (AbstractBallApi)sender;
            if (ball != null)
            {
                BoardCollision(ball);
                foreach (var ball2 in logicBalls)
                {
                    if (!ball2.Equals(ball))
                    {
                        BallsCollision(ball, ball2);
                    }
                }
            }
        }
    }
}

