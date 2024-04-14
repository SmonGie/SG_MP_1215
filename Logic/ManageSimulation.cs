﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    internal class ManageSimulation
    {
        private Window window; //Okno w ktorym miejsce ma symulacja
        private int Radius; // promien kuli
        private int Diameter; // srednica kuli
        private const float maxVelocity = 60; // maksymalna predkosc kuli
        private Random random = new Random(); // inicjalizuje generator losowosci

        public IList<Ball> Balls { get; private set; } //Lista kul w symulacji

        public ManageSimulation(Window window, int Diameter)
        {
            this.window = window; // przechowuj dane o oknie w ktorym miejsce ma symulacji
            this.Diameter = Diameter; // przechowuj sredice kuli
            Balls = new List<Ball>(); // Stworz liste do przechowywania kul
            this.Radius = Diameter / 2; // przechowuj promien kuli
        }

        public void ballsForce(float force = 0.05f) //uzyj sily do kazdej kuli w symulacji
        {
            foreach( var ball in Balls)
            {
                ball.Move(window.GetXBoundry, window.GetYBoundry, force);
            }
        }
        private Vector2 RandomBallPosision() 
        {
            int x = random.Next(Radius, window.Width - Radius); //wygeneruj losowo pozycje(x) kuli
            int y = random.Next(Radius, window.Height - Radius); ////wygeneruj losowo pozycje(y) kuli
            return new Vector2(x, y);  //zwroc wektor polozenie
        }
    
        private Vector2 RandomBallVelocity()
        {
            const float halfVelocity = maxVelocity / 2;
            double x = random.NextDouble() * maxVelocity - halfVelocity; //wygeneruj losowo przyspieszenie(x) kuli
            double y = random.NextDouble() * maxVelocity - halfVelocity; //wygeneruj losowo przyspieszenie(x) kuli

            return new Vector2(x, y);    //zwroc wektor przyspieszenia
        }

        public IList<Ball> RandomBallSpawnPosision(int ballNumber) 
        {
            Balls = new List<Ball>(ballNumber); //Stworz liste o wielkosci rownej ballNumber

            for(int i = 0; i < ballNumber; i++)
            {
                Vector2 position = RandomBallPosision(); // Wygeneruj losowo pozycje
                Vector2 velocity = RandomBallVelocity(); // Wygeneuj losowa predkosc

                Balls.Add(new Ball(velocity, position, Diameter)); //stworz kule o wylosowanych parametrach
            }
            return Balls; //zwroc liste kul
        }
    }
}
