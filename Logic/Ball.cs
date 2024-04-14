﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static Logic.AbstractLogicApi;

namespace Logic
{
    //Zdefiniuj klase Ball jako publiczna i zaimplementuj interfejs IEquatable dla klasy Ball
    internal class Ball : IBall, IEquatable<Ball>
    {
        //kontruktor klasy Ball ze specyficznymi wartosciami ...
        public Ball(double xVelocity, double yVelocity, int x, int y, int diameter)
            : this(new Vector2(xVelocity, yVelocity), new Vector2(x, y), diameter) { }
        public Ball(Vector2 velocity, Vector2 position, int diameter)
        {
            _velocity=velocity;
            _position=position;
            _diameter=diameter;
            _radius = diameter/2 ;


        }
        //Zdefiniuj prywatne wlasnosci klasy Ball
        private Vector2 _velocity;
        private Vector2 _position;
        private int _diameter;
        private int _radius;

        #region IBall
        public Vector2 Velocity
        {
            get { return _velocity; }
            private set { _velocity = value; }
        }

        public Vector2 Position
        {
            get { return _position; }
            private set { _position = value; }
        }

        public int Radius
        {
            get { return _radius; }
            private set { _radius = value; }
        }

        public int Diameter
        {
            get { return _diameter; }
            private set { _diameter = value; }
        }
        #endregion
        //zainicjuj generator losowosci
        private Random Random = new Random();
        //nadpisz metode GetHashCode aby zwracala wartosci hash dla obiektu typu Ball
        public override int GetHashCode()
        {
            return HashCode.Combine(Velocity, Position, Diameter);
        }
        //Zdefiniuj metode dla poruszania sie ze specyficznymi wartosciami granic x i y
        public void Move(Vector2 xBorder, Vector2 yBorder ,float force = 0.5f)
        {
            if (_velocity.VectorEqualsZero()) //jesli kula nie ma predkosci nie poruszaj kula
                return;
            _position += _velocity * force;

            var (x, y) = _position;
            
            if(!x.CheckBoundry(xBorder.X, xBorder.Y, _radius))
            {
                _velocity = new Vector2(-_velocity.X, _velocity.Y); // jesli kula uderzy w sciane, odwroc predkosc
            }
            if(!y.CheckBoundry(yBorder.X, yBorder.X, _radius))
            {
                _velocity = new Vector2(_velocity.X, -_velocity.Y); //jesli kula uderzy w sciane odwroc predkosc
            }
        }
        //nadpisuje i porownuje dwa obiekty typu Ball(czy sa rowne)
        public override bool Equals(Object? other)
        {
            return other is Ball ball
                && Equals(ball);
        }
        //definiuje metode aby sprawdzic dwa obiekty typu Ball(czy sa rowne)
        public bool Equals(Ball? other)
        {
            return other is not null
                && Velocity == other.Velocity
                && Position == other.Position
                && Diameter == other.Diameter;
        }
    }
    //definicja klasy statycznej dla sprawdzania granic x i y
    public static class Boundry
    {
        public static bool CheckBoundry(this double value, double min, double max, double padding = 0f)
        {
            return (value - padding >= min) && (value + padding <= max);
        }

        public static bool CheckBoundry(this int value, int min, int max)
        {
            return (value >= min) && (value <= max);
        }
    }
}
