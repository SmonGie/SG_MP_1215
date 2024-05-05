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
            Velocity = velocity;
            Position = position;
            Diameter = diameter;
            Radius = diameter / 2;
        }
        //Zdefiniuj prywatne wlasnosci klasy Ball
        private Vector2 _velocity;
        private Vector2 _position;
        private int _diameter;
        private int _radius;
        
        //Zdefiniuj metode dla poruszania sie ze specyficznymi wartosciami granic x i y
        public void Move(Vector2 xBorder, Vector2 yBorder ,float force = 1f)
        {
            if (Velocity.VectorEqualsZero()) //jesli kula nie ma predkosci nie poruszaj kula
                return;
            Position += Velocity * force;

            var (X, Y) = Position;
            
            if(!X.CheckBoundary(xBorder.X, xBorder.Y, Radius))
            {
                Velocity = new Vector2(-Velocity.X, Velocity.Y); // jesli kula uderzy w sciane, odwroc predkosc
            }
            if(!Y.CheckBoundary(yBorder.X, yBorder.Y, Radius))
            {
                Velocity = new Vector2(Velocity.X, -Velocity.Y); //jesli kula uderzy w sciane odwroc predkosc
            }
        }
        //nadpisuje i porownuje dwa obiekty typu Ball(czy sa rowne)
        public override bool Equals(object? obj)
        {
            return obj is Ball ball && Equals(ball);
        }
        //definiuje metode aby sprawdzic dwa obiekty typu Ball(czy sa rowne)
        public bool Equals(Ball? other)
        {
            return other is not null &&
                   Velocity.Equals(other.Velocity) &&
                   Position.Equals(other.Position) &&
                   Diameter == other.Diameter;
        }

        #region IBall
        public Vector2 Velocity { get; private set; }
        public Vector2 Position { get; private set; }
        public int Diameter { get; private set; }
        public int Radius { get; private set; }
        #endregion
    }
    //definicja klasy statycznej dla sprawdzania granic x i y
    public static class Boundry
    {
        public static bool CheckBoundary(this double value, double min, double max, double padding = 1f)
        {
            return (value - padding >= min) && (value + padding <= max);
        }

    }
}
