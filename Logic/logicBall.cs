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
        public override int BoardHeight { get; }
        public override int BoardWidth { get; }

        public override List<IBall> logicBalls { get; }
        private AbstractDataApi dataApi;

        public logicBall(AbstractDataApi dataApi)
        {
            logicBalls = new List<IBall>();
            this.dataApi = dataApi;
            this.BoardHeight = dataApi.getHeightOfWindow();
            this.BoardWidth = dataApi.getWidthOfWindow();
        }



        private void BoardCollision(IBall ball)
        {
            
                Vector2 velocity = ball.Velocity;

                if (ball.Position.X  <= 0)
                {
                    velocity.X = ball.Velocity.X;
                    
                }

                if (ball.Position.Y <= 0)
                {
                    velocity.Y = -ball.Velocity.Y;
                }

                if (ball.Position.X + ball.Radius >= dataApi.getWidthOfWindow())
                {
                    velocity.X = -ball.Velocity.X;
                }
                if (ball.Position.Y + ball.Radius > dataApi.getHeightOfWindow())
                {
                    velocity.Y = -ball.Velocity.Y;
                }

            ball.Velocity = velocity;


        }



        private void BallsCollision(IBall ball1)
        {

            lock (collisionLock)
            {
                for (int i = 0; i < dataApi.GetNumberOfBalls(); i++)
                {
                    IBall ball2 = dataApi.GetBall(i);
                    if (ball2 != ball1)
                    {

                        // Calculate the distance between the centers of the balls
                        float distance = Vector2.Distance(ball1.Position, ball2.Position);

                        // Check if the balls are colliding
                        if (distance <= ball1.Radius + ball2.Radius)
                        {
                            Vector2 v1 = ball1.Velocity;
                            Vector2 v2 = ball2.Velocity;
                            Vector2 p1 = ball1.Position;
                            Vector2 p2 = ball2.Position;
                            float m1 = ball1.Mass;
                            float m2 = ball2.Mass;

                            Vector2 deltaP = p1 - p2;
                            float d = deltaP.Length();
                            Vector2 deltaV = v1 - v2;

                            float a = Vector2.Dot(deltaV, deltaP) / (d * d);
                            Vector2 impact = a * deltaP;

                            ball1.Velocity -= impact * (2 * m2 / (m1 + m2));
                            ball2.Velocity += impact * (2 * m1 / (m1 + m2));

                        }
                    }
                }


            }
              
        }


        public override event EventHandler LogicEvent;

        private readonly object collisionLock = new object();

        private void CheckCollisions(Object sender, EventArgs e)
        {
            IBall ball = (IBall)sender;
            
                lock (collisionLock)
                {
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

