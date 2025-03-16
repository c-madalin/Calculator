using System;
using System.Windows;
using System.Windows.Controls;

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
        private int _currentBase = 10;

        public ProgrammerPage()
        {
            InitializeComponent();
            // Set default base to binary
            BinTextBox.IsReadOnly = false;
            BinTextBox.Text = "0";
        }

        // Metodă pentru a adăuga cifre la input în funcție de baza curentă
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                string digit = button.Content.ToString();
                if (IsValidDigit(digit))
                {
                    _currentInput += digit;
                    UpdateResultTextBox();
                }
            }
        }

        // Verifică dacă un digit este valid pentru baza curentă
        private bool IsValidDigit(string digit)
        {
            int value = Convert.ToInt32(digit, _currentBase);
            return value >= 0 && value < _currentBase;
        }

        // Actualizează TextBox-ul de rezultat în funcție de baza curentă
        private void UpdateResultTextBox()
        {
            ResultTextBox.Text = _currentInput;
        }

        // Selectează operatorul și setează primul număr
        private void Operator_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null && !string.IsNullOrEmpty(_currentInput))
            {
                _firstNumber = Convert.ToInt32(_currentInput, _currentBase);
                _operator = button.Content.ToString();
                PreviousTextBox.Text = $"{_firstNumber} {_operator}";
                _currentInput = string.Empty;
            }
        }

        // Șterge ultima cifră introdusă
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (_currentInput.Length > 0)
            {
                _currentInput = _currentInput.Remove(_currentInput.Length - 1);
                UpdateResultTextBox();
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
            PreviousTextBox.Text = string.Empty;
        }

        // Efectuează conversia în baza curentă și afișează rezultatul
        private void ShowResult(int number)
        {
            string result = Convert.ToString(number, _currentBase);
            ResultTextBox.Text = result;
            _currentInput = result;
            // Actualizează toate casetele de text pentru baze diferite
            UpdateAllBaseTextBoxes(number);
        }

        // Metodă pentru calcularea rezultatului
        private void Equals_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(_currentInput)) return;

            _secondNumber = Convert.ToInt32(_currentInput, _currentBase);
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

            PreviousTextBox.Text = $"{_firstNumber} {_operator} {_secondNumber} =";
            ShowResult(result);
        }

        // Operația NOT, aplicabilă pe un singur număr
        private void Not_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(_currentInput))
            {
                int number = Convert.ToInt32(_currentInput, _currentBase);
                int notResult = ~number; // Aplicăm operatorul bitwise NOT
                PreviousTextBox.Text = $"~{number}";
                ShowResult(notResult);
            }
        }

        // Schimbă baza curentă
        private void Base_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton != null)
            {
                _currentBase = int.Parse(radioButton.Tag.ToString());
                UpdateButtonGrid();
            }
        }

        // Actualizează butoanele disponibile în funcție de baza curentă
        private void UpdateButtonGrid()
        {
            // Implementați logica pentru a actualiza butoanele disponibile în funcție de baza curentă
            // De exemplu, dezactivați butoanele 2-9 pentru baza binară, 8-9 pentru baza octală, etc.
        }

        // Actualizează toate casetele de text pentru baze diferite
        private void UpdateAllBaseTextBoxes(int number)
        {
            HexTextBox.Text = Convert.ToString(number, 16);
            DecTextBox.Text = Convert.ToString(number, 10);
            OctTextBox.Text = Convert.ToString(number, 8);
            BinTextBox.Text = Convert.ToString(number, 2);
        }
    }
}