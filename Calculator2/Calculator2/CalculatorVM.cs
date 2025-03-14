namespace Calculator2
{
    public class CalculatorVM
    {
        public CalculatorVM()
        {

        }
        public static double Calculator(double _firstNumber, double _secondNumber, string _operator)
        {
            double result = 0;

            switch (_operator)
            {
                case "+":
                    result = _firstNumber + _secondNumber;
                    break;
                case "-":
                    result = _firstNumber - _secondNumber;
                    break;
                case "*":
                    result = _firstNumber * _secondNumber;
                    break;
                case "/":
                    result = _firstNumber / _secondNumber;
                    break;
                case "%":
                    result = _firstNumber % _secondNumber;
                    break;
            }

            return result;
        }
    }
}