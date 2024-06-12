using System;
using System.Numerics;
using Data;

namespace Logic
{
    internal class logicBall : AbstractLogicApi
    {
        private readonly AbstractDataApi dataApi; // Pole przechowujące instancję API danych

        private const int mass = 5;
        // Konstruktor klasy logicBall, który przyjmuje API danych i subskrybuje zdarzenie BallEvent
        public logicBall(AbstractDataApi dataApi)
        {
            dataApi.BallEvent += CheckCollisions; //metoda CheckCollisions zostanie wykonana za każdym razem, gdy dataApi wywoła zdarzenie BallEvent
            this.dataApi = dataApi; // Inicjalizacja pola dataApi
        }

        public override int GetNumberOfBalls()
        {
            return dataApi.GetNumberOfBalls();
        }

        // Metoda zwracająca pozycję piłki na podstawie jej numeru w liście
        public override Vector2 GetBallPosition(int number)
        {
            return dataApi.GetBallPosition(number);
        }

        // Metoda sprawdzająca kolizje piłki ze ścianami planszy
        private void BoardCollision(IBall ball)
        {
            Vector2 newVelocity = ball.Velocity;

            // Sprawdzenie kolizji ze ścianami pionowymi
            if (ball.Position.X <= 0 || ball.Position.X + IBall.Radius >= dataApi.Width)
            {
                newVelocity.X = -newVelocity.X;  // Odwrócenie kierunku prędkości w osi X
            }

            if (ball.Position.Y <= 0 || ball.Position.Y + IBall.Radius >= dataApi.Height)
            {
                newVelocity.Y = -newVelocity.Y;
            }

            ball.Velocity = newVelocity; // Aktualizacja prędkości piłki
        }

        private void BallsCollision(IBall ball)
        {
            for (int i = 0; i < dataApi.GetNumberOfBalls(); i++)
            {
                IBall ball2 = dataApi.GetBall(i);
                if (ball2 != ball)
                {
                    double ballDistance = Vector2.Distance(ball.Position, ball2.Position);
                    if (0 >= ballDistance - (IBall.Radius))
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
        // Metoda obliczająca nową prędkość piłki po kolizji
        private Vector2 CountCollisionSpeed(IBall ball, IBall ball2)
        {
            Vector2 relativeVelocity = ball.Velocity - ball2.Velocity; // Obliczenie względnej prędkości piłek
            Vector2 relativePosition = ball.Position - ball2.Position; // Obliczenie względnej pozycji piłek
            float distanceSquared = relativePosition.LengthSquared(); // Obliczenie odległości między piłkami

            // Obliczenie zmiany prędkości na skutek kolizji
            Vector2 velocityChange = (2 * mass / (mass + mass)) *
                                     (Vector2.Dot(relativeVelocity, relativePosition) / distanceSquared) *
                                     relativePosition;

            return ball.Velocity - velocityChange;
        }

        public override event EventHandler<BallEventArgs> LogicEvent;

        private readonly object collisionLock = new object(); // Obiekt do blokowania sekcji krytycznej

        private void CheckCollisions(object sender, BallEventArgs e)
        {
            lock (collisionLock)
            {             
                    IBall movingball = dataApi.GetBall(e.BallIndex);
                    BoardCollision(movingball); // Sprawdzenie kolizji ze ścianami
                    BallsCollision(movingball); // Sprawdzenie kolizji z innymi piłkami
                    LogicEvent?.Invoke(this, new BallEventArgs(e.BallIndex));                
            }
        }

        public override void SpawnBalls(int amount)
        {
            dataApi.SpawnBalls(amount); // Wywołanie metody API danych do tworzenia piłek
            for (int i = 0; i < amount; i++)
            {
                _ = dataApi.GetBall(i); // Pobranie każdej nowej piłki
            }
        }
    }
}
