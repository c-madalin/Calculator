using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows;

namespace Calculator2
{
    public class CalculatorViewModel : INotifyPropertyChanged
    {
        private string _display;
        private CalculatorModel _model;

        public CalculatorViewModel()
        {
            _model = new CalculatorModel();
            ButtonCommand = new RelayCommand(param => ExecuteButtonCommand(param?.ToString()));
            Display = "0";
        }

        public string Display
        {
            get => _display;
            set
            {
                _display = value;
                OnPropertyChanged("Display");
            }
        }

        public ICommand ButtonCommand { get; }

        private void ExecuteButtonCommand(string? parameter)
        {
            if (parameter == null) return;

            switch (parameter)
            {
                case "C":
                    _model.Clear();
                    break;
                case "=":
                    _model.Calculate();
                    break;
                case "M+":
                    _model.MemoryAdd();
                    break;
                case "M-":
                    _model.MemorySubtract();
                    break;
                case "MS":
                    _model.MemoryStore();
                    break;
                case "MR":
                    _model.MemoryRecall();
                    break;
                case "M>":
                    OpenMemoryWindow();
                    break;
                default:
                    _model.Append(parameter);
                    break;
            }

            Display = _model.Display;
        }

        private void OpenMemoryWindow()
        {
            var memoryStack = _model.GetMemoryStack();
            var currentDisplayValue = double.TryParse(Display, out double value) ? value : 0;
            MemoryWindow memoryWindow = new MemoryWindow(memoryStack, currentDisplayValue);
            memoryWindow.Show();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}