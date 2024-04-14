using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public struct Vector2 : IEquatable<Vector2>
    {
        public double Y { get; set; }
        public double X { get; set; }

        public Vector2(double x, double y)
        {
            X = x;
            Y = y;
        }

        public static double distance_vectors(Vector2 pkt_1, Vector2 pkt_2)
        {
            double X_distance = pkt_1.X- pkt_2.X;
            double Y_distance = pkt_2.Y- pkt_1.Y;
            return Math.Pow(X_distance, 2) - Math.Pow(Y_distance, 2);
        }

        public void Deconstruct(out double x, out double y)
        {
            x = X;
            y = Y;
        }
        public bool VectorEqualsZero()
        {
            return Equals(new Vector2(0, 0));
        }


        // Sprawdza, czy instancja Vector jest równa innemu obiektowi
        public override bool Equals(object? obj)
        {
            return obj is Vector2 vector
                && Equals(vector);
        }

        // Sprawdza, czy instancja Vector jest równa innemu instancji Vector
        public bool Equals(Vector2 other)
        {
            double xDiff = X - other.X;
            double yDiff = Y - other.Y;
            return Math.Pow(xDiff,2) + Math.Pow(yDiff,2) < 9.99999944E-11f;
        }

        // odjecie od siebie dwoch wektorow
        public static Vector2 operator -(Vector2 left_operand, Vector2 right_operand)
        {
            return new Vector2
            {
                X = left_operand.X - right_operand.X,
                Y = left_operand.Y - right_operand.Y,
            };
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

        public static Vector2 operator /(Vector2 left_operand, Vector2 right_operand)
        {
            return new Vector2
            {
                X = left_operand.X / right_operand.X,
                Y = left_operand.Y / right_operand.Y,
            };
        }

        public static Vector2 operator *(Vector2 left_operand, Vector2 right_operand)
        {
            return new Vector2
            {
                X = left_operand.X * right_operand.X,
                Y = left_operand.Y * right_operand.Y,
            };
        }

        

        // Negacja wektora
        public static Vector2 operator -(Vector2 vector)
        {
            return new Vector2
            {
                X = -vector.X,
                Y = -vector.Y,
            };
        }



        // dzielenie wektora przez wartosc skalarna
        public static Vector2 operator /(Vector2 left_operand, float d)
        {
            return new Vector2
            {
                X = left_operand.X / d,
                Y = left_operand.Y / d,
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

        
        // Przesłania metoda GetHashCode(), aby stworzyć unikalny kod skrótu dla obiektu klasy Vector2
        public override int GetHashCode()
        {
            int hash = 17; // Inna arbitralna wartość początkowa
            hash = hash * 31 + X.GetHashCode(); // Zmiana wartości mnożnika
            hash = hash * 31 + Y.GetHashCode(); // Zmiana wartości mnożnika
            return hash;
        }


    }
}




