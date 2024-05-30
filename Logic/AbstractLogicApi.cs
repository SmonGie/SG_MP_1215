using Data;
using System.Numerics;

namespace Logic
{
    public abstract class AbstractLogicApi
    {
        public abstract Vector2 GetBallPosition(int number);
        public abstract int GetNumberOfBalls();
        public abstract void SpawnBalls(int amount);

        public abstract event EventHandler<BallEventArgs> LogicEvent;

        public static AbstractLogicApi CreateInstance(AbstractDataApi DataInformation)
        {
            if (DataInformation == null)
            {
                return new logicBall(AbstractDataApi.CreateInstance());
            }
            else
            {
                return new logicBall(DataInformation);
            }
        }
    }
 
}