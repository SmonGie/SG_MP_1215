using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Model;

namespace ViewModel
{
    // Ta klasa reprezentuje ViewModel dla widoku symulacji.
    // Implementuje interfejs IObserver do otrzymywania powiadomień od Modelu, gdy zmienia się kolekcja obiektów BallModel.
    internal class SimViewModel : ViewModel, IObserver<IEnumerable<IBallModel>>
    {
        private IDisposable? _unsubscriber;
        private ObservableCollection<IBallModel> _balls;
        private AbstractModelApi _logic;
        private int _numberofBalls;
        private bool _isWorking = false;


        // Ta własnosc zwraca kolekcję obiektów BallModel, które są aktualnie wyświetlane w interfejsie użytkownika.
        public IEnumerable<IBallModel> Balls { get { return _balls; } }

       
        public ICommand Start { get; init; }
        public ICommand Stop { get; init; }

        public SimViewModel(AbstractModelApi? model = default) : base()
        {
            // Jeśli nie podano obiektu AbstractModelApi, utwórz nowy za pomocą metody CreateInstance.
            _logic = model ?? AbstractModelApi.CreateInstance();
            _balls = new ObservableCollection<IBallModel>();

            // Zainicjuj polecenia Start i Stop nowymi instancjami odpowiednich klas wejściowych.
            Start = new Start(this);
            Stop = new Stop(this);

            //Zaubskrybuj powiadomienia od Model.
            Subscriber(_logic);
        }
        // Ta właściwość reprezentuje liczbę obiektów BallModel, które powinny zostać utworzone po rozpoczęciu symulacji.
        public int NumberofBalls
        {
            get => _numberofBalls;
            set { SetField(ref _numberofBalls, value); }
        }
        //Ta metoda sprawdz czy symulacja dziala
        public bool IsWorking
        {
            get { return _isWorking; }
            private set { SetField(ref _isWorking, value); }
        }

        //Ta metoda zaczyna symulacje
        public void StartSim()
        {
            IsWorking = true;
            _logic.SpawnBalls(NumberofBalls);
            _logic.Start();
        }
        //Ta metoda zatrzymuje symulacje
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
