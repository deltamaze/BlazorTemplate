using System;

namespace HelloWorldLibrary
{
    public static class Calculator
    {
        public static double Add(double x, double y)
        {
            return x + y;
        }
        public static double Divide(double x, double y)
        {
            //business logic, when y is zero, return zero
            if(y == 0)
            {
                return 0;
            }
            return x / y;
        }
    }
}