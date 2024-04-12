using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public interface IBillardBall : INotifyPropertyChanged
    {
        double vertical { get; }
        double horizontal { get; }
        double Radius { get; }
    }

    public class DynamicChangeArgs : EventArgs
    {
        public IBillardBall Ball { get; internal set; }
    }
}
