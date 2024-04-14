using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Data;

namespace Logic
{
    internal class SimulationController : AbstractLogicApi
    {
        private ISet<IObserver<IEnumerable<Ball>>> _observers; //obiekty IObserver ktory otrzymaja aktualizacje o stanie zmian symulacji
        private AbstractDataApi _data; // Referencja do obiektu klasy AbstractDataApi ktory inicjuje dane symulacji 
        private ManageSimulation _manageSimulation; // obiekt ManageSimulation ktory zarzadza symulacja
        private bool _isWorking = false; // Sprawdza czy symulacja dziala czy nie

        public SimulationController(AbstractDataApi? data = default)  
        {
            _data = data ?? AbstractDataApi.CreateInstance(); // Jesli obiekt data nie istnieje, stworz domyslny
            _manageSimulation = new ManageSimulation(new Window(_data.WidthWindow, _data.HeightWindow), _data.BallRadius); // stworz nowa obiekt Simulationmanager z parametrami dostarczonymi przez obiekt data
            _observers = new HashSet<IObserver<IEnumerable<Ball>>>();  // zainicjuj liczbe obserwatorow
        }
        public override void SpawnBalls(int numberOfBalls)
        {
            _manageSimulation.RandomBallSpawnPosision(numberOfBalls); // losowo uwtorz liczbe kul w symulacji
        }

        internal override IEnumerable<Ball> Balls => _manageSimulation.Balls; // Zaimplementuj wlanosci kul z interfejsu AbstractLogicApi

        public override void Start() //start symulacji
        {
            if (!_isWorking)
            {
                _isWorking = true;
                Task.Run(Simulation); //Zacznij nowe zadanie ktore rozpoczyna metode symulacji asynchronicznie 
            }
        }

        public override void Stop() //koniec symulacji
        {
            _isWorking = false; //ustaw flage na nie 
        }

        public override IDisposable Subscribe(IObserver<IEnumerable<IBall>> observer) 
        {
            _observers.Add(observer); //dodaj obserwatora
            return new SubscriptionController(_observers, observer); // zwroc obiekt SubscriptionController ktroy zarzadza subskrypcja observera
        }

        public override void Simulation() 
        {
            while (_isWorking)
            {
                _manageSimulation.ballsForce(); // zastosuj sile wobec kul w symulacji
                BallTracker(Balls);
                Thread.Sleep(10); // zaczekaj przez 10 milisekund
            }
        }

        private void BallTracker(IEnumerable<Ball> balls) 
        {
            foreach (var observer in _observers) 
            {
                observer.OnNext(Balls); // Jesli kolekcja kul nie jest null, powiadamia observera wywolujac OnNext
            }
        }

        private class SubscriptionController : IDisposable 
        {
            private readonly ISet<IObserver<IEnumerable<Ball>>> _observers; //lista observatoruj ktorzy sa zasubsrybowani do symulacji
            private readonly IObserver<IEnumerable< Ball>> _observer; // referencja do observera zarzadzanego przez SubsriptionControllera

            public SubscriptionController(ISet<IObserver<IEnumerable<Ball>>> observers, IObserver<IEnumerable<Ball>> observer)
            {
                _observers = observers; // Ustaw set observerow
                _observer = observer; //ustaw observera jako zarzadzanego
            }

            public void Dispose() 
            {
                _observers.Remove(_observer); //usun zarzadzanie observera z kolecji observeruj 
            }
        }
    }
}
