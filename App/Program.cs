using Code;

namespace App
{
    class Program
    {
        static void Main()
        {
            var s = Stream.ConsStream(1,
                () => Stream.ConsStream(2,
                    () => Stream.ConsStream(3,
                        () => Stream.ConsStream(4,
                            () => Stream.EmptyStream))));

            StreamUtils.DisplayStream(s);
            StreamUtils.DisplayStreamN(s, 2);

            StreamUtils.DisplayStreamN(Streams.Ones(), 10);
            StreamUtils.DisplayStreamN(Streams.IntegersStartingFrom(15), 10);
            StreamUtils.DisplayStreamN(Streams.Integers(), 10);
            StreamUtils.DisplayStreamN(Streams.NoSevens(), 30);
            StreamUtils.DisplayStreamN(Streams.Fibs(), 25);
            StreamUtils.DisplayStreamN(Streams.Primes(), 25);
            StreamUtils.DisplayStreamN(Streams.Primes2(), 25);
            StreamUtils.DisplayStreamN(Streams.StreamEnumerateInterval(5, 12), 25);
            StreamUtils.DisplayStreamN(StreamUtils.AddStreams(Streams.Integers(), Streams.Integers()), 20);
            StreamUtils.DisplayStreamN(Streams.Factorials(), 12);
            StreamUtils.DisplayStreamN(StreamUtils.PartialSums(Streams.Integers()), 12);
            StreamUtils.DisplayStreamN(StreamUtils.IntegrateSeries(Streams.Integers()), 12);
        }
    }
}
