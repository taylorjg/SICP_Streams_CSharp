using System.Collections.Generic;

namespace Code
{
    public static class StreamExtensions
    {
        public static IEnumerable<int> ToEnumerable(this Stream s)
        {
            var currentStream = s;
            for (;;)
            {
                if (currentStream.IsEmpty) yield break;
                yield return currentStream.StreamCar;
                currentStream = currentStream.StreamCdr;
            }
        }
    }
}
