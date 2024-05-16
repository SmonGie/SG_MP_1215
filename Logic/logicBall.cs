using Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    internal class logicBall : AbstractLogicApi
    {
        private object lockLogicBall = new object();
        public override int BoardHeight { get; }
        public override int BoardWidth { get; }
        public override List<AbstractBallApi> logicBalls { get; }
        private AbstractDataApi dataApi;
        public logicBall(AbstractDataApi dataApi) 
        {
            logicBalls = new List<AbstractBallApi>();
            this.dataApi =  dataApi;
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
            if(logicBalls.Count<=0)
            {
                ball.isWorking=true;
            }
            else
            {
                ball.isWorking = logicBalls[0].isWorking;
            }
            logicBalls.Add(ball);
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
        private bool BoardCollision(AbstractBallApi ball)
        {
            lock(lockLogicBall)
            {
                bool collided = false;
                int VelocityX = ball.VelocityX;
                int VelocityY = ball.VelocityY;
                Vector2 position = ball.Position;

                if (position.X + ball.VelocityX < 0 || position.X + ball.VelocityX >= BoardWidth)
                {
                    VelocityX = -ball.VelocityX;
                    collided = true;
                }

                if (position.Y + ball.VelocityY < 0 || position.Y + ball.VelocityY >= BoardHeight)
                {
                    VelocityY = -ball.VelocityY;
                    collided = true;
                }

                ball.setVelocity(VelocityX, VelocityY);

                return collided;
            }
        }

        private bool BallsCollision(AbstractBallApi ball1, AbstractBallApi ball2)
        {
            Vector2 Position1 = ball1.Position;
            Vector2 Position2 = ball2.Position;
            int spacing =  (int)Math.Sqrt(Math.Pow((Position1.X + ball1.VelocityX) - (Position2.X + ball2.VelocityX), 2) + Math.Pow((Position1.Y + ball1.VelocityX) - (Position2.Y + ball2.VelocityY), 2));
            if(spacing <= ball1.Radius+ ball2.Radius)
            {
                lock (lockLogicBall)
                {
                    int speedx1 = ball1.VelocityX;
                    int speedx2 = ball2.VelocityX;
                    int speedy1 = ball1.VelocityY;
                    int speedy2 = ball2.VelocityY;

                    int newspeedx1 = (speedx1 * (ball1.Mass - ball2.Mass) + 2 * ball2.Mass * speedx2) / (ball1.Mass + ball2.Mass);
                    int newspeedy1 = (speedy1 * (ball1.Mass - ball2.Mass) + 2 * ball2.Mass * speedy2) / (ball1.Mass + ball2.Mass);
                    int newspeedx2 = (speedx2 * (ball2.Mass - ball1.Mass) + 2 * ball1.Mass * speedx2) / (ball1.Mass + ball2.Mass);
                    int newspeedy2 = (speedy2 * (ball2.Mass - ball1.Mass) + 2 * ball1.Mass * speedy1) / (ball1.Mass + ball2.Mass);

                    ball1.setVelocity(speedx1, speedy1);
                    ball2.setVelocity(speedx2, speedy2);   
                }
                return true;
            }
            return false;
        }


        private void CheckCollisions(object sender, PropertyChangedEventArgs e)
        {
            if (!(sender is AbstractBallApi ball))
                return;

            BoardCollision(ball);

            foreach (var ball2 in logicBalls.Where(b => !b.Equals(ball)))
            {
                BallsCollision(ball, ball2);
            }
        }


    }
}
