using System.Windows.Input;

namespace Calculator2
{
    public class MemoryItemViewModel
    {
        private readonly MemoryModel _memoryModel;
        private readonly CalculatorViewModel _calculatorViewModel;

        public double Value { get; }

        public ICommand MemoryAddCommand { get; }
        public ICommand MemorySubtractCommand { get; }
        public ICommand MemoryRemoveCommand { get; }

        public MemoryItemViewModel(double value, MemoryModel memoryModel, CalculatorViewModel calculatorViewModel)
        {
            Value = value;
            _memoryModel = memoryModel;
            _calculatorViewModel = calculatorViewModel;

            MemoryAddCommand = new RelayCommand(ExecuteMemoryAdd);
            MemorySubtractCommand = new RelayCommand(ExecuteMemorySubtract);
            MemoryRemoveCommand = new RelayCommand(ExecuteMemoryRemove);
        }

        private void ExecuteMemoryAdd(object parameter)
        {
            if (double.TryParse(_calculatorViewModel.CurrentInput, out double num))
            {
                _memoryModel.UpdateMemory(Value, Value + num);
                _calculatorViewModel.RefreshMemoryValues();
            }
        }

        private void ExecuteMemorySubtract(object parameter)
        {
            if (double.TryParse(_calculatorViewModel.CurrentInput, out double num))
            {
                _memoryModel.UpdateMemory(Value, Value - num);
                _calculatorViewModel.RefreshMemoryValues();
            }
        }

        private void ExecuteMemoryRemove(object parameter)
        {
            _memoryModel.RemoveSpecificValue(Value);
            _calculatorViewModel.RefreshMemoryValues();
        }
    }
}