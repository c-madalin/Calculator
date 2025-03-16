using System.Collections.Generic;
using System.ComponentModel;

namespace Calculator2
{
    public class MemoryManager : INotifyPropertyChanged
    {
        private List<double> _memoryStack = new List<double>();
        private double _currentMemory = 0;

        public MemoryManager(IEnumerable<double> memoryStack)
        {
            _memoryStack = new List<double>(memoryStack);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public double CurrentMemory
        {
            get { return _currentMemory; }
            private set
            {
                if (_currentMemory != value)
                {
                    _currentMemory = value;
                    OnPropertyChanged(nameof(CurrentMemory));
                }
            }
        }

        public List<double> MemoryStack
        {
            get { return _memoryStack; }
        }

        public void AddToMemory(double value)
        {
            CurrentMemory += value;
            OnPropertyChanged(nameof(CurrentMemory));
        }

        public void SubtractFromMemory(double value)
        {
            CurrentMemory -= value;
            OnPropertyChanged(nameof(CurrentMemory));
        }

        public void StoreInMemory(double value)
        {
            CurrentMemory = value;
            OnPropertyChanged(nameof(CurrentMemory));
        }

        public double RecallMemory()
        {
            return CurrentMemory;
        }

        public void PushMemory()
        {
            _memoryStack.Add(CurrentMemory);
            OnPropertyChanged(nameof(MemoryStack));
        }

        public List<double> GetMemoryStack()
        {
            return new List<double>(_memoryStack);
        }

        public void ClearMemory()
        {
            CurrentMemory = 0;
            _memoryStack.Clear();
            OnPropertyChanged(nameof(MemoryStack));
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}