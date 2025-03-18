using System.Collections.Generic;

namespace Calculator_APP_   
{
    public class MemoryModel
    {
        private Stack<double> _memoryStack = new Stack<double>();

        public IEnumerable<double> MemoryStack => _memoryStack;

        public void AddToMemory(double value)
        {
            if (_memoryStack.Count > 0)
            {
                _memoryStack.Push(_memoryStack.Pop() + value);
            }
            else
            {
                _memoryStack.Push(+value);
            }
        }

        public void SubtractFromMemory(double value)
        {
            if (_memoryStack.Count > 0)
            {
                _memoryStack.Push(_memoryStack.Pop() - value);
            }
            else
            {
                _memoryStack.Push(-value);
            }
        }

        public void UpdateMemory(double oldValue, double newValue)
        {
            var tempStack = new Stack<double>();
            bool updated = false;

            while (_memoryStack.Count > 0)
            {
                var value = _memoryStack.Pop();
                if (value == oldValue && !updated)
                {
                    tempStack.Push(newValue);
                    updated = true;
                }
                else
                {
                    tempStack.Push(value);
                }
            }

            while (tempStack.Count > 0)
            {
                _memoryStack.Push(tempStack.Pop());
            }
        }

        public void RemoveTopOfMemory()
        {
            if (_memoryStack.Count > 0)
            {
                _memoryStack.Pop();
            }
        }

        public void RemoveSpecificValue(double value)
        {
            var tempStack = new Stack<double>();

            while (_memoryStack.Count > 0)
            {
                var current = _memoryStack.Pop();
                if (current != value)
                {
                    tempStack.Push(current);
                }
            }

            while (tempStack.Count > 0)
            {
                _memoryStack.Push(tempStack.Pop());
            }
        }

        public double? RecallMemory()
        {
            if (_memoryStack.Count > 0)
            {
                return _memoryStack.Peek();
            }
            return null;
        }

        public void StoreMemory(double value)
        {
            _memoryStack.Push(value);
        }
    }
}