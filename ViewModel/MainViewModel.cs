namespace ViewModel
{
    // Ta klasa reprezentuje główny ViewModel aplikacji.
    public class MainViewModel : ViewModel
    {
        // Ta właściwość reprezentuje bieżący ViewModel wyświetlany w interfejsie użytkownika.
        public ViewModel ViewModel { get; }

        // Ten konstruktor inicjuje właściwość ViewModel nową instancją klasy SimViewModel.
        public MainViewModel() : base()
        {
            ViewModel = new SimViewModel();
        }
    }
}
