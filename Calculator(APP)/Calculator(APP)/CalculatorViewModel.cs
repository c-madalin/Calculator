using System.ComponentModel;
using System.Windows.Input;

namespace Calculator_APP_
{
    public class CalculatorViewModel : INotifyPropertyChanged
    {
        private CalculatorModel model;

        public CalculatorViewModel()
        {   

            model = new CalculatorModel();
            ClearCommand = new RelayCommand(Clear);
            NumberCommand = new RelayCommand<string>(AddNumber);
            OperatorCommand = new RelayCommand<string>(SetOperator);
            CalculateCommand = new RelayCommand(Calculate);
            MemoryStoreCommand = new RelayCommand(MemoryStore);
            MemoryRecallCommand = new RelayCommand(MemoryRecall);
            MemoryClearCommand = new RelayCommand(MemoryClear);
            MemoryAddCommand = new RelayCommand(MemoryAdd);
            MemorySubtractCommand = new RelayCommand(MemorySubtract);

        }

        private string currentDisplay;
        public string CurrentDisplay
        {
            get { return currentDisplay; }
            set
            {
                currentDisplay = value;
                OnPropertyChanged("CurrentDisplay");
            }
        }
        private string _currentEquation = "";
        public string CurrentEquation
        {
            get { return _currentEquation; }
            set
            {
                _currentEquation = value;
                OnPropertyChanged(nameof(CurrentEquation));
            }
        }
        private string _lastInput = "";
        public string LastInput
        {
            get { return _lastInput; }
            set
            {
                _lastInput = value;
                OnPropertyChanged(nameof(LastInput));
            }
        }



        public ICommand ClearCommand { get; private set; }
        public ICommand NumberCommand { get; private set; }
        public ICommand OperatorCommand { get; private set; }
        public ICommand CalculateCommand { get; private set; }
        public ICommand MemoryStoreCommand { get; private set; }
        public ICommand MemoryRecallCommand { get; private set; }
        public ICommand MemoryClearCommand { get; private set; }
        public ICommand MemoryAddCommand { get; private set; }
        public ICommand MemorySubtractCommand { get; private set; }

        private void Clear()
        {
            model.Clear();
            CurrentDisplay = "0";
        }

        private void AddNumber(string number)
        {
            CurrentDisplay += number;
        }

        private void SetOperator(string operatorSymbol)
        {
            if (!string.IsNullOrEmpty(CurrentDisplay))
            {
                model.Calculate(double.Parse(CurrentDisplay));
                CurrentDisplay = model.CurrentValue.ToString();
                model.SetOperator(operatorSymbol);
                CurrentDisplay = string.Empty;
            }
        }

        private void Calculate()
        {
            if (!string.IsNullOrEmpty(CurrentDisplay))
            {
                model.Calculate(double.Parse(CurrentDisplay));
                CurrentDisplay = model.CurrentValue.ToString();
            }
        }

        private void MemoryStore()
        {
            model.MemoryStore();
        }

        private void MemoryRecall()
        {
            CurrentDisplay = model.MemoryRecall().ToString();
        }

        private void MemoryClear()
        {
            model.MemoryClear();
        }

        private void MemoryAdd()
        {
            model.MemoryAdd(double.Parse(CurrentDisplay));
        }

        private void MemorySubtract()
        {
            model.MemorySubtract(double.Parse(CurrentDisplay));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}


   