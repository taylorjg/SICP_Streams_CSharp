using System.Linq;
using Code;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    internal class StreamsTests
    {
        [Test]
        public void TestOnes()
        {
            var actual = Streams.OnesAsIntegers().ToEnumerable().Take(10).ToList();
            Assert.That(actual, Has.Count.EqualTo(10));
            Assert.That(actual, Has.All.Matches(Is.EqualTo(1)));
        }

        [Test]
        public void TestIntegersStartingFrom()
        {
            var actual = Streams.IntegersStartingFrom(5).ToEnumerable().Take(7).ToList();
            Assert.That(actual, Is.EqualTo(new[] {5, 6, 7, 8, 9, 10, 11}));
        }

        [Test]
        public void TestIntegers()
        {
            var actual = Streams.Integers().ToEnumerable().Take(5).ToList();
            Assert.That(actual, Is.EqualTo(new[] {1, 2, 3, 4, 5}));
        }

        [Test]
        public void TestNoSevens()
        {
            var actual = Streams.NoSevens().ToEnumerable().Take(20).ToList();
            Assert.That(actual, Is.EqualTo(new[] {1, 2, 3, 4, 5, 6, 8, 9, 10, 11, 12, 13, 15, 16, 17, 18, 19, 20, 22, 23}));
        }

        [Test]
        public void TestFibs()
        {
            var actual = Streams.Fibs().ToEnumerable().Take(12).ToList();
            Assert.That(actual, Is.EqualTo(new[] {0, 1, 1, 2, 3, 5, 8, 13, 21, 34, 55, 89}));
        }

        [Test]
        public void TestPrimes()
        {
            var actual = Streams.Primes().ToEnumerable().Take(20).ToList();
            Assert.That(actual, Is.EqualTo(new[] {2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71}));
        }

        [Test]
        public void TestPrimes2()
        {
            var actual = Streams.Primes2().ToEnumerable().Take(20).ToList();
            Assert.That(actual, Is.EqualTo(new[] {2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71}));
        }

        [Test]
        public void TestStreamEnumerateInterval()
        {
            var actual = Streams.StreamEnumerateInterval(2, 6).ToEnumerable().Take(20).ToList();
            Assert.That(actual, Is.EqualTo(new[] {2, 3, 4, 5, 6}));
        }

        [Test]
        public void TestExpSeries()
        {
            var actual = Streams.ExpSeries().ToEnumerable().Take(6).ToList();
            Assert.That(actual, Is.EqualTo(new[]
                {
                    1d,
                    1d,
                    1d / 2d,
                    1d / (3d * 2d),
                    1d / (4d * 3d * 2d),
                    1d / (5d * 4d * 3d * 2d)
                }));
        }

        [Test]
        public void TestCosineSeries()
        {
            var actual = Streams.CosineSeries().ToEnumerable().Take(8).ToList();
            Assert.That(actual, Is.EqualTo(new[]
                {
                    +1d,
                    0d,
                    -(1d / 2d),
                    0d,
                    +(1d / (4d * 3d * 2d)),
                    0d,
                    -(1d / (6d * 5d * 4d * 3d * 2d)),
                    0d
                }));
        }

        [Test]
        public void TestSineSeries()
        {
            var actual = Streams.SineSeries().ToEnumerable().Take(8).ToList();
            Assert.That(actual, Is.EqualTo(new[]
                {
                    0d,
                    +1d,
                    0d,
                    -(1d / (3d * 2d)),
                    0d,
                    +(1d / (5d* 4d * 3d * 2d)),
                    0d,
                    -(1d / (7d * 6d * 5d* 4d * 3d * 2d))
                }));
        }
    }
}
