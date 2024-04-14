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

        private window _window; // A reference to a Window object
        public int Width => _window.Width; // Property to get the width of the Window
        public int Height => _window.Height; // Property to get the height of the Window

        // Constructor that creates a WindowModel with a reference to a Window object
        public ModelWindow(window window)
        {
            _window = window;
        }
    }
}
