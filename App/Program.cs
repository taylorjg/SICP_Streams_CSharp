﻿using Code;

namespace App
{
    class Program
    {
        static void Main()
        {
            var s1 = Stream<int>.ConsStream(1,
                () => Stream<int>.ConsStream(2,
                    () => Stream<int>.ConsStream(3,
                        () => Stream<int>.ConsStream(4,
                            () => Stream<int>.EmptyStream))));

            StreamUtils.DisplayStream(s1);
            StreamUtils.DisplayStreamN(s1, 2);

            var s2 = Stream<int>.ConsStream(1,
                () => Stream<int>.ConsStream(2,
                    () => Stream<int>.ConsStream(3,
                        () => Stream<int>.ConsStream(4))));

            StreamUtils.DisplayStream(s2);
            StreamUtils.DisplayStreamN(s2, 2);

            StreamUtils.DisplayStreamN(Streams.OnesAsIntegers(), 10);
            StreamUtils.DisplayStreamN(Streams.IntegersStartingFrom(15), 10);
            StreamUtils.DisplayStreamN(Streams.Integers(), 10);
            StreamUtils.DisplayStreamN(Streams.Doubles(), 10);
            StreamUtils.DisplayStreamN(Streams.NoSevens(), 30);
            StreamUtils.DisplayStreamN(Streams.Fibs(), 25);
            StreamUtils.DisplayStreamN(Streams.Primes(), 25);
            StreamUtils.DisplayStreamN(Streams.Primes2(), 25);
            StreamUtils.DisplayStreamN(Streams.StreamEnumerateInterval(5, 12), 25);
            StreamUtils.DisplayStreamN(StreamUtils.AddStreams(Streams.Integers(), Streams.Integers()), 20);
            StreamUtils.DisplayStreamN(Streams.Factorials(), 12);
            StreamUtils.DisplayStreamN(StreamUtils.PartialSums(Streams.Integers()), 12);
            StreamUtils.DisplayStreamN(StreamUtils.IntegrateSeries(Streams.OnesAsDoubles()), 12);
            StreamUtils.DisplayStreamN(Streams.ExpSeries(), 12);
            StreamUtils.DisplayStreamN(Streams.CosineSeries(), 10);
            StreamUtils.DisplayStreamN(Streams.SineSeries(), 10);
            StreamUtils.DisplayStreamN(Streams.TangentSeries(), 10);

            StreamUtils.DisplayStreamN(
                StreamUtils.AddStreams(
                    StreamUtils.MulSeries(Streams.CosineSeries(), Streams.CosineSeries()),
                    StreamUtils.MulSeries(Streams.SineSeries(), Streams.SineSeries())),
                4);

            StreamUtils.DisplayStreamN(StreamUtils.InvertUnitSeries(Streams.OnesAsIntegers()), 10);
            StreamUtils.DisplayStreamN(StreamUtils.InvertUnitSeries(Streams.OnesAsDoubles()), 10);
            StreamUtils.DisplayStreamN(StreamUtils.InvertUnitSeries(Streams.ExpSeries()), 10);

            StreamUtils.DisplayStreamN(
                StreamUtils.MulStreams(
                    Streams.Integers(),
                    StreamUtils.InvertUnitSeries(Streams.Integers())),
                10);

            StreamUtils.DisplayStreamN(StreamUtils.DivSeries(Streams.OnesAsIntegers(), Streams.OnesAsIntegers()), 10);
            StreamUtils.DisplayStreamN(StreamUtils.DivSeries(Streams.OnesAsDoubles(), Streams.OnesAsDoubles()), 10);

            StreamUtils.DisplayStreamN(Streams.PiStream(), 10);
            StreamUtils.DisplayStreamN(StreamUtils.EulerTransform(Streams.PiStream()), 10);
            StreamUtils.DisplayStreamN(StreamUtils.AcceleratedSequence(StreamUtils.EulerTransform, Streams.PiStream()), 10);
        }
    }
}
