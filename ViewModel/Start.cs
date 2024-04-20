using System.ComponentModel;

namespace ViewModel
{
    internal class Start : Command
    {
        private SimViewModel _simViewModel;

        // Konstruktor pobierający jako parametr instancję SimulationViewModel.

        public Start(SimViewModel simViewModel) : base()
        {
            _simViewModel = simViewModel;
            _simViewModel.PropertyChanged += OnSimulationViewModelPropertyChanged;
        }

        // Implementacja metody CanExecute interfejsu ICommand.
        // Sprawdza, czy polecenie może zostać wykonane.
        // Zwraca wartość true, jeśli podstawowa implementacja CanExecute zwraca wartość true, a symulacja jeszcze nie jest uruchomiona.
        public override bool CanExecute(object? parameter)
        {
            return base.CanExecute(parameter)
                && !_simViewModel.IsWorking;
        }

        // Implementacja metody Execute interfejsu ICommand.
        // Wykonuje polecenie.
        // Wywołuje metodę StartSim instancji SimViewModel przekazanej do konstruktora.
        public override void Execute(object? parameter)
        {
            _simViewModel.StartSim();
        }

        // Procedura obsługi zdarzenia PropertyChanged instancji SimViewModel.
        // Sprawdza, czy właściwość IsWorking uległa zmianie.
        // Wywołuje metodę OnExecuteChange, aby wskazać, że metoda CanExecute wymaga ponownej oceny.
        private void OnSimulationViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_simViewModel.IsWorking))
            {
                OnExecuteChange();
            }
        }
    }
}
