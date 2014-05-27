namespace Code
{
    public class Streams
    {
        public static Stream Ones()
        {
            return Stream.ConsStream(1, Ones);
        }

        public static Stream IntegersStartingFrom(int n)
        {
            return Stream.ConsStream(n, () => IntegersStartingFrom(n + 1));
        }

        public static Stream Integers()
        {
            return IntegersStartingFrom(1);
        }

        public static Stream NoSevens()
        {
            return StreamUtils.StreamFilter(x => !IsDivisible(x, 7), Integers());
        }

        private static bool IsDivisible(int x, int y)
        {
            return x % y == 0;
        }

        public static Stream Fibs()
        {
            return FibsGen(0, 1);
        }

        private static Stream FibsGen(int a, int b)
        {
            return Stream.ConsStream(a, () => FibsGen(b, a + b));
        }

        public static Stream Primes()
        {
            return Sieve(IntegersStartingFrom(2));
        }

        public static Stream Primes2()
        {
            return Stream.ConsStream(2, () => StreamUtils.StreamFilter(IsPrime, IntegersStartingFrom(3)));
        }

        private static bool IsPrime(int n)
        {
            return IsPrimeIter(n, Primes2());
        }

        private static bool IsPrimeIter(int n, Stream ps)
        {
            if (ps.StreamCar * ps.StreamCar > n) return true;
            if (IsDivisible(n, ps.StreamCar)) return false;
            return IsPrimeIter(n, ps.StreamCdr);
        }

        private static Stream Sieve(Stream s)
        {
            return Stream.ConsStream(
                s.StreamCar,
                () => Sieve(
                    StreamUtils.StreamFilter(
                        x => !IsDivisible(x, s.StreamCar),
                        s.StreamCdr)));
        }

        public static Stream StreamEnumerateInterval(int low, int high)
        {
            if (low > high) return Stream.EmptyStream;
            return Stream.ConsStream(low, () => StreamEnumerateInterval(low + 1, high));
        }

        public static Stream Factorials()
        {
            return Stream.ConsStream(1, () => StreamUtils.MulStreams(Integers(), Factorials()));
        }
    }
}
