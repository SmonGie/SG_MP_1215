using Logic;
using System.Collections.ObjectModel;

namespace Model
{
    public abstract class AbstractModelApi
    {
        public AbstractLogicApi _logicApi;
        public abstract void SpawnBall(int number);
        public abstract ObservableCollection<BallModel> Balls();

        public static AbstractModelApi CreateInstance()
        {
            return new Model();
        }
    }
}
