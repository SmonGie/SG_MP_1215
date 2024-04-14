using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    // This class represents the main ViewModel for the application.
    public class MainViewModel : ViewModel
    {
        // This property represents the current ViewModel that is displayed in the UI.
        public ViewModel ViewModel { get; }

        // This constructor initializes the ViewModel property with a new instance of the SimulationViewModel class.
        public MainViewModel() : base()
        {
            ViewModel = new SimViewModel();
        }
    }
}
