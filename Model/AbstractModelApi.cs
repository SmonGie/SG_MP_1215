
﻿using Logic;
using Data;
using System.Collections.ObjectModel;

namespace Model
{
    public abstract class AbstractModelApi
    {
        public abstract void SpawnBall();
        public abstract ObservableCollection<object>? GetBalls();

        public static AbstractModelApi CreateInstance(int windowHeight, int windowWidth, AbstractLogicApi logicApi)
        {
            if (logicApi == null)
            {
                return new Model(AbstractLogicApi.CreateInstance(windowWidth, windowHeight, null));
            }
            else
            {
                return new Model(logicApi);
            }
        }
    }
}
