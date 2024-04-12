using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Model
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

    public event PropertyChangedEventHandler PropertyChanged;
    }
}
