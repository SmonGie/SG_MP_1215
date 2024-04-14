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
            // Subscribe to the PropertyChanged event of the SimulationViewModel instance
            _simViewModel.PropertyChanged += OnSimulationViewModelPropertyChanged;
        }

        public override bool CanExecute(object? parameter)
        {
            // Check if the base CanExecute method returns true and the simulation is currently running
            return base.CanExecute(parameter)
                && _simViewModel.IsWorking;
        }

        public override void Execute(object? parameter)
        {
            // Call the StopSimulation method of the SimulationViewModel instance
            _simViewModel.StopSim();
        }

        private void OnSimulationViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            // If the IsSimulationRunning property of the SimulationViewModel instance changes, call the OnExecuteChange method
            if (e.PropertyName == nameof(_simViewModel.IsWorking))
            {
                OnExecuteChange();
            }
        }
    }
}
