using System.Windows.Input;

namespace ViewModel
{
    // Ta klasa abstrakcyjna reprezentuje polecenie, które może zostać wykonane przez element interfejsu użytkownika w warstwie ViewModel.
    // Implementuje interfejs ICommand, który udostępnia metody sprawdzania, czy polecenie może zostać wykonane
    // i wykonanie polecenia.
    public abstract class Command : ICommand
    {
        // To zdarzenie jest wywoływane, gdy zmienia się możliwość wykonania polecenia.
        public event EventHandler? CanExecuteChanged;

        // Ta metoda określa, czy polecenie może zostać wykonane z podanym parametrem.
        // Domyślnie można wykonać wszystkie polecenia, ale klasy pochodne mogą zastąpić tę metodę, aby zapewnić bardziej szczegółową logikę.
        public virtual bool CanExecute(object? parameter) => true;

        // Ta metoda wykonuje polecenie z podanym parametrem.
        // Klasy pochodne muszą zastąpić tę metodę, aby zapewnić specyficzne zachowanie polecenia.
        public abstract void Execute(object? parameter);

        // Ta metoda wywołuje zdarzenie CanExecuteChanged, aby powiadomić element interfejsu użytkownika o zmianie możliwości wykonania polecenia.
        // Jest wywoływane przez klasy pochodne, gdy stan polecenia zmienia się w sposób wpływający na jego możliwość wykonania.
        protected void OnExecuteChange()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
