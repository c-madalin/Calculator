using System;
using System.Windows;
using System.Windows.Controls;

namespace Calculator_APP_
{
    public partial class MainWindow : Window
    {
        private string _currentInput = string.Empty;
        private string _operator = string.Empty;
        private double _firstNumber = 0;
        private double _secondNumber = 0;

        public MainWindow()
        {
            InitializeComponent();
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

        private void Equals_Click(object sender, RoutedEventArgs e)
        {
            _secondNumber = double.Parse(_currentInput);
            double result = 0;

            switch (_operator)
            {
                case "+":
                    result = _firstNumber + _secondNumber;
                    break;
                case "-":
                    result = _firstNumber - _secondNumber;
                    break;
                case "*":
                    result = _firstNumber * _secondNumber;
                    break;
                case "/":
                    result = _firstNumber / _secondNumber;
                    break;
            }

            ResultTextBox.Text = result.ToString();
            _currentInput = result.ToString();
        }
    }
}