using System;

namespace App
{
    public sealed class Stream
    {
        private Stream()
        {
        }

        public static Stream EmptyStream = new Stream();

        public static Stream ConsStream(int head, Func<Stream> delayedStream)
        {
            return new Stream
                {
                    _head = new ValueHolder(head),
                    _delayedStream = delayedStream
                };
        }

        public bool IsEmpty { get { return this == EmptyStream; } }

        public int StreamCar
        {
            get
            {
                if (_head == null) throw new InvalidOperationException("StreamCar of empty stream.");
                return _head.Value;
            }
        }

        public Stream StreamCdr
        {
            get
            {
                if (_delayedStream == null) throw new InvalidOperationException("StreamCdr of empty stream.");
                return _delayedStream();
            }
        }

        private ValueHolder _head;
        private Func<Stream> _delayedStream;

        private class ValueHolder
        {
            public ValueHolder(int value)
            {
                Value = value;
            }

            public int Value { get; private set; }
        }
    }
}
