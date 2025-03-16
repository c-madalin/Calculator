using System;
using System.Data;

namespace Calculator2
{
    public class CalculatorModel
    {
        private string _currentInput;
        private char? _lastOperator;
        private readonly MemoryManager _memoryManager;

        public CalculatorModel()
        {
            Clear();
            _memoryManager = new MemoryManager(new List<double>());
        }
        public string Display { get; private set; }

        public void Append(string input)
        {
            if (IsOperator(input))
            {
                if (_lastOperator.HasValue)
                {
                    // Replace the last operator with the new one
                    _currentInput = _currentInput.TrimEnd(_lastOperator.Value) + input;
                }
                else
                {
                    _currentInput += input;
                }
                _lastOperator = input[0];
            }
            else
            {
                _currentInput += input;
                _lastOperator = null;
            }

            Display = _currentInput;
        }

        public void Calculate()
        {
            try
            {
                var result = new DataTable().Compute(_currentInput, null);
                Display = result.ToString();
                _currentInput = Display;
                _lastOperator = null;
            }
            catch
            {
                Display = "Error";
                _currentInput = string.Empty;
                _lastOperator = null;
            }
        }

        public void Clear()
        {
            Display = "0";
            _currentInput = string.Empty;
            _lastOperator = null;
        }

        private bool IsOperator(string input)
        {
            return input == "+" || input == "-" || input == "*" || input == "/";
        }

        public void MemoryAdd()
        {
            if (double.TryParse(Display, out double value))
            {
                _memoryManager.AddToMemory(value);
            }
        }

        public void MemorySubtract()
        {
            if (double.TryParse(Display, out double value))
            {
                _memoryManager.SubtractFromMemory(value);
            }
        }

        public void MemoryStore()
        {
            if (double.TryParse(Display, out double value))
            {
                _memoryManager.StoreInMemory(value);
            }
        }

        public void MemoryRecall()
        {
            Display = _memoryManager.RecallMemory().ToString();
            _currentInput = Display;
        }

        public void MemoryPush()
        {
            _memoryManager.PushMemory();
        }

        public void MemoryClear()
        {
            _memoryManager.ClearMemory();
        }

        public List<double> GetMemoryStack()
        {
            return _memoryManager.GetMemoryStack();
        }
    }
}