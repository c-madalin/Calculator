using System;

namespace Calculator_APP_
{
    public static class BaseConverter
    {
        private const string Digits = "0123456789ABCDEF";

        public static string ConvertToBase(int value, int toBase)
        {
            if (toBase != 2 && toBase != 8 && toBase != 10 && toBase != 16)
                throw new ArgumentOutOfRangeException(nameof(toBase), "Base must be 2, 8, 10, or 16");

            if (value == 0)
                return "0";

            bool isNegative = value < 0;
            if (isNegative)
                value = -value;

            string result = string.Empty;
            while (value > 0)
            {
                result = Digits[value % toBase] + result;
                value /= toBase;
            }

            if (isNegative)
                result = "-" + result;

            return result;
        }

        public static int ConvertFromBase(string value, int fromBase)
        {
            if (fromBase != 2 && fromBase != 8 && fromBase != 10 && fromBase != 16)
                throw new ArgumentOutOfRangeException(nameof(fromBase), "Base must be 2, 8, 10, or 16");

            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("Value cannot be null or empty", nameof(value));

            bool isNegative = value[0] == '-';
            if (isNegative)
                value = value.Substring(1);

            int result = 0;
            foreach (char digit in value)
            {
                int digitValue = Digits.IndexOf(char.ToUpper(digit));
                if (digitValue < 0 || digitValue >= fromBase)
                    throw new ArgumentException($"Invalid character '{digit}' for base {fromBase}");

                result = result * fromBase + digitValue;
            }

            return isNegative ? -result : result;
        }
    }
}