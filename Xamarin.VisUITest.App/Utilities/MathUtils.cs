using System;

namespace Xamarin.VisUITest.App.Utilities
{
    public static class MathUtils
    {
        public const double Epsilon = 0.000005;

        public static double TruncateDecimal(this double value, uint decimalPlaces)
        {
            if (decimalPlaces > 15)
            {
                throw new ArgumentOutOfRangeException("Truncating decimals must be between 0 and 10 inclusively to prevent loss of precision");
            }

            double factor = Math.Pow(10, decimalPlaces);
            double integralValue = Math.Truncate(value);
            double integralValueFactored = integralValue * factor;
            double fractionFactored = (value * factor) - integralValueFactored;
            double truncatedFraction = Math.Truncate(fractionFactored) / factor;

            return integralValue + truncatedFraction;
        }

        public static double CeilingDecimal(this double value, uint decimalPlaces)
        {
            if (decimalPlaces > 15)
            {
                throw new ArgumentOutOfRangeException("Ceiling decimals must be between 0 and 10 inclusively to prevent loss of precision");
            }

            double factor = Math.Pow(10, decimalPlaces);
            double integralValue = Math.Truncate(value);
            double integralValueFactored = integralValue * factor;
            double fractionFactored = (value * factor) - integralValueFactored;
            double truncatedFraction = Math.Ceiling(fractionFactored) / factor;

            return integralValue + truncatedFraction;
        }

        public static int Clamp(int value, int min, int max)
        {
            double result = Clamp((double)value, min, max);

            return (int)result;
        }

        public static double Clamp(double value, double min, double max)
        {
            if (min > max)
            {
                throw new ArgumentOutOfRangeException("Max clamp value shiould laways be higher or equal to min clamp value");
            }

            double result = value;

            if (value < min)
            {
                result = min;
            }
            else if (value > max)
            {
                result = max;
            }

            return result;
        }

        public static TimeSpan Min(TimeSpan value1, TimeSpan value2)
        {
            return (value1 < value2) ? value1 : value2;
        }

        public static TimeSpan Max(TimeSpan value1, TimeSpan value2)
        {
            return (value1 < value2) ? value2 : value1;
        }

        public static bool DiffersFrom(this double value1, double value2)
        {
            bool result = false;

            if (double.IsNaN(value1) || double.IsInfinity(value1) ||
                double.IsNaN(value2) || double.IsInfinity(value2))
            {
                // We need to do this because operations on these "values" might yield non numeric
                // results and values would be considered equal
                result = value1 != value2;
            }
            else
            {
                result = Math.Abs(value1 - value2) > MathUtils.Epsilon;
            }

            return result;
        }
    }
}
