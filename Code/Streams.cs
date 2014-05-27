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

        private static Stream Sieve(Stream s)
        {
            return Stream.ConsStream(
                s.StreamCar,
                () => Sieve(
                    StreamUtils.StreamFilter(
                        x => !IsDivisible(x, s.StreamCar),
                        s.StreamCdr)));
        }
    }
}
