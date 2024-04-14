using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace ViewModel
{
    internal class SimViewModel : ViewModel, IObserver<IEnumerable<IBallModel>>
    {
        private IDisposable? _unsubscriber;
        private ObservableCollection<IBallModel> _balls;
        private AbstractModelApi _logic;
        private int _numberofBalls;
        private bool _isWorking = false;


        
        public IEnumerable<IBallModel> Balls { get { return _balls; } }

       
        public ICommand Start { get; init; }
        public ICommand Stop { get; init; }

        public SimViewModel(AbstractModelApi? model = default) : base()
        {
            // If no AbstractModelAPI object is provided, create a new one using the CreateInstance method.
            _logic = model ?? AbstractModelApi.CreateInstance();
            _balls = new ObservableCollection<IBallModel>();

            // Initialize the Start and Stop commands with new instances of the respective input classes.
            Start = new Start(this);
            Stop = new Stop(this);

            // Subscribe to notifications from the Model.
            Subscriber(_logic);
        }
        // This property represents the number of BallModel objects that should be created when the simulation is started.
        public int NumberofBalls
        {
            get => _numberofBalls;
            set { SetField(ref _numberofBalls, value); }
        }

        public bool IsWorking
        {
            get { return _isWorking; }
            private set { SetField(ref _isWorking, value); }
        }

        // This method starts the simulation.
        public void StartSim()
        {
            IsWorking = true;
            _logic.SpawnBalls(NumberofBalls);
            _logic.Start();
        }

        public void StopSim()
        {
            IsWorking = false;
            _logic.Stop();
        }

        #region Observer
        // This method is called when the ViewModel is subscribed to the Model's notifications.
        public void Subscriber(IObservable<IEnumerable<IBallModel>> provider)
        {
            _unsubscriber = provider.Subscribe(this);
        }

        // This method is called when the Model has finished sending notifications.
        public void OnCompleted()
        {
            _unsubscriber?.Dispose();
        }

        // This method is called when an error occurs while the Model is sending notifications.
        public void OnError(Exception error)
        {
            throw error;
        }

        // This method is called when the collection of BallModel objects in the Model changes.
        public void OnNext(IEnumerable<IBallModel> balls)
        {
            if (balls is null)
            {
                balls = new List<IBallModel>();
            }

            // Update the collection of BallModel objects in the ViewModel and notify the UI that the Balls property has changed.
            _balls = new ObservableCollection<IBallModel>(balls);
            OnPropertyChanged(nameof(Balls));
        }
        #endregion

    }
}
