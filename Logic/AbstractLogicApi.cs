using Data;

namespace Logic
{
    public abstract class AbstractLogicApi
    {
        public abstract int BoardWidth {  get; }
        public abstract int BoardHeight { get; }

       
        public abstract void SpawnBalls(int amount);

        public abstract event EventHandler LogicEvent;

        public abstract List<IBall> logicBalls { get; }

        public static AbstractLogicApi CreateInstance(int boardWidth, int boardHeight, AbstractDataApi DataInformation)
        {
            if (DataInformation == null)
            {
                return new logicBall(AbstractDataApi.CreateInstance(boardHeight, boardWidth));
            }
            else
            {
                return new logicBall(DataInformation);
            }
        }
    }
 
}