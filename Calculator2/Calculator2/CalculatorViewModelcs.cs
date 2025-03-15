using System.ComponentModel;
using System.Windows.Input;

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

            if (parameter == "C")
            {
                _model.Clear();
            }
            else if (parameter == "=")
            {
                _model.Calculate();
            }
            else
            {
                _model.Append(parameter);
            }

            Display = _model.Display;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}