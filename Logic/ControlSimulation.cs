using Data;

namespace Logic
{
    internal class SimulationController : AbstractLogicApi
    {
        private ISet<IObserver<IEnumerable<Ball>>> _observers = new HashSet<IObserver<IEnumerable<Ball>>>(); //obiekty IObserver ktory otrzymaja aktualizacje o stanie zmian symulacji
        private AbstractDataApi _data; // Referencja do obiektu klasy AbstractDataApi ktory inicjuje dane symulacji 
        private ManageSimulation _manageSimulation; // obiekt ManageSimulation ktory zarzadza symulacja
        private bool _isWorking = false; // Sprawdza czy symulacja dziala czy nie
        private Thread _simulationThread;
        public SimulationController(AbstractDataApi? data = default)  
        {
            _data = data ?? AbstractDataApi.CreateInstance(); // Jesli obiekt data nie istnieje, stworz domyslny
            _manageSimulation = new ManageSimulation(new Window(_data.WidthWindow, _data.HeightWindow), _data.BallRadius); // stworz nowa obiekt Simulationmanager z parametrami dostarczonymi przez obiekt data
        }
        public override void SpawnBalls(int numberOfBalls)
        {
            _manageSimulation.RandomBallSpawnPosition(numberOfBalls); // losowo uwtorz liczbe kul w symulacji
        }

        internal override IEnumerable<Ball> Balls => _manageSimulation.Balls; // Zaimplementuj wlanosci kul z interfejsu AbstractLogicApi
        
        public override void Start() //start symulacji
        {
            if (!_isWorking)
            {
                _isWorking = true;
                _simulationThread = new Thread(Simulation);
                _simulationThread.Start();
            }
        }

        public override void Stop() //koniec symulacji
        {
            _isWorking = false; //ustaw flage na nie 
        }

        public override void Simulation() 
        {
            while (_isWorking)
            {
                _manageSimulation.ballsForce(); // zastosuj sile wobec kul w symulacji
                BallTracker(Balls);
                Thread.Sleep(15); // zaczekaj przez 10 milisekund
            }
        }

        private void BallTracker(IEnumerable<Ball> balls) 
        {
            foreach (var observer in _observers) 
            {
                observer.OnNext(Balls); // Jesli kolekcja kul nie jest null, powiadamia observera wywolujac OnNext
            }
        }

        public override IDisposable Subscribe(IObserver<IEnumerable<IBall>> observer)
        {
            var subscription = new SubscriptionController(_observers, observer);
            _observers.Add(observer);
            return subscription;
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
