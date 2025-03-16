
using System.Windows.Controls;

namespace Calculator_APP_
{   
    public partial class StandardPage : Page
    {
      
        public StandardPage()
        {
            InitializeComponent();
            DataContext = new CalculatorViewModel();

        }
    }
}
