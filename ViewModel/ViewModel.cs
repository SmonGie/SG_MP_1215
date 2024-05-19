using System.Collections.ObjectModel;
using System.Windows.Input;
using Model;

namespace ViewModel
{
    // Ta klasa reprezentuje ViewModel dla widoku symulacji.
    // Implementuje interfejs IObserver do otrzymywania powiadomień od Modelu, gdy zmienia się kolekcja obiektów BallModel.
    internal class ViewModel : AbstractViewModelApi
    {
       private ObservableCollection<object>? balls;
       private readonly AbstractModelApi modelApi;
       private int ballsCount;
       public override ICommand StartCommand { get; }
       public override ICommand StopCommand { get; }
       public override ICommand SpawnBallCommand { get; }

       public ViewModel(int boardHeight, int boardWidth)
       {
            modelApi = AbstractModelApi.CreateInstance(boardHeight, boardWidth, null);
            Balls = GetBalls();
            StartCommand = new Command(StartSim);
            StopCommand = new Command(StopSim);
            SpawnBallCommand = new Command(SpawnBall);
        }

       public override void SpawnBall()
       {
           int maxBalls = 10;
           int currentBallsCount = Balls.Count;
           int remainingBalls = Math.Max(0, maxBalls - currentBallsCount);
           int numberOfBalls = Math.Min(BallsCount, remainingBalls);

           for (int i = 0; i < numberOfBalls; i++)
           {
               modelApi.SpawnBall();
           }

           Balls = GetBalls();
       }

        public int BallsCount
       {
           get => ballsCount;
           set
           {
               ballsCount = value;
               OnPropertyChanged();
           }
       }

        public override void StartSim()
       {
           modelApi.StartSimulation();
       }

       public override void StopSim()
       {
           modelApi.StopSimulation();
       }

       public override ObservableCollection<object>? Balls
       {
           get => balls;
           set
           {
               balls = value;
               OnPropertyChanged();
           }
       }
        public override ObservableCollection<object>? GetBalls()
       {
           return modelApi.GetBalls();
       }
    }
}
