using System.Diagnostics;
using System.Numerics;

namespace Data
{
    public interface IBall
    {

        Vector2 Position { get; }
        Vector2 Velocity { get; set; }
        const int Radius = 50;
        int ID { get; }

    }
    internal class Ball : IBall
    {

        public int ID { get; }
        private Vector2 _position;
        private Vector2 _velocity;
        private readonly int Break = 5;
        private readonly object movelock = new object(); // Obiekt do blokowania sekcji krytycznej
        private readonly object velocitylock = new object(); // Obiekt do blokowania sekcji krytycznej
        public event EventHandler<Tuple<Vector2, int, DateTime>> PositionChange;
        // Konstruktor inicjalizujący pozycję i prędkość piłki
        public Ball(int x, int y, int id)
        {
            ID = id;
            Random random = new Random();
            _position = new Vector2(x, y);

            _velocity = new Vector2(random.Next(-4, 4), random.Next(-4, 4)); // Inicjalizacja prędkości losowymi wartościami
            while (_velocity.X == 0 || _velocity.Y == 0) 
            {
                _velocity = new Vector2(random.Next(-4, 4), random.Next(-4, 4));
            }

            MoveBall(); // Rozpoczęcie ruchu piłki
        }


        // Zdarzenie wywoływane przy zmianie pozycji

        internal void OnPositionChange()
        {
            PositionChange?.Invoke(this, new Tuple<Vector2, int, DateTime>(Position, ID, DateTime.Now));
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

        public void move(int Time)
        {       
                Position += Velocity * Time * 0.1f; // Aktualizacja pozycji
                OnPositionChange(); // Wywołanie zdarzenia zmiany pozycji       
        }


        // Metoda uruchamiająca ruch piłki w osobnym wątku
        private void MoveBall()
        {
            Task.Run(async () =>
            {
                Stopwatch stopwatch = new Stopwatch(); // Inicjalizacja stopera
                stopwatch.Start(); // Startowanie stopera
                long lastUpdateTime = stopwatch.ElapsedMilliseconds; // Inicjalizacja czasu ostatniej aktualizacji
                int wait; // Zmienna do przechowywania czasu oczekiwania
                while (true)
                {

                    long currentTime = stopwatch.ElapsedMilliseconds; // Pobranie bieżącego czasu
                    int elapsedTime = (int)(currentTime - lastUpdateTime); // Obliczenie upływu czasu

                    move(elapsedTime); // Wykonanie ruchu piłki

                    lastUpdateTime = currentTime; // Aktualizacja czasu ostatniej aktualizacji
                    long howLongToWait = currentTime + Break; // Obliczenie czasu oczekiwania
                    // Ustawienie czasu oczekiwania w zależności od aktualnego czasu
                    if (stopwatch.ElapsedMilliseconds < howLongToWait)
                    {
                        wait = Break;
                    }
                    else
                    {
                        wait = 0;
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
