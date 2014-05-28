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

        public static Stream<T> StreamMap<T>(Func<IEnumerable<T>, T> proc, params Stream<T>[] streams)
        {
            if (streams[0].IsEmpty) return Stream<T>.EmptyStream;
            return Stream<T>.ConsStream(
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

        public static Stream<double> ScaleStream(Stream<double> s, double factor)
        {
            return StreamMap(xs => xs.First() * factor, s);
        }

        public static Stream<int> PartialSums(Stream<int> s)
        {
            return Stream<int>.ConsStream(s.StreamCar, () => AddStreams(s.StreamCdr, PartialSums(s)));
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

        public static Stream<double> MulSeries(Stream<double> s1, Stream<double> s2)
        {
            return Stream<double>.ConsStream(
                s1.StreamCar * s2.StreamCar,
                () => AddStreams(
                    ScaleStream(s2.StreamCdr, s1.StreamCar),
                    MulSeries(s1.StreamCdr, s2)));
        }
    }
}
