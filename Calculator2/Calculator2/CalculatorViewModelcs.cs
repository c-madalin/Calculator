using System.ComponentModel;
using System.Windows.Input;

namespace Calculator2
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

        public ICommand AddCommand { get; }
        public ICommand DivideCommand { get; }
        public ICommand MinusCommand { get; }
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
            CurrentInput = string.Empty;
        }

        private void ExecuteDivide(object obj)
        {
            PerformIntermediateCalculation();
            _calculatorModel.SetOperation("/");
            CurrentInput = string.Empty;
        }

        private void ExecuteMinus(object parameter)
        {
            PerformIntermediateCalculation();
            _calculatorModel.SetOperation("-");
            CurrentInput = string.Empty;
        }

        private void ExecuteAdd(object parameter)
        {
            PerformIntermediateCalculation();
            _calculatorModel.SetOperation("+");
            CurrentInput = string.Empty;
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
            PerformIntermediateCalculation();

        }



        private void ExecuteNumber(string number)
        {
            CurrentInput += number;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}