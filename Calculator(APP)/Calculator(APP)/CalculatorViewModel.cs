using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Input;

namespace Calculator_APP_
{
    public class CalculatorViewModel : INotifyPropertyChanged
    {
        private CalculatorModel _calculatorModel;
        private MemoryModel _memoryModel;

        private string _currentInput;
        private string _lastOperation;
        private string _errorMessage;
        public ObservableCollection<MemoryItemViewModel> MemoryValues { get; }

        private CultureInfo _cultureInfo;

        public CultureInfo CultureInfo
        {
            get { return _cultureInfo; }
            set
            {
                _cultureInfo = value;
                OnPropertyChanged(nameof(CultureInfo));
            }
        }

        private bool _isDigitGroupingEnabled;

        public bool IsDigitGroupingEnabled
        {
            get { return _isDigitGroupingEnabled; }
            set
            {
                _isDigitGroupingEnabled = value;
                OnPropertyChanged(nameof(IsDigitGroupingEnabled));
                CurrentInput = FormatNumber(_currentInput);
            }
        }

        public string CurrentInput
        {
            get { return _currentInput; }
            set
            {
                _currentInput = FormatNumber(value);
                OnPropertyChanged(nameof(CurrentInput));
            }
        }

        public string LastOperation
        {
            get { return _lastOperation; }
            set
            {
                _lastOperation = value;
                OnPropertyChanged(nameof(LastOperation));
            }
        }

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        public ICommand AddCommand { get; }
        public ICommand SubtractCommand { get; }
        public ICommand MultiplyCommand { get; }
        public ICommand DivideCommand { get; }
        public ICommand EqualsCommand { get; }
        public ICommand NumberCommand { get; }
        public ICommand ClearCommand { get; }
        public ICommand ClearEntryCommand { get; }
        public ICommand BackspaceCommand { get; }
        public ICommand PercentCommand { get; }
        public ICommand SquareRootCommand { get; }
        public ICommand SquareCommand { get; }
        public ICommand ReciprocalCommand { get; }
        public ICommand PlusMinusCommand { get; }
        public ICommand MemoryAddCommand { get; }
        public ICommand MemorySubtractCommand { get; }
        public ICommand MemoryStoreCommand { get; }
        public ICommand MemoryRecallCommand { get; }
        public ICommand MemoryViewCommand { get; }
        public ICommand MemoryRemoveTopCommand { get; }

        public CalculatorViewModel()
        {
            _calculatorModel = new CalculatorModel();
            _memoryModel = new MemoryModel();
            MemoryValues = new ObservableCollection<MemoryItemViewModel>();
            AddCommand = new RelayCommand(ExecuteAdd);
            SubtractCommand = new RelayCommand(ExecuteSubtract);
            MultiplyCommand = new RelayCommand(ExecuteMultiply);
            DivideCommand = new RelayCommand(ExecuteDivide);
            EqualsCommand = new RelayCommand(ExecuteEquals);
            NumberCommand = new RelayCommand<string>(ExecuteNumber);
            ClearCommand = new RelayCommand(ExecuteClear);
            ClearEntryCommand = new RelayCommand(ExecuteClearEntry);
            BackspaceCommand = new RelayCommand(ExecuteBackspace);
            PercentCommand = new RelayCommand(ExecutePercent);
            SquareRootCommand = new RelayCommand(ExecuteSquareRoot);
            SquareCommand = new RelayCommand(ExecuteSquare);
            ReciprocalCommand = new RelayCommand(ExecuteReciprocal);
            PlusMinusCommand = new RelayCommand(ExecutePlusMinus);
            MemoryAddCommand = new RelayCommand(ExecuteMemoryAdd);
            MemorySubtractCommand = new RelayCommand(ExecuteMemorySubtract);
            MemoryStoreCommand = new RelayCommand(ExecuteMemoryStore);
            MemoryRecallCommand = new RelayCommand(ExecuteMemoryRecall);
            MemoryViewCommand = new RelayCommand(ExecuteMemoryView);
            MemoryRemoveTopCommand = new RelayCommand(ExecuteMemoryRemoveTop);
            CurrentInput = string.Empty;

            CultureInfo = CultureInfo.CurrentCulture;
            IsDigitGroupingEnabled = true;
        }

        private string FormatNumber(string number)
        {
            if (!IsDigitGroupingEnabled)
            {
                return number;
            }

            var culture = CultureInfo;
            if (long.TryParse(number, NumberStyles.AllowThousands, culture, out long num))
            {
                return num.ToString("N0", culture);
            }
            else
            {
                return number;
            }
        }

        public void ExecuteAdd(object parameter)
        {
            PerformIntermediateCalculation();
            _calculatorModel.SetOperation("+");
            CurrentInput = string.Empty;
            LastOperation = $"{_calculatorModel.Result} +";
        }

        public void ExecuteSubtract(object parameter)
        {
            PerformIntermediateCalculation();
            _calculatorModel.SetOperation("-");
            CurrentInput = string.Empty;
            LastOperation = $"{_calculatorModel.Result} -";
        }

        public void ExecuteMultiply(object parameter)
        {
            PerformIntermediateCalculation();
            _calculatorModel.SetOperation("*");
            CurrentInput = string.Empty;
            LastOperation = $"{_calculatorModel.Result} *";
        }

