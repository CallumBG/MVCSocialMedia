namespace MVCSocialMedia.Models
{
    public class Calculator
    {

        public decimal Value { get; set; } = 0;

        public bool FirstValue = true;
        public decimal Add(decimal value)
        {
            return Value += value;
        }

        public decimal Divide(decimal value1 ,decimal value2)
        {
            if (value1 == 0 || value2 == 0)
            {
                return 0;
            }

            return value1 / value2;
        }

        public decimal Multiply(decimal value)
        {
            if(Value == 0 && FirstValue == true)
            {
                FirstValue= false;
                return Value = value;
            }
            return Value *= value;
        }

        public decimal Subtract(decimal value)
        {
            if (Value == 0 && FirstValue == true)
            {
                FirstValue = false;
                return Value = value;
            }
            return Value -= value;
        }
    }
}
