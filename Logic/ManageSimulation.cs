using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    internal class ManageSimulation
    {
        private Window window;
        private int Radius;
        private int Diameter;
        private const float maxVelocity = 40;
        private Random random = new Random();

        public IList<Ball> Balls { get; private set; }

        public ManageSimulation(Window window, int Diameter)
        {
            this.window = window;
            this.Diameter = Diameter;
            Balls = new List<Ball>();
            this.Radius = Diameter / 2;
        }

        public void ballsForce(float force = 0.05f)
        {
            foreach( var ball in Balls)
            {
                ball.Move(window.GetXBoundry, window.GetYBoundry, force);
            }
        }
        private Vector2 RandomBallPosision()
        {
            int x = random.Next(Radius, window.Width - Radius);
            int y = random.Next(Radius, window.Height - Radius);
            return new Vector2(x, y);
        }
    
        private Vector2 RandomBallVelocity()
        {
            const float halfVelocity = maxVelocity / 2;
            double x = random.NextDouble() * maxVelocity - halfVelocity;
            double y = random.NextDouble() * maxVelocity - halfVelocity;

            return new Vector2(x, y);    
        }

        public IList<Ball> RandomBallSpawnPosision(int ballNumber)
        {
            Balls = new List<Ball>(ballNumber);

            for(int i = 0; i < ballNumber; i++)
            {
                Vector2 position = RandomBallPosision();
                Vector2 velocity = RandomBallVelocity(); 

                Balls.Add(new Ball(velocity, position, Diameter));
            }
            return Balls;
        }
    }
}
