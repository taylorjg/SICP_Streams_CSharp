using System;
using System.Collections.Generic;
using System.Linq;

namespace Code
{
    public static class StreamUtils
    {
        public static void DisplayStream<T>(Stream<T> s)
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

        public static void DisplayStreamN<T>(Stream<T> s, int n)
        {
            DisplayStreamNIter(s, n);
        }

        private static void DisplayStreamNIter<T>(Stream<T> s, int remaining)
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

        public static Stream<T> StreamFilter<T>(Func<T, bool> pred, Stream<T> s)
        {
            if (s.IsEmpty) return s;
            if (pred(s.StreamCar)) return Stream<T>.ConsStream(s.StreamCar, () => StreamFilter(pred, s.StreamCdr));
            return StreamFilter(pred, s.StreamCdr);
        }

        public static Stream<T2> StreamMap<T1, T2>(Func<IEnumerable<T1>, T2> proc, params Stream<T1>[] streams)
        {
            if (streams[0].IsEmpty) return Stream<T2>.EmptyStream;
            return Stream<T2>.ConsStream(
                proc(streams.Select(s => s.StreamCar)),
                () => StreamMap(proc, streams.Select(s => s.StreamCdr).ToArray()));
        }

        public static Stream<int> AddStreams(Stream<int> s1, Stream<int> s2)
        {
            return StreamMap(xs => xs.Aggregate(0, (x, y) => x + y), s1, s2);
        }

        public static Stream<double> AddStreams(Stream<double> s1, Stream<double> s2)
        {
            return StreamMap(xs => xs.Aggregate(0d, (x, y) => x + y), s1, s2);
        }

        public static Stream<int> MulStreams(Stream<int> s1, Stream<int> s2)
        {
            return StreamMap(xs => xs.Aggregate(1, (x, y) => x * y), s1, s2);
        }

        public static Stream<double> MulStreams(Stream<double> s1, Stream<double> s2)
        {
            return StreamMap(xs => xs.Aggregate(1d, (x, y) => x * y), s1, s2);
        }

        public static Stream<int> NegateStream(Stream<int> s)
        {
            return StreamMap(xs => -xs.First(), s);
        }

        public static Stream<double> NegateStream(Stream<double> s)
        {
            return StreamMap(xs => -xs.First(), s);
        }

        public static Stream<int> ScaleStream(Stream<int> s, int factor)
        {
            return StreamMap(xs => xs.First() * factor, s);
        }

        public static Stream<double> ScaleStream(Stream<double> s, double factor)
        {
            return StreamMap(xs => xs.First() * factor, s);
        }

        public static Stream<int> PartialSums(Stream<int> s)
        {
            return Stream<int>.ConsStream(s.StreamCar, () => AddStreams(s.StreamCdr, PartialSums(s)));
        }

        public static Stream<double> PartialSums(Stream<double> s)
        {
            return Stream<double>.ConsStream(s.StreamCar, () => AddStreams(s.StreamCdr, PartialSums(s)));
        }

        public static Stream<double> IntegrateSeries(Stream<double> s)
        {
            return StreamMap(xs =>
                {
                    var arr = xs.ToArray();
                    var n = arr[0];
                    var d = arr[1];
                    return n / d;
                }, s, Streams.Doubles());
        }

        public static Stream<int> MulSeries(Stream<int> s1, Stream<int> s2)
        {
            return Stream<int>.ConsStream(
                s1.StreamCar * s2.StreamCar,
                () => AddStreams(
                    ScaleStream(s2.StreamCdr, s1.StreamCar),
                    MulSeries(s1.StreamCdr, s2)));
        }

        public static Stream<double> MulSeries(Stream<double> s1, Stream<double> s2)
        {
            return Stream<double>.ConsStream(
                s1.StreamCar * s2.StreamCar,
                () => AddStreams(
                    ScaleStream(s2.StreamCdr, s1.StreamCar),
                    MulSeries(s1.StreamCdr, s2)));
        }

        public static Stream<int> InvertUnitSeries(Stream<int> s)
        {
            CheckUnitSeries(s);

            return Stream<int>.ConsStream(
                1,
                () => NegateStream(MulSeries(s.StreamCdr, InvertUnitSeries(s))));
        }

        public static Stream<double> InvertUnitSeries(Stream<double> s)
        {
            CheckUnitSeries(s);

            return Stream<double>.ConsStream(
                1d,
                () => NegateStream(MulSeries(s.StreamCdr, InvertUnitSeries(s))));
        }

        private static void CheckUnitSeries(Stream<int> s)
        {
            if (s.StreamCar != 1) throw new ArgumentException("Constant term of unit series should be 1.", "s");
        }

        private static void CheckUnitSeries(Stream<double> s)
        {
            const double tolerance = 0.0000000000000001d;
            if (Math.Abs(s.StreamCar - 1d) > tolerance) throw new ArgumentException("Constant term of unit series should be 1.", "s");
        }

        public static Stream<int> DivSeries(Stream<int> s1, Stream<int> s2)
        {
            return MulSeries(s1, InvertUnitSeries(ScaleStream(s2, 1 / s2.StreamCar)));
        }

        public static Stream<double> DivSeries(Stream<double> s1, Stream<double> s2)
        {
            return MulSeries(s1, InvertUnitSeries(ScaleStream(s2, 1 / s2.StreamCar)));
        }

        private static double Square(double x)
        {
            return x * x;
        }

        public static Stream<double> EulerTransform(Stream<double> s)
        {
            var firstThree = s.ToEnumerable().Take(3).ToArray();
            var s0 = firstThree[0];
            var s1 = firstThree[1];
            var s2 = firstThree[2];
            return Stream<double>.ConsStream(
                s2 - (Square(s2 - s1) / (s0 + (-2 * s1) + s2)),
                () => EulerTransform(s.StreamCdr));
        }

        public static Stream<Stream<double>> MakeTableau(Func<Stream<double>, Stream<double>> transform, Stream<double> s)
        {
            return Stream<Stream<double>>.ConsStream(s, () => MakeTableau(transform, transform(s)));
        }

        public static Stream<double> AcceleratedSequence(Func<Stream<double>, Stream<double>> transform, Stream<double> s1)
        {
            return StreamMap(streams => streams.First().StreamCar, MakeTableau(transform, s1));
        }
    }
}
