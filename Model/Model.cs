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
        // Method to subscribe the Model to an IObservable<IEnumerable<Ball>> object
        public void Subscribe(IObservable<IEnumerable<IBall>> provider)
        {
            _unsubscriber = provider.Subscribe(this); // Subscribe the Model to the provider
        }

        // Method called when the Observable is completed
        public override void OnCompleted()
        {
            _unsubscriber?.Dispose(); // Dispose the subscription object
        }

        // Method called when the Observable encounters an error
        public override void OnError(Exception error)
        {
            throw error;
        }

        // Method called when the Observable sends a collection of Ball objects
        public override void OnNext(IEnumerable<IBall> balls)
        {
            TrackBalls(BallToBallModel(balls)); // Convert the Ball objects to BallModel objects and track them
        }
        #endregion

        #region Provider
        // Method to subscribe an IObserver<IEnumerable<BallModel>> object to the Model
        public override IDisposable Subscribe(IObserver<IEnumerable<IBallModel>> observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer); // Add the observer to the set of observers
            }

            return new SubscriptionController(_observers, observer); // Return a SubscriptionController object that represents the subscription
        }

        // Class that controls the subscription between the Model and its observers
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
            // Iterate through all subscribed observers
            foreach (var observer in _observers)
            {
                // If the collection of balls is null, throw an exception
                if (balls is null)
                {
                    observer.OnError(new NullReferenceException("Ball is null!"));
                }
                // Otherwise, call the observer's OnNext method with the collection of balls
                else
                {
                    observer.OnNext(balls);
                }
            }
        }

        public void CompleteTracking()
        {
            // Iterate through all subscribed observers
            foreach (var observer in _observers)
            {
                // If the current observer is in the set of observers, call its OnCompleted method
                if (_observers.Contains(observer))
                {
                    observer.OnCompleted();
                }
            }

            // Clear the set of observers
            _observers.Clear();
        }

        #endregion
    }
}

