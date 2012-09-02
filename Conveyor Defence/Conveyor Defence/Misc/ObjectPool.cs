using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Conveyor_Defence.Misc
{
    public sealed class ObjectPool<T> : IEnumerable<T> where T : new()
    {
        private readonly List<T> _collection;

        public ObjectPool(int size)
        {
            if (size <= 0)
            {
                const string message = "The size of the pool must be greater than zero.";
                throw new ArgumentOutOfRangeException("size", size, message);
            }

            _collection = new List<T>(size);
        }

        public T AddNewObject()
        {
            var element = new T();
            _collection.Add(element);
            return element;
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            foreach (T obj in _collection)
            {
                yield return obj;
            }
        }

        public IEnumerator GetEnumerator()
        {
            foreach (var obj in _collection)
            {
                yield return obj;
            }
        }
    }
}
