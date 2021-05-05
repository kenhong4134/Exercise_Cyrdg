using System;

namespace Question2
{
    class Program
    {

        public delegate int DelIncrement(int value);
        // Create a method for a delegate.
        public static int Increment(int value)
        {
            return value + 1;
        }

        static void Main(string[] args)
        {
            // DelIncrement handler = Increment;

            DelIncrement handler = (value)=>{return value +1;};


            Console.WriteLine(Pipe(5, handler).ToString());
            Console.WriteLine(Pipe(5, handler, handler, handler).ToString());
            Console.ReadKey();
        }

        public static int Pipe(int param1, params DelIncrement[] callbacks)
        {
            foreach (var item in callbacks)
            {
                param1= item(param1);
            }
            return param1;
        }
    }
}
