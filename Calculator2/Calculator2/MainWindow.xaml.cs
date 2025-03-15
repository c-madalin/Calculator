using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Calculator2
{
    public partial class MainWindow : Window
    {
        private bool isInitialized = false;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new CalculatorViewModel();
        }
    }
}