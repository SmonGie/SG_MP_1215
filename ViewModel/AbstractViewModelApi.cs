using Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace ViewModel
{
    public abstract class AbstractViewModelApi : INotifyPropertyChanged
    {

        public abstract ICommand StartCommand { get; }
        public abstract ICommand StopCommand { get; }
        public abstract ICommand SpawnBallCommand { get; }
        public abstract void StartSim();
        public abstract void StopSim();
        public abstract void SpawnBall();
        public abstract ObservableCollection<object>? GetBalls();
        public abstract ObservableCollection<object>? Balls{get; set; }

    public static AbstractViewModelApi CreateInstance(int windowHeight, int windowWidth)
        {
            return new ViewModel(windowHeight, windowWidth);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}