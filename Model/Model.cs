using Logic;

namespace Model
{
    // Ta klasa reprezentuje konkretną implementację AbstractModelAPI i zapewnia funkcjonalność
    // śledzenie i symulowanie kuler. Spełnia rolę zarówno obserwatora dostarczającego kule, jak i obserwatora
    // dostawca BallModels dla swoich obserwatorów.
    internal class Model : AbstractModelApi
    {
        private AbstractLogicApi _logic;  // referencja do instancji AbstractLogicApi
        private ISet<IObserver<IEnumerable<IBallModel>>> _observers; // Zestaw obserwatorów śledzących BallModels
        private IDisposable? _unsubscriber; // Obiekt reprezentujący subskrypcję między Modelem a Observable

        // Konstruktor tworzący instancję Modelu z referencją do obiektu AbstractLogicApi
        public Model(AbstractLogicApi? logic = default)
        {
            _logic = logic ?? AbstractLogicApi.CreateInstance(); // Jeśli logic ma wartość null, utwórz nową instancję AbstractLogicAPI
            _observers = new HashSet<IObserver<IEnumerable<IBallModel>>>(); //Utwórz pusty zestaw hash obserwatorów 
            Subscribe(_logic); // zasubsrybuj model do observable
        }

        // Metoda to tworzenia kul przy pomocy obiektu AbstractLogicApi
        public override void SpawnBalls(int numberOfBalls)
        {
            _logic.SpawnBalls(numberOfBalls);
        }

        // Metoda do tworzenia symulacji przy pomocy przy pomocy obiektu AbstractLogicApi
        public override void Start()
        {
            _logic.Start();
        }

        // Metoda do zatrzymywania symulacji przy pomocy obiektu AbstractLogicApi
        public override void Stop()
        {
            _logic.Stop();
        }

        // Metoda do przekonwertowania obiektow Ball do obiektow BallModel 
        public static IEnumerable<IBallModel> BallToBallModel(IEnumerable<IBall> balls)
        {
            return balls.Select(ball => new BallModel(ball));
        }

        #region Observer
        
        public void Subscribe(IObservable<IEnumerable<IBall>> provider)
        {
            _unsubscriber = provider.Subscribe(this);
        }

        
        public override void OnCompleted()
        {
            _unsubscriber?.Dispose(); 
        }

       
        public override void OnError(Exception error)
        {
            throw error;
        }

        
        public override void OnNext(IEnumerable<IBall> balls)
        {
            TrackBalls(BallToBallModel(balls)); 
        }
        #endregion

        #region Provider
        
        public override IDisposable Subscribe(IObserver<IEnumerable<IBallModel>> observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }

            return new SubscriptionController(_observers, observer); 
        }

       
        private class SubscriptionController : IDisposable
        {
            private ISet<IObserver<IEnumerable<IBallModel>>> _observers;
            private IObserver<IEnumerable<IBallModel>> _observer;

            public SubscriptionController(ISet<IObserver<IEnumerable<IBallModel>>> observers,
                                    IObserver<IEnumerable<IBallModel>> observer)
            {
                _observers = observers;
                _observer = observer;
            }

            public void Dispose()
            {
                if (_observers != null)
                {
                    _observers.Remove(_observer);
                }
            }
        }

        public void TrackBalls(IEnumerable<IBallModel> balls)
        {
            
            foreach (var observer in _observers)
            {
                
                if (balls is null)
                {
                    observer.OnError(new NullReferenceException("Ball is null!"));
                }
                
                else
                {
                    observer.OnNext(balls);
                }
            }
        }

        public void CompleteTracking()
        {
            
            foreach (var observer in _observers)
            {
                
                if (_observers.Contains(observer))
                {
                    observer.OnCompleted();
                }
            }

            
            _observers.Clear();
        }

        #endregion
    }
}

