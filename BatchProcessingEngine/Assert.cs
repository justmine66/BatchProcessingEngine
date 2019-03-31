using System;

namespace BatchProcessingEngine
{
    public static class Assert
    {
        public static void NotNull(object obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));
        }

        public static void Range(float value, int from, int to)
        {
            if (value > to || value <= from)
                throw new ArgumentOutOfRangeException($"{nameof(value)}[{value}] must be greater than {from} and less than or equal to {to}.");
        }
    }
}
