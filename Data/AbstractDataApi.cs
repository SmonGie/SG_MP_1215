using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public abstract class AbstractDataApi
    {
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
