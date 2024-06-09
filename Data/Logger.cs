using System.Collections.Concurrent;
using System.Numerics;
using System.Text.Json;

namespace Data
{
    internal class Logger
    {
        private class BallSerialization
        {
            public BallSerialization(int id, Vector2 position, string date)
            {
                X = position.X;
                Y = position.Y;
                Date = date;
                Id = id;
            }

            public float X { get; }
            public float Y { get; }
            public int Id { get; }
            public string Date { get; }
        }
        private readonly object queueLock = new object();

        ConcurrentQueue<BallSerialization> queue;
        public Logger()
        {
            queue = new ConcurrentQueue<BallSerialization>();
            Write();
        }

        private void Write()
        {
            Task.Run(async () =>
            {
                string filePath = "logger.json";
                using StreamWriter streamWriter = new StreamWriter(filePath);
                while (true)
                {
                    while (queue.TryDequeue(out BallSerialization item))
                    {
                        string jsonString = JsonSerializer.Serialize(item);
                        lock (queueLock)
                        {
                            streamWriter.WriteLine(jsonString);
                        }
                    }
                    await streamWriter.FlushAsync();
                }
            });
        }

        public void AddObjectToQueue(IBall ball, string date)
        {
            queue.Enqueue(new BallSerialization(ball.ID, ball.Position, date));
        }
    }
}
