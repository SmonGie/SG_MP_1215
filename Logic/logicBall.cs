using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    internal class logicBall : AbstractLogicApi
    {
        public override int BoardHeight { get; }
        public override int BoardWidth { get; }
        public override List<AbstractBallApi> logicBalls { get; }
        private AbstractDataApi dataApi;
        public logicBall(AbstractDataApi dataApi) 
        {
            logicBalls = new List<AbstractBallApi>();
            this.dataApi =  dataApi;
            this.BoardHeight = dataApi.getHeightOfWindow();
            this.BoardWidth = dataApi.getWidthOfWindow();
        }

        public override void StartSimulation()
        {
            foreach (var ball in logicBalls)
            {
                ball.isWorking = true;
            }
        }

        public override void StopSimulation()
        {
            foreach (var ball in logicBalls)
            {
                ball.isWorking = false;
            }
        }

    }
}
