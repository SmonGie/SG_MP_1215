using Logic;
using Vector2 = Logic.Vector2;

namespace Model
{
    internal class BallModel : IBallModel
    {
        private IBall _ball; // Prywatne pola do przechowywania wartosci obiektu
        private Vector2 _velocity => _ball.Velocity;
        private Vector2 _position => CalcOffsetPosition(_ball.Position);
        private int _radius => _ball.Radius;
        private int _diameter => _ball.Diameter;

        #region IBallModel
        public Vector2 Velocity
        {
            get { return _velocity; }
        }

        public Vector2 Position
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

        // konstuktor ktory bierze obiekt i inicjalizuje prywatne wartosci
        public BallModel(IBall ball)
        {
            _ball = ball;
        }

        // prywatna metoda ktora kalkuluje pozycje offset kuli na podstawie promienia
        private Vector2 CalcOffsetPosition(Vector2 position)
        {
            return new Vector2(position.X - Radius, position.Y - Radius);
        }

    }
}
