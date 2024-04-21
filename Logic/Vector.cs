namespace Logic
{
    public struct Vector2 : IEquatable<Vector2>
    {
        //Wlasnosci X i Y ktore pozwalaja na wywolanie getterow i setterow Vector2
        public double X { get; set; }
        public double Y { get; set; }
        //Konstruktor ktory tworzy wektor z specyficznym X i Y
        public Vector2(double x, double y)
        {
            X = x;
            Y = y;
        }
        
        //Rozlozenie wektora na dwie wartosci x i y
        public void Deconstruct(out double x, out double y)
        {
            x = X;
            y = Y;
        }
        //Sprawdza czy wartosc wektora jest rowna 0
        public bool VectorEqualsZero()
        {
            return Equals(new Vector2(0, 0));
        }


        // Sprawdza, czy instancja Vector jest równa innemu instancji Vector
        public bool Equals(Vector2 other)
        {
            double xDiff = X - other.X;
            double yDiff = Y - other.Y;
            return Math.Pow(xDiff,2) + Math.Pow(yDiff,2) < 9.99999944E-11f;
        }


        //dodanie do siebie dwoch wektorow
        public static Vector2 operator +(Vector2 left_operand, Vector2 right_operand)
        {
            return new Vector2
            {
                X = left_operand.X + right_operand.X,
                Y = left_operand.Y + right_operand.Y,
            };
        }


        // mnozenie wektora przez wartosc skalarna
        public static Vector2 operator *(Vector2 left_operand, float d)
        {
            return new Vector2
            {
                X = left_operand.X * d,
                Y = left_operand.Y * d,
            };
        }

        // Zastępuje operator równości w celu porównania dwóch wektorów Vector2
        public static bool operator ==(Vector2 left_operand, Vector2 right_operand)
        {
            return left_operand.Equals(right_operand);
        }

        // Zastępuje operator nierówności w celu porównania dwóch wektorów Vector2
        public static bool operator !=(Vector2 left_operand, Vector2 right_operand)
        {
            return !(left_operand == right_operand);
        }

    }
}




