using System.Collections.Generic;

namespace Calculator2
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
                _memoryStack.Push(value);
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

        public void RemoveTopOfMemory()
        {
            if (_memoryStack.Count > 0)
            {
                _memoryStack.Pop();
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