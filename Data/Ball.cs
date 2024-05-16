﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    internal class Ball : AbstractBallApi, INotifyPropertyChanged
    {
        private Vector2 position;
        private int Vx;
        private int Vy;
        private bool isWorkingSim;
        private readonly int mass;
        private object lockBall = new object();
        private Stopwatch stopwatch = new Stopwatch();

        public Ball(Vector2 position, int vx, int vy, int mass, bool isWorkingSim)
        {
            this.position = position;
            Vx = vx;
            Vy = vy;
            this.isWorkingSim = isWorkingSim;
            this.mass = mass;
        }
        public override void AddPropertyChangedListener(PropertyChangedEventHandler handler)
        {
            this.PropertyChanged += handler;
        }

        public override Vector2 Position
        {
            get
            {
                lock (lockBall)
                {
                    return position;
                }
            }
        }

        public override int PositionX
        {
            get { return (int)position.X; }
        }
        public override int PositionY
        {
            get { return (int)position.X; }
        }

        private void setPosition(Vector2 Pos)
        {
            lock (lockBall)
            {
                position.X = Pos.X;
                position.Y = Pos.Y;
            }
            OnPropertyChanged(nameof(Position.X));
            OnPropertyChanged(nameof(Position.Y));
        }

        public override int VelocityX
        {
            get
            {
                lock (lockBall)
                {
                    return Vx;
                }
            }
            set => Vx = value;
        }

        public override int VelocityY
        {
            get
            {
                lock (lockBall)
                {
                    return Vy;
                }
            }
            set => Vy = value;
        }
        public override void setVelocity(int Vx, int Vy)
        {
            lock (lockBall)
            {
                this.Vx = Vx;
                this.Vy = Vy;
            }
        }

        public override int Mass => mass;

        private async Task Move()
        {
            while(true)
            {
                stopwatch.Restart();
                if(isWorking)
                {
                    int posX = (int)position.X + Vx;
                    int posY = (int)position.Y + Vy;
                    Vector2 Pos = new Vector2(posX, posY);
                    setPosition(Pos);
                }
                double velocity = Math.Sqrt(Vx * Vx + Vy * Vy);
                stopwatch.Stop();
                await Task.Delay(TimeSpan.FromMilliseconds(1000 / 460 * velocity + (int)stopwatch.ElapsedMilliseconds));
            }
        }
        public override bool isWorking
        {
            get { return isWorking; }
            set => isWorkingSim = value;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
