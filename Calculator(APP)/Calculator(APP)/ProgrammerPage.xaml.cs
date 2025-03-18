using System.Windows.Controls;
using System.Windows.Input;

namespace Calculator_APP_
{
    public partial class ProgrammerPage : Page
    {
        public ProgrammerPage()
        {
            InitializeComponent();
            DataContext = new CalculatorViewModel();
        }

        private void Page_KeyDown(object sender, KeyEventArgs e)
        {
            var viewModel = DataContext as CalculatorViewModel;
            if (viewModel == null)
                return;

            if (e.Key >= Key.D0 && e.Key <= Key.D9)
            {
                viewModel.ExecuteNumber((e.Key - Key.D0).ToString());
            }
            else if (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
            {
                viewModel.ExecuteNumber((e.Key - Key.NumPad0).ToString());
            }
            else
            {
                switch (e.Key)
                {
                    case Key.Add:
                        viewModel.ExecuteAdd(null);
                        break;
                    case Key.Subtract:
                        viewModel.ExecuteSubtract(null);
                        break;
                    case Key.Multiply:
                        viewModel.ExecuteMultiply(null);
                        break;
                    case Key.Divide:
                        viewModel.ExecuteDivide(null);
                        break;
                    case Key.Enter:
                        viewModel.ExecuteEquals(null);
                        break;
                    case Key.Back:
                        viewModel.ExecuteBackspace(null);
                        break;
                    case Key.Decimal:
                        viewModel.ExecuteNumber(".");
                        break;
                    case Key.Delete:
                        viewModel.ExecuteClear(null);
                        break;
                    case Key.Escape:
                        viewModel.ExecuteClear(null);
                        break;
                }
            }
            e.Handled = true;
        }
    }
}