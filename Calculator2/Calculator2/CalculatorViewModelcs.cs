using System.ComponentModel;
using System.Windows.Input;

namespace Calculator2
{
    public class CalculatorViewModel : INotifyPropertyChanged
    {
        private CalculatorModel _calculatorModel;

        private string _number1;
        public string Number1
        {
            get { return _number1; }
            set
            {
                _number1 = value;
                OnPropertyChanged(nameof(Number1));
            }
        }

        private string _number2;
        public string Number2
        {
            get { return _number2; }
            set
            {
                _number2 = value;
                OnPropertyChanged(nameof(Number2));
            }
        }

        private double _result;
        public double Result
        {
            get { return _result; }
            set
            {
                _result = value;
                OnPropertyChanged(nameof(Result));
            }
        }

        public ICommand AddCommand { get; }
        public ICommand MinusCommand { get; }
        public ICommand NumberCommand { get; }

        public CalculatorViewModel()
        {
            _calculatorModel = new CalculatorModel();
            AddCommand = new RelayCommand(ExecuteAdd, CanExecuteAdd);
            MinusCommand = new RelayCommand(ExecuteMinus, CanExecuteMinus);
            NumberCommand = new RelayCommand<string>(ExecuteNumber);
            Number1 = string.Empty;
            Number2 = string.Empty;
        }

        private void ExecuteAdd(object parameter)
        {
            if (double.TryParse(Number1, out double num1) && double.TryParse(Number2, out double num2))
            {
                _calculatorModel.Add(num1, num2);
                Result = _calculatorModel.Result;
            }
        }

        private bool CanExecuteAdd(object parameter)
        {
            return true; // Poți adăuga logica de validare aici
        }

        private void ExecuteMinus(object parameter)
        {
            if (double.TryParse(Number1, out double num1) && double.TryParse(Number2, out double num2))
            {
                _calculatorModel.Minus(num1, num2);
                Result = _calculatorModel.Result;
            }
        }

        private bool CanExecuteMinus(object parameter)
        {
            return true; // Poți adăuga logica de validare aici
        }

        private void ExecuteNumber(string number)
        {
            // Concatenate the number to the appropriate TextBox
            if (string.IsNullOrEmpty(Number2))
            {
                Number1 += number;
            }
            else
            {
                Number2 += number;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}