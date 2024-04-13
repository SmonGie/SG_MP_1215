using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public struct Vector : IEquatable<Vector>
    {
        public double Y { get; set; }
        public double X { get; set; }

        public Vector(double x, double y)
        {
            x = X;
            y = Y;
        }

        public static double distance_vectors(Vector pkt_1, Vector pkt_2)
        {
            double X_distance = pkt_1.X- pkt_2.X;
            double Y_distance = pkt_2.Y- pkt_1.Y;
            return Math.Pow(X_distance, 2) - Math.Pow(Y_distance, 2);
        }

        public void deconstruct(out double x, out double y)
        {
            x = X;
            y = Y;
        }


        // Sprawdza, czy instancja Vector jest równa innemu obiektowi
        public override bool Equals(object? obj)
        {
            return obj is Vector vector
                && Equals(vector);
        }

        // Sprawdza, czy instancja Vector jest równa innemu instancji Vector
        public bool Equals(Vector other)
        {
            double xDiff = X - other.X;
            double yDiff = Y - other.Y;
            return Math.Pow(xDiff,2) + Math.Pow(yDiff,2) < 9.99999944E-11f;
        }

        // odjecie od siebie dwoch wektorow
        public static Vector operator -(Vector left_operand, Vector right_operand)
        {
            return new Vector
            {
                X = left_operand.X - right_operand.X,
                Y = left_operand.Y - right_operand.Y,
            };
        }

        //dodanie do siebie dwoch wektorow
        public static Vector operator +(Vector left_operand, Vector right_operand)
        {
            return new Vector
            {
                X = left_operand.X + right_operand.X,
                Y = left_operand.Y + right_operand.Y,
            };
        }

        public static Vector operator /(Vector left_operand, Vector right_operand)
        {
            return new Vector
            {
                X = left_operand.X / right_operand.X,
                Y = left_operand.Y / right_operand.Y,
            };
        }

        public static Vector operator *(Vector left_operand, Vector right_operand)
        {
            return new Vector
            {
                X = left_operand.X * right_operand.X,
                Y = left_operand.Y * right_operand.Y,
            };
        }

        

        // Negacja wektora
        public static Vector operator -(Vector vector)
        {
            return new Vector
            {
                X = -vector.X,
                Y = -vector.Y,
            };
        }



        // dzielenie wektora przez wartosc skalarna
        public static Vector operator /(Vector left_operand, float d)
        {
            return new Vector
            {
                X = left_operand.X / d,
                Y = left_operand.Y / d,
            };
        }


        // mnozenie wektora przez wartosc skalarna
        public static Vector operator *(Vector left_operand, float d)
        {
            return new Vector
            {
                X = left_operand.X * d,
                Y = left_operand.Y * d,
            };
        }

        // Zastępuje operator równości w celu porównania dwóch wektorów Vector2
        public static bool operator ==(Vector left_operand, Vector right_operand)
        {
            return left_operand.Equals(right_operand);
        }

        // Zastępuje operator nierówności w celu porównania dwóch wektorów Vector2
        public static bool operator !=(Vector left_operand, Vector right_operand)
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




