using Data;

namespace Logic
{
    public abstract class AbstractLogicApi
    {
        public abstract int BoardWidth {  get; }
        public abstract int BoardHeight { get; }
        public abstract int GetPositionX(int id);
        public abstract int GetPositionY(int id);
        public abstract void StartSimulation();
        public abstract void StopSimulation();
        public abstract void SpawnBalls();
       

        public abstract List<AbstractBallApi> logicBalls { get; }
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