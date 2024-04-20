using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ViewModel
{
    // Ta klasa abstrakcyjna reprezentuje ViewModel, który jest odpowiedzialny za dostarczanie danych i zachowanie widoku w środowisku MVVM.
    // Implementuje interfejs INotifyPropertyChanged, który umożliwia elementom interfejsu użytkownika nasłuchiwanie zmian we wlasciwosciach ViewModel.
    public abstract class ViewModel : INotifyPropertyChanged
    {
        // To zdarzenie jest wywoływane, gdy zmienia się właściwość ViewModel.
        public event PropertyChangedEventHandler? PropertyChanged;

        // Ta metoda wywołuje zdarzenie PropertyChanged, aby powiadomić interfejs użytkownika o zmianie właściwości.
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Ta metoda ustawia wartość pola i wywołuje zdarzenie PropertyChanged, jeśli wartość uległa zmianie.
        // Parametr propertyName jest automatycznie wypełniany nazwą wywołującej właściwości, więc nie trzeba go przekazywać jawnie.
        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
            {
                // Jeśli nowa wartość jest taka sama jak bieżąca, nie ma potrzeby aktualizowania pola ani wywoływania zdarzenia.
                return false;
            }
            field = value;
            OnPropertyChanged(propertyName);

            // Zwraca wartość true, aby wskazać, że wartość uległa zmianie.
            return true;
        }
    }


}
