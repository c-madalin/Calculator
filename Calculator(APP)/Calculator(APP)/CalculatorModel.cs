using System;

namespace Calculator_APP_
{
    public class CalculatorModel
    {
        private double memory;
        private double currentValue;
        private string currentOperator;
        private double _result;
        private bool isOperatorSet;

        public CalculatorModel()
        {
            Clear();
        }
        public double Result
        {
            get { return _result; }
            set { _result = value; }
        }


        public double CurrentValue
        {
            get { return currentValue; }
            private set { currentValue = value; }
        }
        public void Add(double a, double b)
        {
            Result = a + b;
        }
        public void Minus(double a, double b)
        {
            Result = a - b;
        }
        public void Clear()
        {
            currentValue = 0;
            currentOperator = string.Empty;
            isOperatorSet = false;
        }

        public void SetOperator(string operatorSymbol)
        {
            if (!isOperatorSet)
            {
                currentOperator = operatorSymbol;
                isOperatorSet = true;
            }
        }

        public void Calculate(double value)
        {
            switch (currentOperator)
            {
                case "+":
                    currentValue += value;
                    break;
                case "-":
                    currentValue -= value;
                    break;
                case "*":
                    currentValue *= value;
                    break;
                case "÷":
                    currentValue /= value;
                    break;
                case "√x":
                    currentValue = Math.Sqrt(value);
                    break;
                case "x²":
                    currentValue = Math.Pow(value, 2);
                    break;
                case "1/x":
                    currentValue = 1 / value;
                    break;
                default:
                    currentValue = value;
                    break;
            }

            isOperatorSet = false;
        }

        public void MemoryStore()
        {
            memory = currentValue;
        }

        public double MemoryRecall()
        {
            return memory;
        }

        public void MemoryClear()
        {
            memory = 0;
        }

        public void MemoryAdd(double value)
        {
            memory += value;
        }

        public void MemorySubtract(double value)
        {
            memory -= value;
        }
    }
}