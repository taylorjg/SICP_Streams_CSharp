using System.Linq;
using Code;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    internal class StreamsTests
    {
        [Test]
        public void TestIntegers()
        {
            var actual = Streams.Integers().ToEnumerable().Take(5).ToList();
            Assert.That(actual, Is.EqualTo(new[] {1, 2, 3, 4, 5}));
        }
    }
}
