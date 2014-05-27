using System;
using System.Collections.Generic;
using System.Linq;

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

        public static Stream StreamFilter(Func<int, bool> pred, Stream s)
        {
            if (s.IsEmpty) return s;
            if (pred(s.StreamCar)) return Stream.ConsStream(s.StreamCar, () => StreamFilter(pred, s.StreamCdr));
            return StreamFilter(pred, s.StreamCdr);
        }

        public static Stream StreamMap(Func<IEnumerable<int>, int> proc, params Stream[] streams)
        {
            if (streams[0].IsEmpty) return Stream.EmptyStream;
            return Stream.ConsStream(
                proc(streams.Select(s => s.StreamCar)),
                () => StreamMap(proc, streams.Select(s => s.StreamCdr).ToArray()));
        }

        public static Stream AddStreams(Stream s1, Stream s2)
        {
            return StreamMap(xs => xs.Sum(), s1, s2);
        }

        public static Stream MulStreams(Stream s1, Stream s2)
        {
            return StreamMap(xs => xs.Aggregate(1, (x, y) => x * y), s1, s2);
        }

        public static Stream PartialSums(Stream s)
        {
            return Stream.ConsStream(s.StreamCar, () => StreamUtils.AddStreams(s.StreamCdr, PartialSums(s)));
        }

        public static Stream IntegrateSeries(Stream s)
        {
            return StreamMap(xs => { var arr = xs.ToArray(); return arr[0] / arr[1]; }, s, Streams.Integers());
        }
    }
}
