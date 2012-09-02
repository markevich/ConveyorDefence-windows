using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Conveyor_Defence.Misc
{
    /// <summary>
    /// Represents a pool of objects with a size limit.
    /// </summary>
    /// <typeparam name="T">The type of object in the pool.</typeparam>
    public sealed class ObjectPool<T> : IDisposable
        where T : new()
    {
        private readonly int _size;
        private readonly object _locker;
        private readonly Queue<T> _queue;
        private int _count;


        /// <summary>
        /// Initializes a new instance of the ObjectPool class.
        /// </summary>
        /// <param name="size">The size of the object pool.</param>
        public ObjectPool(int size)
        {
            if (size <= 0)
            {
                const string message = "The size of the pool must be greater than zero.";
                throw new ArgumentOutOfRangeException("size", size, message);
            }

            this._size = size;
            _locker = new object();
            _queue = new Queue<T>();
        }


        /// <summary>
        /// Retrieves an item from the pool. 
        /// </summary>
        /// <returns>The item retrieved from the pool.</returns>
        public T Get()
        {
            lock (_locker)
            {
                if (_queue.Count > 0)
                {
                    return _queue.Dequeue();
                }

                _count++;
                return new T();
            }
        }

        /// <summary>
        /// Places an item in the pool.
        /// </summary>
        /// <param name="item">The item to place to the pool.</param>
        public void Put(T item)
        {
            lock (_locker)
            {
                if (_count < _size)
                {
                    _queue.Enqueue(item);
                }
                else
                {
                    using (item as IDisposable)
                    {
                        _count--;
                    }
                }
            }
        }

        /// <summary>
        /// Disposes of items in the pool that implement IDisposable.
        /// </summary>
        public void Dispose()
        {
            lock (_locker)
            {
                _count = 0;
                while (_queue.Count > 0)
                {
                    using (_queue.Dequeue() as IDisposable)
                    {

                    }
                }
            }
        }
    }
}
