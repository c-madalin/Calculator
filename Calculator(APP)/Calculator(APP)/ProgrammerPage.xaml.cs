using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator_APP_
{
    /// <summary>
    /// Interaction logic for ProgrammerPage.xaml
    /// </summary>
    public partial class ProgrammerPage : Page
    {

        private string _currentInput = string.Empty;
        private string _operator = string.Empty;
        private int _firstNumber = 0;
        private int _secondNumber = 0;
        public ProgrammerPage()
        {
            InitializeComponent();
        }
        // Metodă pentru a adăuga cifre binare (0 și 1) la input
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                string digit = button.Content.ToString();
                if (digit == "0" || digit == "1")
                {
                    _currentInput += digit;
                    ResultTextBox.Text = _currentInput;
                }
            }
        }

        // Selectează operatorul și setează primul număr
        private void Operator_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null && !string.IsNullOrEmpty(_currentInput))
            {
                _firstNumber = Convert.ToInt32(_currentInput, 2); // Convertire din binar în int
                _operator = button.Content.ToString();
                _currentInput = string.Empty;
            }
        }

        // Șterge ultima cifră introdusă
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (_currentInput.Length > 0)
            {
                _currentInput = _currentInput.Remove(_currentInput.Length - 1);
                ResultTextBox.Text = _currentInput;
            }
        }

        // Șterge tot
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            _currentInput = string.Empty;
            _firstNumber = 0;
            _secondNumber = 0;
            _operator = string.Empty;
            ResultTextBox.Text = string.Empty;
        }

        // Efectuează conversia în binar și afișează rezultatul
        private void ShowResult(int number)
        {
            string binaryResult = Convert.ToString(number, 2); // Convertire în binar
            ResultTextBox.Text = binaryResult;
            _currentInput = binaryResult;
        }

        // Metodă pentru calcularea rezultatului
        private void Equals_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(_currentInput)) return;

            _secondNumber = Convert.ToInt32(_currentInput, 2); // Convertire din binar în int
            int result = 0;

            switch (_operator)
            {
                case "+": result = _firstNumber + _secondNumber; break;
                case "-": result = _firstNumber - _secondNumber; break;
                case "*": result = _firstNumber * _secondNumber; break;
                case "/":
                    if (_secondNumber != 0)
                        result = _firstNumber / _secondNumber;
                    else
                    {
                        ResultTextBox.Text = "Err"; // Evităm împărțirea la 0
                        return;
                    }
                    break;
                case "&": result = _firstNumber & _secondNumber; break;
                case "|": result = _firstNumber | _secondNumber; break;
                case "^": result = _firstNumber ^ _secondNumber; break;
                case "<<": result = _firstNumber << _secondNumber; break;
                case ">>": result = _firstNumber >> _secondNumber; break;
            }

            ShowResult(result);
        }

        // Operația NOT, aplicabilă pe un singur număr
        private void Not_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(_currentInput))
            {
                int number = Convert.ToInt32(_currentInput, 2);
                int notResult = ~number; // Aplicăm operatorul bitwise NOT
                ShowResult(notResult);
            }
        }
    }
}
