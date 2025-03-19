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
        private Stack<double> memoryStack = new Stack<double>();

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
                else
                {
                    MessageBox.Show($"Digit '{digit}' is not valid for base {_currentBase}.", "Invalid Digit", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        // Verifică dacă un digit este valid pentru baza curentă
        private bool IsValidDigit(string digit)
        {
            try
            {
                int value = Convert.ToInt32(digit, _currentBase);
                return value >= 0 && value < _currentBase;
            }
            catch (FormatException)
            {
                return false;
            }
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
            ClearAll();
        }

        // Adaugă o metodă pentru resetarea completă a calculatorului
        private void ClearAll()
        {
            _currentInput = string.Empty;
            _firstNumber = 0;
            _secondNumber = 0;
            _operator = string.Empty;
            ResultTextBox.Text = string.Empty;
            PreviousTextBox.Text = string.Empty;
            HexTextBox.Text = string.Empty;
            DecTextBox.Text = string.Empty;
            OctTextBox.Text = string.Empty;
            BinTextBox.Text = string.Empty;
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
                ClearAll();
                UpdateButtonGrid();
            }
        }

        // Actualizează butoanele disponibile în funcție de baza curentă
        private void UpdateButtonGrid()
        {
            foreach (Button button in ButtonGrid.Children)
            {
                string content = button.Content.ToString();
                if (int.TryParse(content, out int number))
                {
                    button.IsEnabled = number < _currentBase;
                }
                else
                {
                    button.IsEnabled = true;
                }
            }
        }

        // Actualizează toate casetele de text pentru baze diferite
        private void UpdateAllBaseTextBoxes(int number)
        {
            HexTextBox.Text = Convert.ToString(number, 16);
            DecTextBox.Text = Convert.ToString(number, 10);
            OctTextBox.Text = Convert.ToString(number, 8);
            BinTextBox.Text = Convert.ToString(number, 2);
        }
       

        // Funcția MS: Salvează valoarea din ResultTextBox în stiva
        private void MemorySaveClick(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(ResultTextBox.Text, out double value))
            {
                memoryStack.Push(value);  // Salvează valoarea în stiva
            }
            else
            {
                MessageBox.Show("Introduceți o valoare validă!");
            }
        }

        // Funcția M+: Adaugă valoarea din TextBox la valoarea de la topul stivei
        private void MemoryAddClick(object sender, RoutedEventArgs e)
        {
            if (memoryStack.Count > 0 && double.TryParse(ResultTextBox.Text, out double value))
            {
                double topValue = memoryStack.Peek();
                memoryStack.Push(topValue + value);  // Adaugă valoarea din TextBox la topul stivei
            }
            else
            {
                MessageBox.Show("Stiva este goală sau valoare invalidă!");
            }
        }

        // Funcția M-: Scade valoarea din TextBox de la valoarea de la topul stivei
        private void MemorySubstractClick(object sender, RoutedEventArgs e)
        {
            if (memoryStack.Count > 0 && double.TryParse(ResultTextBox.Text, out double value))
            {
                double topValue = memoryStack.Peek();
                memoryStack.Push(topValue - value);  // Scade valoarea din TextBox de la topul stivei
            }
            else
            {
                MessageBox.Show("Stiva este goală sau valoare invalidă!");
            }
        }

        // Funcția MR: Schimbă valoarea din TextBox cu valoarea de la topul stivei
        private void MemoryRecallComand(object sender, RoutedEventArgs e)
        {
            if (memoryStack.Count > 0)
            {
                ResultTextBox.Text = memoryStack.Peek().ToString();  // Setează valoarea topului stivei în TextBox
            }
            else
            {
                MessageBox.Show("Stiva este goală!");
            }
        }

        // Funcția MC: Șterge ultima valoare din stivă
        private void MemoryClearClick(object sender, RoutedEventArgs e)
        {
            if (memoryStack.Count > 0)
            {
                memoryStack.Pop();  // Șterge valoarea de la topul stivei
            }
            else
            {
                MessageBox.Show("Stiva este goală!");
            }
        }

        // Funcția Mv: Afișează o fereastră nouă pentru a vizualiza valoarea stivei
        private void MemoryViewClick(object sender, RoutedEventArgs e)
        {
            string memoryContent = "Stiva de memorie:\n";
            if (memoryStack.Count > 0)
            {
                foreach (var item in memoryStack)
                {
                    memoryContent += item + "\n";
                }
            }
            else
            {
                memoryContent = "Stiva este goală!";
            }

            MessageBox.Show(memoryContent, "Vizualizare Memorie");
        }
    }
}