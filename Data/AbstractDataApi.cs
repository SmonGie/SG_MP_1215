using System.Numerics;

namespace Data
{
    // Definicja klasy AbstractDataApi jako abstrakcyjnej
    public abstract class AbstractDataApi
    {
        // Definicja abstrakcyjnych elementow dla wysokosci i szerokosci okna
        public abstract int Width { get; }
        public abstract int Height { get; }

        public abstract event EventHandler<BallEventArgs> BallEvent;
        public abstract void SpawnBalls(int amount);
        public abstract IBall GetBall(int number);

        public abstract Vector2 GetBallPosition(int number);

        // Definicja metody statycznnej, aby utworzyć wystąpienie klasy AbstractDataAPI.
        public static AbstractDataApi CreateInstance()
        {
            // Zwraca nową instancję klasy Data.
            return new Data();
        }
        public abstract int GetNumberOfBalls();
    }
}
