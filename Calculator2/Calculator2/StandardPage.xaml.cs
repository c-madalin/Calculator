using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Calculator2
{
    /// <summary>
    /// Interaction logic for ProgrammerPage.xaml
    /// </summary>
    public partial class StandardPage : Page
    {
        private string _currentInput = string.Empty;
        private string _operator = string.Empty;
        private double _firstNumber = 0;
        private double _secondNumber = 0;

        public StandardPage()
        {
            InitializeComponent();
        }
        private void Operator_ClickFromKeyboard(Key key)
        {
            if (!string.IsNullOrEmpty(_currentInput))
            {
                _firstNumber = double.Parse(_currentInput);
                _currentInput = string.Empty;

                switch (key)
                {
                    case Key.Add:
                        _operator = "+";
                        break;
                    case Key.Subtract:
                        _operator = "-";
                        break;
                    case Key.Multiply:
                        _operator = "*";
                        break;
                    case Key.Divide:
                        _operator = "/";
                        break;
                }
            }
        }

        private void Page_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9) // Numere de la tastatură
            {
                _currentInput += (e.Key - Key.D0).ToString();
            }
            else if (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) // Numere de la Numpad
            {
                _currentInput += (e.Key - Key.NumPad0).ToString();
            }
            else if (e.Key == Key.OemComma || e.Key == Key.OemPeriod) // Punct sau virgulă
            {
                if (!_currentInput.Contains(".")) // Evită mai multe puncte
                {
                    _currentInput += ".";
                }
            }
            else if (e.Key == Key.Add || e.Key == Key.Subtract || e.Key == Key.Multiply || e.Key == Key.Divide)
            {
                Operator_ClickFromKeyboard(e.Key);
            }
            else if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                Equals_Click(null, null);
            }
            else if (e.Key == Key.Back)
            {
                Delete_Click(null, null);
            }
            else if (e.Key == Key.Escape)
            {
                Clear_Click(null, null);
            }

            ResultTextBox.Text = _currentInput;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                _currentInput += button.Content.ToString();
                ResultTextBox.Text = _currentInput;
            }
        }

        private void Operator_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                _firstNumber = double.Parse(_currentInput);
                _operator = button.Content.ToString();
                _currentInput = string.Empty;
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (_currentInput.Length > 0)
            {
                _currentInput = _currentInput.Remove(_currentInput.Length - 1);
                ResultTextBox.Text = _currentInput;
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            _currentInput = string.Empty;
            ResultTextBox.Text = string.Empty;
        }

        private void Equals_Click(object sender, RoutedEventArgs e)
        {
            _secondNumber = double.Parse(_currentInput);
            ResultTextBox.Text = _currentInput;

        }
    }
}