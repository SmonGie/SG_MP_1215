﻿using Model;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ViewModel
{
    public class ViewModel
    {
        private AbstractModelApi ModelApi { get; set; }
        public ObservableCollection<BallModel> Balls { get; }
        public int NumberOfBalls { get; set; }
        public ICommand AddCommand { get; set; }

        public ViewModel()
        {
            ModelApi = AbstractModelApi.CreateInstance();
            AddCommand = new RelayCommand(AddBalls);
            Balls = ModelApi.Balls();
        }

        public void AddBalls()
        {
            ModelApi.SpawnBall(NumberOfBalls);
        }
    }
}