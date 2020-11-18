using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class CalculatorLogic
    {
        public long Add(int firstValue, int secondValue)
        {
            return firstValue + secondValue;
        }

        public long Subtract(int firstValue, int secondValue)
        {
            return firstValue - secondValue;
        }

        public long Divide(int firstValue, int secondValue)
        {
            return firstValue / secondValue;
        }

        public long Multiply(int firstValue, int secondValue)
        {
            return firstValue * secondValue;
        }

        public long ChangeTo(int currentValue)
        {
            return currentValue *= -1;
        }
    }
}
