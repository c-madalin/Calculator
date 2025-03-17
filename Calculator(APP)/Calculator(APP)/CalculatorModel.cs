namespace Calculator_APP_
{
    public class CalculatorModel
    {
        private double _currentValue;
        private string _operation;
        private double _lastNumber;
        private string _lastOperation;

        public double Result { get; private set; }
        public string Error { get; private set; }
        private bool isRepeatedEqual = false;

        public void SetOperation(string operation)
        {
            _operation = operation;
            isRepeatedEqual = false;
            Error = null; // Resetăm mesajul de eroare
        }

        public void SetNumber(double number)
        {
            if (_operation == null)
            {
                Result = number;
            }
            else
            {
                _currentValue = number;
                CalculateResult();
                _lastNumber = number;
                _lastOperation = _operation;
            }
        }

        public void CalculateResult()
        {
            if (_operation == null)
            {
                if (_lastOperation != null)
                {
                    _operation = _lastOperation;
                    _currentValue = _lastNumber;
                }
                else
                {
                    return;
                }
            }

            switch (_operation)
            {
                case "+":
                    Result += _currentValue;
                    break;
                case "-":
                    Result -= _currentValue;
                    break;
                case "*":
                    Result *= _currentValue;
                    break;
                case "/":
                    if (_currentValue == 0)
                        Error = "Cannot divide by zero";
                    else
                        Result /= _currentValue;
                    break;
                case "%":
                    Result = (Result * _currentValue) / 100;
                    break;
                case "√":
                    if (Result < 0)
                        Error = "Invalid input for square root";
                    else
                        Result = Math.Sqrt(Result);
                    break;
                case "x²":
                    Result *= Result;
                    break;
                case "1/x":
                    if (Result == 0)
                        Error = "Cannot divide by zero";
                    else
                        Result = 1 / Result;
                    break;
                case "+/-":
                    Result = -Result;
                    break;
            }

            _operation = null;
            isRepeatedEqual = true;
        }

        public void Clear()
        {
            Result = 0;
            _currentValue = 0;
            _lastNumber = 0;
            _operation = null;
            _lastOperation = null;
            Error = null;
        }

        public void ClearEntry()
        {
            _currentValue = 0;
            Error = null;
        }

        public void Backspace()
        {
            Result = (int)(Result / 10);
        }
    }
}
