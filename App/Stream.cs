using System;

namespace App
{
    public sealed class Stream
    {
        private Stream(Tuple<int, Func<Stream>> pair)
        {
            _pair = pair;
        }

        public static Stream EmptyStream = new Stream(null);

        public static Stream ConsStream(int head, Func<Stream> delayedStream)
        {
            return new Stream(Tuple.Create(head, delayedStream));
        }

        public bool IsEmpty { get { return this == EmptyStream; } }

        public int StreamCar
        {
            get
            {
                PerformEmptyCheck("StreamCar");
                return _pair.Item1;
            }
        }

        public Stream StreamCdr
        {
            get
            {
                PerformEmptyCheck("StreamCdr");
                return _pair.Item2();
            }
        }

        private void PerformEmptyCheck(string functionName)
        {
            if (_pair == null) throw new InvalidOperationException(string.Format("{0} called on empty stream.", functionName));
        }

        private readonly Tuple<int, Func<Stream>> _pair;
    }
}
