namespace Logic
{
    internal class ManageSimulation
    {
        private Window window; //Okno w ktorym miejsce ma symulacja
        private int Radius; // promien kuli
        private int Diameter; // srednica kuli
        private const float maxVelocity = 60; // maksymalna predkosc kuli
        private Random random = new Random(); // inicjalizuje generator losowosci

        public IList<Ball> Balls { get; private set; } //Lista kul w symulacji

        public ManageSimulation(Window window, int Diameter)
        {
            this.window = window; // przechowuj dane o oknie w ktorym miejsce ma symulacji
            this.Diameter = Diameter; // przechowuj sredice kuli
            Balls = new List<Ball>(); // Stworz liste do przechowywania kul
            this.Radius = Diameter / 2; // przechowuj promien kuli
        }

        public void ballsForce(float force = 0.1f) //uzyj sily do kazdej kuli w symulacji
        {
            foreach( var ball in Balls)
            {
                ball.Move(window.GetXBoundry, window.GetYBoundry, force);
            }
        }
        private Vector2 RandomBallPosition() 
        {
            int x = random.Next(Radius, window.Width - Radius); //wygeneruj losowo pozycje(x) kuli
            int y = random.Next(Radius, window.Height - Radius); ////wygeneruj losowo pozycje(y) kuli
            return new Vector2(x, y);  //zwroc wektor polozenie
        }
    
        private Vector2 RandomBallVelocity()
        {
            const float halfVelocity = maxVelocity / 2;
            double x = random.NextDouble() * maxVelocity - halfVelocity; //wygeneruj losowo przyspieszenie(x) kuli
            double y = random.NextDouble() * maxVelocity - halfVelocity; //wygeneruj losowo przyspieszenie(x) kuli

            return new Vector2(x, y);    //zwroc wektor przyspieszenia
        }

        public IList<Ball> RandomBallSpawnPosition(int ballNumber) 
        {
            Balls.Clear();
            for (int i = 0; i < ballNumber; i++)
            {
                try
                {
                    Vector2 position = RandomBallPosition();
                    Vector2 velocity = RandomBallVelocity();
                    Balls.Add(new Ball(velocity, position, Diameter));
                }
                catch (Exception ex)
                {
                    // Obsługa błędów związanych z generowaniem pozycji i prędkości kuli
                    Console.WriteLine($"Error generating ball: {ex.Message}");
                }
            }
            return Balls;
        }
    }
}
