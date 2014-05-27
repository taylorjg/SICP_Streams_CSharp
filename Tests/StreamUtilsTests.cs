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
            Assert.That(actual, Is.EqualTo(new[] {1 + 1, 2 + 2, 3 + 3, 4 + 4, 5 + 5}));
        }

        [Test]
        public void TestMulStreams()
        {
            var actual = StreamUtils.MulStreams(Streams.Integers(), Streams.Integers())
                                    .ToEnumerable()
                                    .Take(5);
            Assert.That(actual, Is.EqualTo(new[] {1 * 1, 2 * 2, 3 * 3, 4 * 4, 5 * 5}));
        }
    }
}
