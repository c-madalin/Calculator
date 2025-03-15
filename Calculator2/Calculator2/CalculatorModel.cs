using System;
using System.Data;

namespace Calculator2
{
    public class CalculatorModel
    {
        private string _currentInput;

        public CalculatorModel()
        {
            Clear();
        }

        public string Display { get; private set; }

        public void Append(string input)
        {
            if (Display == "0" && input != ".")
            {
                Display = input;
            }
            else
            {
                Display += input;
            }

            _currentInput += input;
        }

        public void Calculate()
        {
            try
            {
                var result = new DataTable().Compute(_currentInput, null);
                Display = result.ToString();
                _currentInput = Display;
            }
            catch
            {
                Display = "Error";
                _currentInput = string.Empty;
            }
        }

        public void Clear()
        {
            Display = "0";
            _currentInput = string.Empty;
        }
    }
}