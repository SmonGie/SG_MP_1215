using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    internal class Stop : Command
    {
        private SimViewModel _simViewModel;

        public Stop(SimViewModel simViewModel) : base()
        {
            _simViewModel = simViewModel;
            // Subskrybuj zdarzenie PropertyChanged instancji SimulationViewModel
            _simViewModel.PropertyChanged += OnSimulationViewModelPropertyChanged;
        }

        public override bool CanExecute(object? parameter)
        {
            // Sprawdź, czy podstawowa metoda CanExecute zwraca wartość true i czy symulacja jest aktualnie uruchomiona
            return base.CanExecute(parameter)
                && _simViewModel.IsWorking;
        }

        public override void Execute(object? parameter)
        {
            // Wywołaj metodę StopSim instancji SimViewModel
            _simViewModel.StopSim();
        }

        private void OnSimulationViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            // Jeśli własnosc IsWorking instancji SimViewModel ulegnie zmianie, wywołaj metodę OnExecuteChange
            if (e.PropertyName == nameof(_simViewModel.IsWorking))
            {
                OnExecuteChange();
            }
        }
    }
}
