using System.Diagnostics;
using System.Numerics;

namespace Data
{
    public interface IBall
    {

        Vector2 Position { get; }
        Vector2 Velocity { get; set; }
        const int Radius = 50;
        const int Mass = 5;

    }
    internal class Ball : IBall
    {

        
        private Vector2 _position;
        private Vector2 _velocity;
        private int _MovingTime; // Pole przechowujące czas ruchu

        private Stopwatch stopwatch = new Stopwatch(); // Stoper do mierzenia czasu

        private readonly object movelock = new object(); // Obiekt do blokowania sekcji krytycznej
        private readonly object velocitylock = new object(); // Obiekt do blokowania sekcji krytycznej

        // Konstruktor inicjalizujący pozycję i prędkość piłki
        public Ball(int x, int y)
        {
            Random random = new Random();
            _position = new Vector2(x, y);

            _velocity = new Vector2(random.Next(-4, 4), random.Next(-4, 4)); // Inicjalizacja prędkości losowymi wartościami
            while (_velocity.X == 0 || _velocity.Y == 0) 
            {
                _velocity = new Vector2(random.Next(-4, 4), random.Next(-4, 4));
            }

            MovingTime = 1; // Inicjalizacja czasu ruchu

            MoveBall(); // Rozpoczęcie ruchu piłki
        }





        // Zdarzenie wywoływane przy zmianie pozycji
        public event EventHandler PositionChange;

        internal void OnPositionChange()
        {
            PositionChange?.Invoke(this, EventArgs.Empty);
        }
       

        public Vector2 Position
        {
            get //get jest używana do odczytu wartości pola _position
            {
                lock (movelock) //Blokada movelock zapewnia, że dostęp do pola _position jest synchronizowany. 
                {
                    return _position; //metoda zwraca wartość _position
                }
            }
            private set
            {
                lock (movelock) //tylko jeden wątek może zapisywać wartość _position
                {
                    _position = value; //metoda ustawia wartość _position na przekazaną wartość
                }
            }
        }

        public Vector2 Velocity
        {
            get
            {
                lock (velocitylock)
                {
                    return _velocity;
                }
            }
            set
            {
                lock (velocitylock)
                {
                    _velocity = value;
                }
            }
        }

        public void move()
        {       
                Position += Velocity * MovingTime*0.1f; // Aktualizacja pozycji
                OnPositionChange(); // Wywołanie zdarzenia zmiany pozycji       
        }

        private int MovingTime
        {
            get => _MovingTime;
            set
            {
                _MovingTime = value;
            }
        }

        // Metoda uruchamiająca ruch piłki w osobnym wątku
        private void MoveBall()
        {
            Task.Run(async () =>
            {
                int wait = 0;
                while (true)
                {
                    stopwatch.Restart(); // Restartowanie stopera
                    stopwatch.Start(); // Startowanie stopera

                    move(); // Wykonanie ruchu piłki

                    stopwatch.Stop(); // Zatrzymanie stopera

                    // Obliczanie czasu oczekiwania
                    if (MovingTime - stopwatch.ElapsedMilliseconds < 0)
                    {
                        wait = 0;
                    }
                    else
                    {
                        wait = MovingTime - (int)stopwatch.ElapsedMilliseconds;
                    }

                    await Task.Delay(wait); // Oczekiwanie przez wyliczony czas
                }
            });
        }
    }

    public class BallEventArgs : EventArgs
    {
        public int BallIndex { get; } // Indeks piłki

        public BallEventArgs(int ballIndex) // Konstruktor inicjalizujący indeks piłki
        {
            BallIndex = ballIndex;
        }
    }
}
