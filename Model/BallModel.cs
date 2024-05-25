using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace Model
{
    public class BallModel: INotifyPropertyChanged
    {
        private double x;
        private double y;

        public BallModel(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double X
        {
            get { return x; }
            set
            {
                x = value;
                RaisePropertyChanged();
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
        public double Radius
        {
            get { return 50; }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
