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
            Console.WriteLine("board locked");
            try
            {
                int VelocityX = ball.VelocityX;
                int VelocityY = ball.VelocityY;
                Vector2 position = ball.Position;
                Console.WriteLine($"Position before collision check: X={position.X}, Y={position.Y}");

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
                    Console.WriteLine("Velocity updated after collision check");
                }
                else
                {
                    Console.WriteLine("No collision detected. Velocity remains unchanged.");
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
                Console.WriteLine("board Unlocked");
            }
        }



        private void BallsCollision(AbstractBallApi ball1, AbstractBallApi ball2)
        {
            readerWriterLockSlim.EnterWriteLock();
            try
            {
                Console.WriteLine("Ball collision started");

                float distance = Vector2.Distance(ball1.Position, ball2.Position);

                if (distance <= ball1.Radius + ball2.Radius)
                {
                    Vector2 direction = Vector2.Normalize(ball2.Position - ball1.Position);

                    Vector2 relativeVelocity = new Vector2(ball2.VelocityX - ball1.VelocityX, ball2.VelocityY - ball1.VelocityY);
                    float relativeSpeed = Vector2.Dot(relativeVelocity, direction);

                    if (relativeSpeed > 0)
                        return;

                    float impulseMagnitude = -2 * relativeSpeed / (1 / ball1.Mass + 1 / ball2.Mass);
                    Vector2 impulse = impulseMagnitude * direction;

                    ball1.VelocityX -= (int)(impulse.X / ball1.Mass);
                    ball1.VelocityY -= (int)(impulse.Y / ball1.Mass);
                    ball2.VelocityX += (int)(impulse.X / ball2.Mass);
                    ball2.VelocityY += (int)(impulse.Y / ball2.Mass);
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
                Console.WriteLine("board collision check started");
                BoardCollision(ball);
                foreach (var ball2 in logicBalls)
                {
                    if (!ball2.Equals(ball))
                    {
                        Console.WriteLine("ball collision check started");
                        BallsCollision(ball, ball2);
                    }
                }
            }
        }
    }
}

