namespace Code
{
    public class Streams
    {
        public static Stream<int> OnesAsIntegers()
        {
            return Stream<int>.ConsStream(1, OnesAsIntegers);
        }

        public static Stream<double> OnesAsDoubles()
        {
            return Stream<double>.ConsStream(1d, OnesAsDoubles);
        }

        public static Stream<int> IntegersStartingFrom(int n)
        {
            return Stream<int>.ConsStream(n, () => IntegersStartingFrom(n + 1));
        }

        public static Stream<int> Integers()
        {
            return IntegersStartingFrom(1);
        }

        public static Stream<double> DoublesStartingFrom(double d)
        {
            return Stream<double>.ConsStream(d, () => DoublesStartingFrom(d + 1d));
        }

        public static Stream<double> Doubles()
        {
            return DoublesStartingFrom(1d);
        }

        public static Stream<int> NoSevens()
        {
            return StreamUtils.StreamFilter(x => !IsDivisible(x, 7), Integers());
        }

        private static bool IsDivisible(int x, int y)
        {
            return x % y == 0;
        }

        public static Stream<int> Fibs()
        {
            return FibsGen(0, 1);
        }

        private static Stream<int> FibsGen(int a, int b)
        {
            return Stream<int>.ConsStream(a, () => FibsGen(b, a + b));
        }

        public static Stream<int> Primes()
        {
            return Sieve(IntegersStartingFrom(2));
        }

        public static Stream<int> Primes2()
        {
            return Stream<int>.ConsStream(2, () => StreamUtils.StreamFilter(IsPrime, IntegersStartingFrom(3)));
        }

        private static bool IsPrime(int n)
        {
            return IsPrimeIter(n, Primes2());
        }

        private static bool IsPrimeIter(int n, Stream<int> ps)
        {
            if (ps.StreamCar * ps.StreamCar > n) return true;
            if (IsDivisible(n, ps.StreamCar)) return false;
            return IsPrimeIter(n, ps.StreamCdr);
        }

        private static Stream<int> Sieve(Stream<int> s)
        {
            return Stream<int>.ConsStream(
                s.StreamCar,
                () => Sieve(
                    StreamUtils.StreamFilter(
                        x => !IsDivisible(x, s.StreamCar),
                        s.StreamCdr)));
        }

        public static Stream<int> StreamEnumerateInterval(int low, int high)
        {
            if (low > high) return Stream<int>.EmptyStream;
            return Stream<int>.ConsStream(low, () => StreamEnumerateInterval(low + 1, high));
        }

        public static Stream<int> Factorials()
        {
            return Stream<int>.ConsStream(1, () => StreamUtils.MulStreams(Integers(), Factorials()));
        }

        public static Stream<double> ExpSeries()
        {
            return Stream<double>.ConsStream(1d, () => StreamUtils.IntegrateSeries(ExpSeries()));
        }

        public static Stream<double> CosineSeries()
        {
            return Stream<double>.ConsStream(1d, () => StreamUtils.NegateStream(StreamUtils.IntegrateSeries(SineSeries())));
        }

        public static Stream<double> SineSeries()
        {
            return Stream<double>.ConsStream(0d, () => StreamUtils.IntegrateSeries(CosineSeries()));
        }

        public static Stream<double> TangentSeries()
        {
            return StreamUtils.DivSeries(SineSeries(), CosineSeries());
        }

        private static Stream<double> PiSummands(int n)
        {
            return Stream<double>.ConsStream(
                1d / n,
                () => StreamUtils.ScaleStream(PiSummands(n + 2), -1d));
        }

        public static Stream<double> PiStream()
        {
            return StreamUtils.ScaleStream(StreamUtils.PartialSums(PiSummands(1)), 4d);
        }
    }
}
