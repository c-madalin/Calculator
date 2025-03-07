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

        //private void ModeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    ComboBox comboBox = sender as ComboBox;
        //    if (comboBox != null)
        //    {
        //        ComboBoxItem selectedItem = comboBox.SelectedItem as ComboBoxItem;
        //        if (selectedItem != null)
        //        {
        //            string selectedMode = selectedItem.Content.ToString();
        //            switch (selectedMode)
        //            {
        //                case "Calculator Standard":
        //                    // Navigate to Standard Calculator Page
        //                    MessageBox.Show("Navigating to Standard Calculator");
        //                    break;
        //                case "Calculator Programator":
        //                    // Navigate to Programmer Calculator Page
        //                    MessageBox.Show("Navigating to Programmer Calculator");
        //                    break;
        //            }
        //        }
        //    }
        //}
        // MainWindow.xaml.cs
        private void ModeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ModeComboBox.SelectedIndex == 0)
            {
                // Calculator standard
                MainFrame.Navigate(new Uri("StandardCalculatorPage.xaml", UriKind.Relative));
            }
            else if (ModeComboBox.SelectedIndex == 1)
            {
                // Calculator programator
                MainFrame.Navigate(new Uri("ProgrammerCalculatorPage.xaml", UriKind.Relative));
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
                case "%":
                    result = _firstNumber % _secondNumber;
                    break;
            }

            ResultTextBox.Text = result.ToString();
            _currentInput = result.ToString();
        }
    }
}