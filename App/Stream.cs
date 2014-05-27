using System;

namespace App
{
    public sealed class Stream
    {
        private Stream()
            : this(null, null)
        {
        }

        private Stream(HeadHolder headHolderHolder, Func<Stream> delayedStream)
        {
            _headHolder = headHolderHolder;
            _delayedStream = delayedStream;
        }

        public static Stream EmptyStream = new Stream();

        public static Stream ConsStream(int head, Func<Stream> delayedStream)
        {
            return new Stream(new HeadHolder(head), delayedStream);
        }

        public bool IsEmpty { get { return this == EmptyStream; } }

        public int StreamCar
        {
            get
            {
                if (_headHolder == null) throw new InvalidOperationException("StreamCar of empty stream.");
                return _headHolder.Head;
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

        private readonly HeadHolder _headHolder;
        private readonly Func<Stream> _delayedStream;

        private class HeadHolder
        {
            public HeadHolder(int head)
            {
                _head = head;
            }

            public int Head {
                get { return _head; }
            }

            private readonly int _head;
        }
    }
}
