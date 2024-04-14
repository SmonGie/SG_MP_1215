using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Data;

namespace Logic
{
    internal class SimulationController : AbstractLogicApi
    {
        private ISet<IObserver<IEnumerable<Ball>>> _observers;
        private AbstractDataApi _data;
        private ManageSimulation _manageSimulation;
        private bool _isWorking = false;

        public SimulationController(AbstractDataApi? data = default)
        {
            _data = data ?? AbstractDataApi.CreateInstance();
            _manageSimulation = new ManageSimulation(new Window(_data.WidthWindow, _data.HeightWindow), _data.BallRadius);
            _observers = new HashSet<IObserver<IEnumerable<Ball>>>();
        }
        public override void SpawnBalls(int numberOfBalls)
        {
            _manageSimulation.RandomBallSpawnPosision(numberOfBalls); // Spawn a number of balls randomly in the simulation.
        }

        internal override IEnumerable<Ball> Balls => _manageSimulation.Balls;

        public override void Start()
        {
            if (!_isWorking)
            {
                _isWorking = true;
                Task.Run(Simulation);
            }
        }

        public override void Stop()
        {
            _isWorking = false;
        }

        public override IDisposable Subscribe(IObserver<IEnumerable<IBall>> observer)
        {
            _observers.Add(observer);
            return new SubscriptionController(_observers, observer);
        }

        public override void Simulation()
        {
            while (_isWorking)
            {
                _manageSimulation.ballsForce();
                BallTracker(Balls);
                Thread.Sleep(10); // Using Task.Delay instead of Thread.Sleep to avoid blocking the thread.
            }
        }

        private void BallTracker(IEnumerable<Ball> balls)
        {
            foreach (var observer in _observers)
            {
                observer.OnNext(Balls);
            }
        }

        private class SubscriptionController : IDisposable
        {
            private readonly ISet<IObserver<IEnumerable<Ball>>> _observers;
            private readonly IObserver<IEnumerable< Ball>> _observer;

            public SubscriptionController(ISet<IObserver<IEnumerable<Ball>>> observers, IObserver<IEnumerable<Ball>> observer)
            {
                _observers = observers;
                _observer = observer;
            }

            public void Dispose()
            {
                _observers.Remove(_observer);
            }
        }
    }
}
