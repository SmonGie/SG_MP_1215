using Logic;
using Data;
using System.Collections.ObjectModel;

namespace Model
{
    internal class Model : AbstractModelApi
    {
        private AbstractLogicApi _logicApi;

        public Model(AbstractLogicApi logicApi)
        {
            _logicApi = logicApi;
        }

        public override ObservableCollection<object>? GetBalls()
        {
            ObservableCollection<object>? _balls = new ObservableCollection<object>();

            foreach (object ball in _logicApi.logicBalls)
            {
                _balls.Add(ball);
            }

            return _balls;
        }

        public override void SpawnBall()
        {
            _logicApi.SpawnBalls();
        }

    }
}