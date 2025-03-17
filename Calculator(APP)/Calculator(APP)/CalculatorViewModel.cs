using System.ComponentModel;
using System.Windows.Input;

namespace Calculator_APP_
{
    public class CalculatorViewModel : INotifyPropertyChanged
    {
        private CalculatorModel _calculatorModel;

        private string _currentInput;
        public string CurrentInput
        {
            get { return _currentInput; }
            set
            {
                _currentInput = value;
                OnPropertyChanged(nameof(CurrentInput));
            }
        }

        private string _lastOperation;
        public string LastOperation
        {
            get { return _lastOperation; }
            set
            {
                _lastOperation = value;
                OnPropertyChanged(nameof(LastOperation));
            }
        }

        public ICommand AddCommand { get; }
        public ICommand DivideCommand { get; }
        public ICommand MinusCommand { get; }
        public ICommand MultiplyCommand { get; }
        public ICommand EqualsCommand { get; }
        public ICommand NumberCommand { get; }

        public CalculatorViewModel()
        {
            _calculatorModel = new CalculatorModel();
            AddCommand = new RelayCommand(ExecuteAdd);
            EqualsCommand = new RelayCommand(ExecuteEquals);
            NumberCommand = new RelayCommand<string>(ExecuteNumber);
            MinusCommand = new RelayCommand(ExecuteMinus);
            DivideCommand = new RelayCommand(ExecuteDivide);
            MultiplyCommand = new RelayCommand(ExecuteMultiply);
            CurrentInput = string.Empty;
        }

        private void ExecuteDivide(object obj)
        {
            PerformIntermediateCalculation();
            _calculatorModel.SetOperation("/");
            CurrentInput = string.Empty;
            LastOperation = $"{_calculatorModel.Result} /";
        }

        private void ExecuteMultiply(object obj)
        {
            PerformIntermediateCalculation();
            _calculatorModel.SetOperation("*");
            CurrentInput = string.Empty;
            LastOperation = $"{_calculatorModel.Result} *";
        }

        private void ExecuteMinus(object parameter)
        {
            PerformIntermediateCalculation();
            _calculatorModel.SetOperation("-");
            CurrentInput = string.Empty;
            LastOperation = $"{_calculatorModel.Result} -";
        }

        private void ExecuteAdd(object parameter)
        {
            PerformIntermediateCalculation();
            _calculatorModel.SetOperation("+");
            CurrentInput = string.Empty;
            LastOperation = $"{_calculatorModel.Result} +";
        }

        private void PerformIntermediateCalculation()
        {
            if (double.TryParse(CurrentInput, out double num))
            {
                _calculatorModel.SetNumber(num);
                CurrentInput = _calculatorModel.Result.ToString();
            }
        }

        private void ExecuteEquals(object parameter)
        {
            if (double.TryParse(CurrentInput, out double num))
            {
                _calculatorModel.SetNumber(num);
            }
            _calculatorModel.CalculateResult();
            CurrentInput = _calculatorModel.Result.ToString();
            LastOperation = $"{_calculatorModel.Result}";
        }

        private void ExecuteNumber(string number)
        {
            if (_currentInput == "0" || _currentInput == string.Empty)
            {
                CurrentInput = number;
            }
            else
            {
                CurrentInput += number;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}