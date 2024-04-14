using Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public abstract class AbstractLogicApi : IObservable<IEnumerable<IBall>>
    {
        internal abstract IEnumerable<Ball> Balls { get; }
        public abstract void Simulation();
        public abstract void Start();
        public abstract void Stop();
        public abstract void SpawnBalls(int ballNumber);
        public abstract IDisposable Subscribe(IObserver<IEnumerable<IBall>> observer);
        public static AbstractLogicApi CreateInstance(AbstractDataApi? data = default)
        {
            // Return a new instance of the SimulationController class, passing in the provided AbstractDataAPI instance or creating a new instance of AbstractDataAPI if no instance is provided.
            return new SimulationController(data ?? AbstractDataApi.CreateInstance());
        }
        public interface IBall
        {
            Vector2 Velocity { get; }
            Vector2 Position { get; }
            int Radius { get; }
            int Diameter { get; }
        }
    }
}