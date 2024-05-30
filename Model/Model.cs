using Data;
using Logic;
using System.Collections.ObjectModel;
using System.Numerics;

namespace Model
{
    internal class Model : AbstractModelApi
    {
        private readonly ObservableCollection<BallModel> _balls;
        private readonly object _ballsLock = new object(); // Lock object for thread safety
        public Model()
        {
            _balls = new ObservableCollection<BallModel>(); // Inicjowanie kolekcji kuli
            _logicApi = AbstractLogicApi.CreateInstance(null); // Tworzenie instancji obiektu logiki gry
            _logicApi.LogicEvent += LogicApiEventHandler; // Subskrypcja zdarzenia logiki gry
        }

        public override ObservableCollection<BallModel> Balls()
        {
            return _balls;
        }

        public override void SpawnBall(int number)
        {
            _logicApi.SpawnBalls(number);
            for (int i = 0; i < number; i++)
            {
                Vector2 position = _logicApi.GetBallPosition(i); // Pobranie pozycji kuli z logiki
                BallModel model = new BallModel(position.X, position.Y);
                _balls.Add(model);
            }
        }

        private void LogicApiEventHandler(object sender, BallEventArgs args)
        {
            int ballIndex = args.BallIndex;
            lock (_ballsLock)
            {
                if (ballIndex >= 0 && ballIndex < _balls.Count)
                {
                    // Pobranie pozycji kuli z logiki
                    Vector2 position = _logicApi.GetBallPosition(ballIndex);
                    // Aktualizacja pozycji X i Y kuli 
                    _balls[ballIndex].X = position.X; // Updating X position
                    _balls[ballIndex].Y = position.Y; // Updating Y position
                }
            }
        }
    }
}

