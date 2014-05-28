using System.Linq;
using Code;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    internal class StreamUtilsTests
    {
        [Test]
        public void TestStreamMap()
        {
            var actual = StreamUtils.StreamMap(
                xs => xs.Sum(),
                Streams.Integers(),
                Streams.Integers())
                                    .ToEnumerable()
                                    .Take(5);
            Assert.That(actual, Is.EqualTo(new[] {1 + 1, 2 + 2, 3 + 3, 4 + 4, 5 + 5}));
        }

        [Test]
        public void TestAddStreams()
        {
            var actual = StreamUtils.AddStreams(Streams.Integers(), Streams.Integers())
                                    .ToEnumerable()
                                    .Take(5);
            Assert.That(actual, Is.EqualTo(new[] { 1 + 1, 2 + 2, 3 + 3, 4 + 4, 5 + 5 }));
        }

        [Test]
        public void TestMulStreams()
        {
            var actual = StreamUtils.MulStreams(Streams.Integers(), Streams.Integers())
                                    .ToEnumerable()
                                    .Take(5);
            Assert.That(actual, Is.EqualTo(new[] { 1 * 1, 2 * 2, 3 * 3, 4 * 4, 5 * 5 }));
        }

        [Test]
        public void TestNegateStreamOfIntegers()
        {
            var actual = StreamUtils.NegateStream(Streams.Integers())
                                    .ToEnumerable()
                                    .Take(5);
            Assert.That(actual, Is.EqualTo(new[] {-1, -2, -3, -4, -5}));
        }

        [Test]
        public void TestNegateStreamOfDoubles()
        {
            var actual = StreamUtils.NegateStream(Streams.Doubles())
                                    .ToEnumerable()
                                    .Take(5);
            Assert.That(actual, Is.EqualTo(new[] {-1d, -2d, -3d, -4d, -5d}));
        }

        [Test]
        public void TestScaleStreamOfDoubles()
        {
            var actual = StreamUtils.ScaleStream(Streams.Doubles(), 2d)
                                    .ToEnumerable()
                                    .Take(5);
            Assert.That(actual, Is.EqualTo(new[]
                {
                    1d * 2d,
                    2d * 2d,
                    3d * 2d,
                    4d * 2d,
                    5d * 2d
                }));
        }

        [Test]
        public void TestIntegrateSeries()
        {
            var actual = StreamUtils.IntegrateSeries(Streams.OnesAsDoubles())
                                    .ToEnumerable()
                                    .Take(5);
            Assert.That(actual, Is.EqualTo(new[]
                {
                    1d / 1d,
                    1d / 2d,
                    1d / 3d,
                    1d / 4d,
                    1d / 5d
                }));
        }
    }
}