        public void ExecuteDivide(object parameter)
        {
            if (double.TryParse(CurrentInput, out double value) && value == 0)
            {
                ErrorMessage = "Eroare: Împărțire la zero!";
                return;
            }

            PerformIntermediateCalculation();
            _calculatorModel.SetOperation("/");
            CurrentInput = string.Empty;
            LastOperation = $"{_calculatorModel.Result} ÷";
        }


        public void ExecuteEquals(object parameter)
        {
            if (double.TryParse(CurrentInput, out double num))
            {
                _calculatorModel.SetNumber(num);
            }
            _calculatorModel.CalculateResult();
            if (_calculatorModel.Error != null)
            {
                ErrorMessage = _calculatorModel.Error;
                CurrentInput = "Error";
            }
            else
            {
                CurrentInput = FormatNumber(_calculatorModel.Result.ToString());
                LastOperation = $"{_calculatorModel.Result}";
                ErrorMessage = null;
            }
        }

        public void ExecuteNumber(string number)
        {
            if (_currentInput == "0" || _currentInput == string.Empty || _currentInput == "Error")
            {
                CurrentInput = number;
            }
            else
            {
                CurrentInput += number;
            }
        }

        public void ExecuteClear(object parameter)
        {
            _calculatorModel.Clear();
            CurrentInput = string.Empty;
            LastOperation = string.Empty;
            ErrorMessage = null;
        }

        private void ExecuteClearEntry(object parameter)
        {
            _calculatorModel.ClearEntry();
            CurrentInput = string.Empty;
            ErrorMessage = null;
        }

        public void ExecuteBackspace(object parameter)
        {
            if (CurrentInput.Length > 0)
            {
                CurrentInput = CurrentInput.Substring(0, CurrentInput.Length - 1);
            }
        }

        private void ExecutePercent(object parameter)
        {
            PerformIntermediateCalculation();
            _calculatorModel.SetOperation("%");
            CurrentInput = string.Empty;
            LastOperation = $"{_calculatorModel.Result} %";
        }

        private void ExecuteSquareRoot(object parameter)
        {
            PerformIntermediateCalculation();
            _calculatorModel.SetOperation("√");
            _calculatorModel.CalculateResult();
            if (_calculatorModel.Error != null)
            {
                ErrorMessage = _calculatorModel.Error;
                CurrentInput = "Error";
            }
            else
            {
                CurrentInput = FormatNumber(_calculatorModel.Result.ToString());
                LastOperation = $"√{_calculatorModel.Result}";
                ErrorMessage = null;
            }
        }

        private void ExecuteSquare(object parameter)
        {
            PerformIntermediateCalculation();
            _calculatorModel.SetOperation("x²");
            _calculatorModel.CalculateResult();
            if (_calculatorModel.Error != null)
            {
                ErrorMessage = _calculatorModel.Error;
                CurrentInput = "Error";
            }
            else
            {
                CurrentInput = FormatNumber(_calculatorModel.Result.ToString());
                LastOperation = $"{_calculatorModel.Result}²";
                ErrorMessage = null;
            }
        }

        private void ExecuteReciprocal(object parameter)
        {
            PerformIntermediateCalculation();
            _calculatorModel.SetOperation("1/x");
            _calculatorModel.CalculateResult();
            if (_calculatorModel.Error != null)
            {
                ErrorMessage = _calculatorModel.Error;
                CurrentInput = "Error";
            }
            else
            {
                CurrentInput = FormatNumber(_calculatorModel.Result.ToString());
                LastOperation = $"1/{_calculatorModel.Result}";
                ErrorMessage = null;
            }
        }

        private void ExecutePlusMinus(object parameter)
        {
            if (double.TryParse(CurrentInput, out double num))
            {
                _calculatorModel.SetNumber(num);
                _calculatorModel.SetOperation("+/-");
                _calculatorModel.CalculateResult();
                if (_calculatorModel.Error != null)
                {
                    ErrorMessage = _calculatorModel.Error;
                    CurrentInput = "Error";
                }
                else
                {
                    CurrentInput = FormatNumber(_calculatorModel.Result.ToString());
                    LastOperation = $"±{_calculatorModel.Result}";
                    ErrorMessage = null;
                }
            }
        }

        private void PerformIntermediateCalculation()
        {
            if (double.TryParse(CurrentInput, out double num))
            {
                _calculatorModel.SetNumber(num);
                CurrentInput = FormatNumber(_calculatorModel.Result.ToString());
            }
        }

        public void ProcessKeyboardInput(string input)
        {
            ExecuteNumber(input);
        }

        // Memory commands
        private void ExecuteMemoryAdd(object parameter)
        {
            if (double.TryParse(CurrentInput, out double num))
            {
                _memoryModel.AddToMemory(num);
                RefreshMemoryValues();
            }
        }

        private void ExecuteMemorySubtract(object parameter)
        {
            if (double.TryParse(CurrentInput, out double num))
            {
                _memoryModel.SubtractFromMemory(num);
                RefreshMemoryValues();
            }
        }

        private void ExecuteMemoryStore(object parameter)
        {
            if (double.TryParse(CurrentInput, out double num))
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
                CurrentInput = FormatNumber(memoryValue.Value.ToString());
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
            MemoryValues.Clear();
            foreach (var value in _memoryModel.MemoryStack)
            {
                MemoryValues.Add(new MemoryItemViewModel(value, _memoryModel, this));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}