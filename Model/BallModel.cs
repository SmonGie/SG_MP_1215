﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

using Logic;

namespace Model
{
    internal class BallModel : IBallModel
    {
        private IBall _ball; // Private field to store the Ball object.
        private Vector _velocity => _ball.Velocity;
        private Vector _position => CalcOffsetPosition(_ball.Position);
        private int _radius => _ball.Radius;
        private int _diameter => _ball.Diameter;

        #region IBallModel
        public Vector Velocity
        {
            get { return _velocity; }
        }

        public Vector Position
        {
            get { return _position; }
        }

        public int Radius
        {
            get { return _radius; }
        }

        public int Diameter
        {
            get { return _diameter; }
        }
        #endregion

        // Constructor that takes a Ball object and initializes the private field.
        public BallModel(IBall ball)
        {
            _ball = ball;
        }

        // Private method that calculates the offset position of the Ball based on its radius.
        private Vector CalcOffsetPosition(Vector position)
        {
            return new Vector(position.X - Radius, position.Y - Radius);
        }

    }
}
