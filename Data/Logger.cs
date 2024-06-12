using System.Collections.Concurrent;
using System.Numerics;
using System.Text.Json;

namespace Data
{
    internal class Logger
    {
        private readonly int buffer = 100;
        // Klasa reprezentująca serializowane dane piłki
        private class BallSerialization
        {
            public BallSerialization(int id, Vector2 position, DateTime date)
            {
                X = position.X;
                Y = position.Y;
                Date = date;
                Id = id;
            }
            public float X { get; } // Współrzędna X piłki
            public float Y { get; }
            public int Id { get; } // Unikalny identyfikator piłki
            public DateTime Date { get; } // Data logowania danych
        }

        ConcurrentQueue<BallSerialization> queue; // Kolejka równoległa do przechowywania serializowanych danych piłki
        public Logger()
        {
            queue = new ConcurrentQueue<BallSerialization>(); // Inicjalizacja kolejki równoległej
            Write(); // Rozpoczęcie zapisu danych do pliku
        }
        // Metoda asynchroniczna do zapisu danych z kolejki do pliku
        private void Write()
        {
            Task.Run(async () =>
            {
                string filePath = "logger.json"; // Ścieżka do pliku dziennika
                using StreamWriter streamWriter = new StreamWriter(filePath); // Utworzenie obiektu do zapisu do pliku
                while (true)
                {
                    while (queue.TryDequeue(out BallSerialization item)) // Pobieranie elementów z kolejki
                    {
                        string jsonString = JsonSerializer.Serialize(item); // Serializacja danych piłki do formatu JSON
                        streamWriter.WriteLine(jsonString); // Zapisanie ciągu JSON do pliku
                        
                    }
                    await streamWriter.FlushAsync(); // Wypłukanie obiektu strumienia w celu zapewnienia zapisu danych do pliku
                }
            });
        }
        // Metoda dodająca obiekt piłki do kolejki w celu zalogowania
        public void AddObjectToQueue(IBall ball, DateTime date)
        {
            lock (queue)
            {
                if (queue.Count < buffer)
                {
                    queue.Enqueue(new BallSerialization(ball.ID, ball.Position, date)); // Dodanie serializowanych danych piłki do kolejki
                }
                else
                {
                    return;
                }
            }
        }
    }
}
