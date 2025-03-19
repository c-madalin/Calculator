using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
        private MemoryModel _memoryModel = new MemoryModel();

        public ProgrammerPage()
        {
            InitializeComponent();
            BinTextBox.IsReadOnly = false;
            BinTextBox.Text = "0";
            UpdateButtonGrid();

            MemoryAddCommand = new RelayCommand(ExecuteMemoryAdd);
            MemorySubtractCommand = new RelayCommand(ExecuteMemorySubtract);
            MemoryStoreCommand = new RelayCommand(ExecuteMemoryStore);
            MemoryRecallCommand = new RelayCommand(ExecuteMemoryRecall);
            MemoryViewCommand = new RelayCommand(ExecuteMemoryView);
            MemoryRemoveTopCommand = new RelayCommand(ExecuteMemoryRemoveTop);
        }

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

        // Memory commands
        private void ExecuteMemoryAdd(object parameter)
        {
            if (double.TryParse(_currentInput, out double num))
            {
                _memoryModel.AddToMemory(num);
                RefreshMemoryValues();
            }
        }

        private void ExecuteMemorySubtract(object parameter)
        {
            if (double.TryParse(_currentInput, out double num))
            {
                _memoryModel.SubtractFromMemory(num);
                RefreshMemoryValues();
            }
        }

        private void ExecuteMemoryStore(object parameter)
        {
            if (double.TryParse(_currentInput, out double num))
            {
                _memoryModel.StoreMemory(num);
                RefreshMemoryValues();
            }
        }

        private void ExecuteMemoryRecall(object parameter)
        {
            var memoryValue = _memoryModel.RecallMemory();
            if (memoryValue.HasValue)
            {
                _currentInput = memoryValue.Value.ToString();
                UpdateResultTextBox();
            }
        }

        private void ExecuteMemoryView(object parameter)
        {
            var memoryWindow = new MemoryWindow { DataContext = this };
            memoryWindow.Show();
        }

        private void ExecuteMemoryRemoveTop(object parameter)
        {
            _memoryModel.RemoveTopOfMemory();
            RefreshMemoryValues();
        }

        public void RefreshMemoryValues()
        {
            // Refresh logic for memory values
            // This could update UI elements or internal properties
        }

        // Initialize memory commands
        public ICommand MemoryAddCommand { get; }
        public ICommand MemorySubtractCommand { get; }
        public ICommand MemoryStoreCommand { get; }
        public ICommand MemoryRecallCommand { get; }
        public ICommand MemoryViewCommand { get; }
        public ICommand MemoryRemoveTopCommand { get; }

        // Event handlers for memory buttons
        private void MemoryClearClick(object sender, RoutedEventArgs e) => ExecuteMemoryRemoveTop(null);
        private void MemoryRecallComand(object sender, RoutedEventArgs e) => ExecuteMemoryRecall(null);
        private void MemoryAddClick(object sender, RoutedEventArgs e) => ExecuteMemoryAdd(null);
        private void MemorySubstractClick(object sender, RoutedEventArgs e) => ExecuteMemorySubtract(null);
        private void MemorySaveClick(object sender, RoutedEventArgs e) => ExecuteMemoryStore(null);
        private void MemoryViewClick(object sender, RoutedEventArgs e) => ExecuteMemoryView(null);

        // Handle keyboard input
        private void Page_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9)
            {
                string digit = (e.Key - Key.D0).ToString();
                Button_Click(new Button { Content = digit }, null);
            }
            else if (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
            {
                string digit = (e.Key - Key.NumPad0).ToString();
                Button_Click(new Button { Content = digit }, null);
            }
            else if (e.Key == Key.Add)
            {
                Operator_Click(new Button { Content = "+" }, null);
            }
            else if (e.Key == Key.Subtract)
            {
                Operator_Click(new Button { Content = "-" }, null);
            }
            else if (e.Key == Key.Multiply)
            {
                Operator_Click(new Button { Content = "*" }, null);
            }
            else if (e.Key == Key.Divide)
            {
                Operator_Click(new Button { Content = "/" }, null);
            }
            else if (e.Key == Key.Enter)
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
        }
    }
}