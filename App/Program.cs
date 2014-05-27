using System;

namespace App
{
    class Program
    {
        static void Main()
        {
            var s = Stream.ConsStream(1, () => Stream.ConsStream(2, () => Stream.ConsStream(3, () => Stream.ConsStream(4, () => Stream.EmptyStream))));
            DisplayStream(s);
            DisplayStreamN(s, 2);

            DisplayStreamN(Ones(), 10);
            DisplayStreamN(IntegersStartingFrom(15), 10);
            DisplayStreamN(Integers(), 10);
        }

        private static Stream Ones()
        {
            return Stream.ConsStream(1, Ones);
        }

        private static Stream IntegersStartingFrom(int n)
        {
            return Stream.ConsStream(n, () => IntegersStartingFrom(n + 1));
        }

        private static Stream Integers()
        {
            return IntegersStartingFrom(1);
        }

        private static void DisplayStream(Stream s)
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

        private static void DisplayStreamN(Stream s, int n)
        {
            DisplayStreamNIter(s, n);
        }
    }
}
