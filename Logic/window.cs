using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    //klasa Window z określoną szerokością i wysokością
    public class Window
    {
        public int Width { get; init; }

        public int Height { get; init; }

        // Pobiera Vector reprezentujący granice okna na osi X
        // Składowa X Vector wynosi 0, a składowa Y jest równa właściwości Width
        public Vector2 GetXBoundry => new Vector2(0, Width);

        // Pobiera Vector reprezentujący granice okna na osi Y
        // Składowa X Vector wynosi 0, a składowa Y jest równa właściwości Height
        public Vector2 GetYBoundry => new Vector2(0, Height);

        // Inicjalizuje nową instancję klasy MyWindow z określoną szerokością i wysokością
        public Window(int width, int height)
        {
            Width = width;
            Height = height;
        }
    }


}
