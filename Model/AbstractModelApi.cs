
﻿using Logic;
using Data;
using System.Collections.ObjectModel;

namespace Model
{
    public abstract class AbstractModelApi
    {
        public abstract void StartSimulation();
        public abstract void StopSimulation();
        public abstract void SpawnBall();
        public abstract ObservableCollection<object> GetBalls();

        public static AbstractModelApi CreateInstance(int windowHeight, int windowWidth, AbstractBallApi logicAPI)
        {
            if (logicAPI == null)
            {
                return new Model(AbstractBallApi.CreateInstance(windowHeight, windowWidth, null));
            }
            else
            {
                return new Model(logicAPI);
            }
        }
    }
}
