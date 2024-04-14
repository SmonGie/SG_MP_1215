using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    internal class Start : Command
    {
        private SimViewModel _simViewModel;

        // Constructor that takes a SimulationViewModel instance as a parameter.
        public Start(SimViewModel simViewModel) : base()
        {
            _simViewModel = simViewModel;
            _simViewModel.PropertyChanged += OnSimulationViewModelPropertyChanged;
        }

        // Implementation of the CanExecute method of the ICommand interface.
        // Checks if the command can be executed.
        // Returns true if the base implementation of CanExecute returns true and the simulation is not already running.
        public override bool CanExecute(object? parameter)
        {
            return base.CanExecute(parameter)
                && !_simViewModel.IsWorking;
        }

        // Implementation of the Execute method of the ICommand interface.
        // Executes the command.
        // Calls the StartSimulation method of the SimulationViewModel instance passed to the constructor.
        public override void Execute(object? parameter)
        {
            _simViewModel.StartSim();
        }

        // Event handler for the PropertyChanged event of the SimulationViewModel instance.
        // Checks if the IsSimulationRunning property changed.
        // Calls the OnExecuteChange method to indicate that the CanExecute method needs to be reevaluated.
        private void OnSimulationViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_simViewModel.IsWorking))
            {
                OnExecuteChange();
            }
        }
    }
}
