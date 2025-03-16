using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace Calculator2
{
    public class MemoryViewModel : INotifyPropertyChanged
    {
        private readonly MemoryManager _memoryManager;
        private readonly double _currentDisplayValue;

        public ObservableCollection<double> MemoryStack { get; }

        public MemoryViewModel(MemoryManager memoryManager, double currentDisplayValue)
        {
            _memoryManager = memoryManager;
            _currentDisplayValue = currentDisplayValue;
            _memoryManager.PropertyChanged += MemoryManager_PropertyChanged;
            MemoryStack = new ObservableCollection<double>(_memoryManager.GetMemoryStack());
            MemoryClearCommand = new RelayCommand(param => ClearMemoryItem((double)param));
            MemoryAddCommand = new RelayCommand(param => AddToMemoryItem((double)param));
            MemorySubtractCommand = new RelayCommand(param => SubtractFromMemoryItem((double)param));
        }

        public ICommand MemoryClearCommand { get; }
        public ICommand MemoryAddCommand { get; }
        public ICommand MemorySubtractCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void MemoryManager_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MemoryManager.MemoryStack))
            {
                MemoryStack.Clear();
                foreach (var item in _memoryManager.GetMemoryStack())
                {
                    MemoryStack.Add(item);
                }
            }
        }

        private void ClearMemoryItem(double value)
        {
            _memoryManager.MemoryStack.Remove(value);
            MemoryStack.Remove(value);
        }

        private void AddToMemoryItem(double value)
        {
            _memoryManager.AddToMemory(_currentDisplayValue);
        }

        private void SubtractFromMemoryItem(double value)
        {
            _memoryManager.SubtractFromMemory(_currentDisplayValue);
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}