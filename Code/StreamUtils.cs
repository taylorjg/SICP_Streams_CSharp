using System;

namespace Code
{
    public static class StreamUtils
    {
        public static void DisplayStream(Stream s)
        {
            if (s.IsEmpty)
            {
                Console.WriteLine();
            }
            else
            {
                Console.Write("{0} ", s.StreamCar);
                DisplayStream(s.StreamCdr);
            }
        }

        public static void DisplayStreamN(Stream s, int n)
        {
            DisplayStreamNIter(s, n);
        }

        private static void DisplayStreamNIter(Stream s, int remaining)
        {
            if (remaining > 0)
            {
                if (s.IsEmpty)
                {
                    Console.WriteLine();
                }
                else
                {
                    Console.Write("{0} ", s.StreamCar);
                    DisplayStreamNIter(s.StreamCdr, remaining - 1);
                }
            }
            else
            {
                Console.WriteLine();
            }
        }
    }
}
