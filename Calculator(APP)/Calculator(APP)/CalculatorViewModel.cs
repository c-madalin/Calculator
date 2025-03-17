using System.ComponentModel;
using System.Web;
using System.Windows.Input;

namespace Calculator_APP_
{
    public class CalculatorViewModel : INotifyPropertyChanged
    {
        private CalculatorModel _model;
        private string _number1;
        private string _number2;
        private double _result;
        private string currentDisplay = "0";
        private string currentEquation = "";


        //public ICommand
        public ICommand AddCommand { get; }
        public ICommand MinusCommand { get; }
        public ICommand NumberCommand { get; }


        //Constructor
        public CalculatorViewModel()
        {
            _model = new CalculatorModel();
            AddCommand = new RelayCommand(ExecuteAdd);
           // MinusCommand = new RelayCommand(Minus);
            //NumberCommand = new RelayCommand(AddNumber);

        }

        private void ExecuteAdd(object parameter)
        {
            if (double.TryParse(Number1, out double num1) && double.TryParse(Number2, out double num2))
            {
                _model.Add(num1, num2);
                Result = _model.Result;
            }
        }

        //Properties
        public string Number1
        {
            get { return _number1; }
            set
            {
                _number1 = value;
                OnPropertyChanged(nameof(Number1));
            }
        }
        public string Number2
        {
            get { return _number2; }
            set
            {
                _number2 = value;
                OnPropertyChanged(nameof(Number2));
            }
        }

        public double Result
        {
            get { return _result; }
            set
            {
                _result = value;
                OnPropertyChanged(nameof(Result));
            }
        }

        public string CurrentDisplay
        {
            get { return currentDisplay; }
            set
            {
                currentDisplay = value;
                OnPropertyChanged("CurrentDisplay");
            }
        }

        public string CurrentEquation
        {
            get { return currentEquation; }
            set
            {
                currentEquation = value;
                OnPropertyChanged("CurrentEquation");
            }
        }

     



        private void Clear()
        {
            _model.Clear();
            CurrentDisplay = "0";
            CurrentEquation = string.Empty;
        }

        private void AddNumber(string number)
        {
            if (CurrentDisplay == "0")
                CurrentDisplay = number;
            else
                CurrentDisplay += number;
        }

      
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}