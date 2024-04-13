using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
//Model
namespace Ball
{
    internal class Ball
    {
        public Ball(double vertical, double horizontal)
        {
            X = horizontal;
            Y = vertical;

        }
        public double X { get; set; }
        public double Y { get; set; }
        public double Radius { get; set; }
    


    private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private Random Random = new Random();


    public event PropertyChangedEventHandler PropertyChanged;

        private void MoveABall(object state)
        {
            if (state != null)
                throw new ArgumentOutOfRangeException(nameof(state));
            Y = Y + (Random.NextDouble() - 0.5) * 5;
            X = X + (Random.NextDouble() - 0.5) * 5;
        }
    }
}
