﻿using System.Collections.ObjectModel;
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
        public IEnumerable<IBallModel> Balls => _balls;

       
        public ICommand Start { get; }
        public ICommand Stop { get; }

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

        public void Subscriber(IObservable<IEnumerable<IBallModel>> provider)
        {
            _unsubscriber = provider.Subscribe(this);
        }

        public void OnCompleted() => _unsubscriber?.Dispose();
        

        
        public void OnError(Exception error)
        {
            throw error;
        }

  
        public void OnNext(IEnumerable<IBallModel> balls)
        {
            _balls = new ObservableCollection<IBallModel>(balls ?? new List<IBallModel>());
            OnPropertyChanged(nameof(Balls));
        }
        #endregion

    }
}
