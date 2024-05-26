﻿using System.Numerics;

namespace Data
{
    //Definicja klasy Data jako wewnetrzenj i podklasy AbstractDataApi
    internal class Data : AbstractDataApi
    {
        public override int Width { get; }
        public override int Height { get; }
        private List<IBall> Balls { get; }

        public Data()
        {
            Balls = new List<IBall>();
            Width = 500;
            Height = 500;
        }
        // Zdarzenie wywoływane przy zmianie pozycji piłki
        public override event EventHandler BallEvent;

        private void BallPositionChanged(object sender, EventArgs e)
        {
            if (sender != null)
            {
                BallEvent?.Invoke(sender, EventArgs.Empty);
            }
        }

        // Metoda zwracająca pozycję piłki na podstawie numeru w liście
        public override Vector2 GetBallPosition(int number)
        {
            return Balls[number].Position;
        }

        public override void SpawnBalls(int amount)
        {

            int ballnumber = Balls.Count; // Aktualna liczba piłek w liście


            Random random = new Random();
            for (int i = 0; i < amount; i++)
            {
                // Tworzenie nowej piłki z losową pozycją w obrębie planszy
                Ball ball = new Ball(random.Next(100,Width-100), random.Next(Height-100));
                Balls.Add(ball); // Dodanie piłki do listy
                ball.PositionChange += BallPositionChanged;

            }

    }

        // Metoda zwracająca liczbę piłek w liście
        public override int GetNumberOfBalls()
        {
            return Balls.Count;
        }
        // Metoda zwracająca piłkę na podstawie numeru w liście
        public override IBall GetBall(int number)
        {
            return Balls[number];
        }
    }
}
