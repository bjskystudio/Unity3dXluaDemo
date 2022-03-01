using System;
using System.Collections.Generic;

namespace YoukiaEngine
{
    public class ConcurrentQueue<T>
    {
        private Queue<T> _frontQueue = new Queue<T>();
        private Queue<T> _backQueue = new Queue<T>();

        private readonly object _synLock = new object();

        public void Enqueue(T item)
        {
            lock (_synLock)
            {
                _frontQueue.Enqueue(item);
            }
        }

        public Queue<T> Switch()
        {
            lock (_synLock)
            {
                if (_backQueue.Count == 0)
                {
                    Queue<T> tmp = _frontQueue;
                    _frontQueue = _backQueue;
                    _backQueue = tmp;
                }
            }

            return _backQueue;
        }

        public void Clear()
        {
            lock (_synLock)
            {
                _frontQueue.Clear();
                _backQueue.Clear();
            }
        }

        public int Count
        {
            get
            {
                lock (_synLock)
                {
                    return _frontQueue.Count;
                }
            }
        }
    }
}
