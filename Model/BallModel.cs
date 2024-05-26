using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace Model
{
    public class BallModel: INotifyPropertyChanged
    {
        private double x;
        private double y;

        public double Radius
        {
            get { return 50; }
        }

        public BallModel(double x, double y)
        {
            X = x; // Inicjalizacja współrzędnej X kuli
            Y = y;
        }
        // Właściwość X reprezentująca współrzędną X kuli
        public double X
        {
            get { return x; } // Zwraca wartość współrzędnej X
            set
            {
                x = value; // Ustawia wartość współrzędnej X
                RaisePropertyChanged(); // Wywołuje metodę powiadamiającą o zmianie
            }
        }
        public double Y
        {
            get { return y; }
            set
            {
                y = value;
                RaisePropertyChanged();
            }
        }
        // Zdarzenie PropertyChanged wywoływane w przypadku zmiany właściwości
        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
