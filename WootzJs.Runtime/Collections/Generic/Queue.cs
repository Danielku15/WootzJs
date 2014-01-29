﻿using System.Runtime.WootzJs;

namespace System.Collections.Generic
{
    public class Queue<T> : IEnumerable<T>, ICollection
    {
        private JsArray storage = new JsArray();
 
        public void Enqueue(T item)
        {
            storage.push(item.As<JsObject>());
        }

        public T Dequeue()
        {
            return storage.shift().As<T>();
        }

        public T Peek()
        {
            if (storage.length == 0)
                throw new InvalidOperationException();
            return storage[0].As<T>();
        }

        public int Count
        {
            get { return storage.length; }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return GetEnumerable().GetEnumerator();
        }

        private IEnumerable<T> GetEnumerable()
        {
            for (var i = 0; i < storage.length; i++)
            {
                yield return storage[i].As<T>();
            }
        }

        public object SyncRoot
        {
            get { return this; }
        }

        public bool IsSynchronized
        {
            get { return true; }
        }

        public void CopyTo(Array array, int index)
        {
            for (int i = 0, j = index; index < array.Length && i < storage.length; i++, j++)
            {
                array[index] = storage[i];
            }
        }
    }
}