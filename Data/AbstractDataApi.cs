using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    // Definicja klasy AbstractDataApi jako abstrakcyjnej
    public abstract class AbstractDataApi
    {
        // Definicja abstrakcyjnych elementow dla wysokosci i szerokosci okna i promienia kuli
        public abstract int BallRadius { get; }
        public abstract int HeightWindow { get; }
        public abstract int WidthWindow { get; }


        // Zdefiniuj metodę statyczną, aby utworzyć wystąpienie klasy AbstractDataAPI.
        public static AbstractDataApi CreateInstance()
        {
            // Zwraca nową instancję klasy Data.
            return new Data();
        }
    }
}
