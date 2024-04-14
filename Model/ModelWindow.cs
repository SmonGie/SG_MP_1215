using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic;

namespace Model
{
    public class ModelWindow
    {

        private Window _window; // referencja do obiektu window
        public int Width => _window.Width; // wlasnosc do dostania szerokosci okna
        public int Height => _window.Height; // wlasnosc do dostania wysokosci okna

        // konstruktor ktory tworzy ModelWindow przy pomocy referencji do obiektu typu window
        public ModelWindow(Window window)
        {
            _window = window;
        }
    }
}
