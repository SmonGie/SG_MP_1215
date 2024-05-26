using System.Windows.Input;

namespace ViewModel
{
    public class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged; // Zdarzenie wywoływane przy zmianie możliwości wykonania polecenia

        private readonly Action _execute; // Delegat akcji do wykonania
        private readonly Func<bool> _canExecute; // Delegat funkcji sprawdzającej możliwość wykonania polecenia

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));  // Sprawdzenie, czy podano metodę do wykonania polecenia
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            // Zwraca true, jeśli nie ma metody określającej możliwość wykonania polecenia lub zwraca wartość z metody _canExecute()
            return _canExecute == null || _canExecute();
        }

        public void Execute(object parameter)
        {
            _execute();
        }

        protected virtual void OnExecuteChange()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}