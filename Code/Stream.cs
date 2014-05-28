using System;

namespace Code
{
    public sealed class Stream<T>
    {
        private Stream()
        {
            _pair = null;
        }

        private Stream(T head, Func<Stream<T>> delayedStream)
        {
            _pair = Tuple.Create(head, delayedStream);
        }

        public static Stream<T> ConsStream(T head)
        {
            return new Stream<T>(head, () => EmptyStream);
        }

        public static Stream<T> ConsStream(T head, Func<Stream<T>> delayedStream)
        {
            return new Stream<T>(head, delayedStream);
        }

        public static Stream<T> EmptyStream = new Stream<T>();

        public bool IsEmpty { get { return this == EmptyStream; } }

        public T StreamCar
        {
            get
            {
                PerformEmptyCheck("StreamCar");
                return _pair.Item1;
            }
        }

        public Stream<T> StreamCdr
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

        private readonly Tuple<T, Func<Stream<T>>> _pair;
    }
}
