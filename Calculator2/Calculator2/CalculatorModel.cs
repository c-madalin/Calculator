namespace Calculator2
{
    public class CalculatorModel
    {
        private double _result;

        public double Result
        {
            get { return _result; }
            set { _result = value; }
        }

        public void Add(double a, double b)
        {
            Result = a + b;
        }
        public void Minus(double a, double b)
        {
            Result = a - b;
        }

    }
}