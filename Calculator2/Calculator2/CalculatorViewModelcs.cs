using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Calculator2
{
    public class CalculatorViewModel : INotifyPropertyChanged
    {
        private CalculatorModel _calculatorModel;
        private MemoryModel _memoryModel;

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

        public ObservableCollection<MemoryItemViewModel> MemoryValues { get; }

        public ICommand AddCommand { get; }
        public ICommand DivideCommand { get; }
        public ICommand MinusCommand { get; }
        public ICommand EqualsCommand { get; }
        public ICommand NumberCommand { get; }
        public ICommand HexNumberCommand { get; }
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
            EqualsCommand = new RelayCommand(ExecuteEquals);
            NumberCommand = new RelayCommand<string>(ExecuteNumber);
            HexNumberCommand = new RelayCommand<string>(ExecuteHexNumber);
            MinusCommand = new RelayCommand(ExecuteMinus);
            DivideCommand = new RelayCommand(ExecuteDivide);
            MemoryAddCommand = new RelayCommand(ExecuteMemoryAdd);
            MemorySubtractCommand = new RelayCommand(ExecuteMemorySubtract);
            MemoryStoreCommand = new RelayCommand(ExecuteMemoryStore);
            MemoryRecallCommand = new RelayCommand(ExecuteMemoryRecall);
            MemoryViewCommand = new RelayCommand(ExecuteMemoryView);
            MemoryRemoveTopCommand = new RelayCommand(ExecuteMemoryRemoveTop);
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
            if (double.TryParse(CalculatorModel.ConvertHexToDecimal(CurrentInput).ToString(), out double num))
            {
                _calculatorModel.SetNumber(num);
                CurrentInput = CalculatorModel.ConvertDecimalToHex(_calculatorModel.Result);
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

        private void ExecuteHexNumber(string hexNumber)
        {
            CurrentInput += hexNumber;
        }

        private void ExecuteMemoryAdd(object parameter)
        {
            if (double.TryParse(CalculatorModel.ConvertHexToDecimal(CurrentInput).ToString(), out double num))
            {
                _memoryModel.AddToMemory(num);
                RefreshMemoryValues();
            }
        }

        private void ExecuteMemorySubtract(object parameter)
        {
            if (double.TryParse(CalculatorModel.ConvertHexToDecimal(CurrentInput).ToString(), out double num))
            {
                _memoryModel.SubtractFromMemory(num);
                RefreshMemoryValues();
            }
        }

        private void ExecuteMemoryStore(object parameter)
        {
            if (double.TryParse(CalculatorModel.ConvertHexToDecimal(CurrentInput).ToString(), out double num))
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
                CurrentInput = CalculatorModel.ConvertDecimalToHex(memoryValue.Value);
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