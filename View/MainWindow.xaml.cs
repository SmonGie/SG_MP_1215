using System.Windows;
using ViewModel;

namespace View
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            AbstractViewModelApi viewModel = AbstractViewModelApi.CreateInstance(150,305);

            DataContext = viewModel;
        }
    }
}
