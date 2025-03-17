using System;

namespace Calculator_APP_
{
    public class CalculatorModel
    {
        private double _currentValue;
        private string? _operation;
        private double _lastNumber; // Ultimul număr utilizat în operație
        private string? _lastOperation; // Ultima operație efectuată

        public double Result { get; private set; }
        private bool isRepeatedEqual = false; // Indică dacă "=" a fost apăsat consecutiv

        public void SetOperation(string operation)
        {
            _operation = operation;
            isRepeatedEqual = false; // Resetăm flag-ul "="
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
                _lastNumber = number; // Memorăm numărul pentru repetare
                _lastOperation = _operation; // Memorăm ultima operație
            }
        }

        public void CalculateResult()
        {
            if (_operation == null) // Dacă "=" a fost apăsat consecutiv, repetăm ultima operație
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
                    if (_currentValue != 0)
                        Result /= _currentValue;
                    break;
            }

            _operation = null; // Resetăm operația pentru a permite repetarea la "="
            isRepeatedEqual = true; // Marcăm că "=" a fost apăsat
        }
    }
}