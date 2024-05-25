using Logic;
using System.Collections.ObjectModel;
using System.Numerics;

namespace Model
{
    internal class Model : AbstractModelApi
    {
        private readonly ObservableCollection<BallModel> _balls;
        public Model()
        {
            _balls = new ObservableCollection<BallModel>();
            _logicApi = AbstractLogicApi.CreateInstance(null);
            _logicApi.LogicEvent += (sender, args) => LogicApiEventHandler();
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
                Vector2 position = _logicApi.GetBallPosition(i);
                BallModel model = new BallModel(position.X, position.Y);
                _balls.Add(model);
            }
        }

        private void LogicApiEventHandler()
        {
            for (int i = 0; i < _logicApi.GetNumberOfBalls(); i++)
            {
                if (_logicApi.GetNumberOfBalls() == _balls.Count)
                {
                    Vector2 position = _logicApi.GetBallPosition(i);
                    _balls[i].X = position.X;
                    _balls[i].Y = position.Y;
                }
            }
        }

    }
}