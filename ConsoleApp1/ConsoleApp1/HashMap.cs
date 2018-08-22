using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class HashMap<TKey, TValue> : IDictionary<TKey, TValue>
    {
        LinkedList<KeyValuePair<TKey, TValue>>[] buckets;
        const int defaultCapacity = 30;

        public TValue this[TKey key] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public IEqualityComparer<TKey> EqualityComparer { get; private set; }

        public ICollection<TKey> Keys => throw new NotImplementedException();

        public ICollection<TValue> Values => throw new NotImplementedException();

        public int Count { get; private set; }

        public bool IsReadOnly => false;

        public HashMap()
            : this(defaultCapacity, null)
        { }

        public HashMap(int capacity)
            : this(capacity, null)
        { }

        public HashMap(IEqualityComparer<TKey> equalityComparer)
            : this(defaultCapacity, equalityComparer)
        { }

        public HashMap(int capacity, IEqualityComparer<TKey> equalityComparer)
        {
            if (capacity < 0)
            {
                throw new ArgumentOutOfRangeException("Capacity is less than zero");
            }

            EqualityComparer = equalityComparer ?? EqualityComparer<TKey>.Default;

            buckets = new LinkedList<KeyValuePair<TKey, TValue>>[capacity];            
        }

        public void Add(TKey key, TValue value)
        {
            Add(new KeyValuePair<TKey, TValue>(key, value));         
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            if (ContainsKey(item.Key))
            {
                throw new Exception("Duplicate key");
            }

            if (Count + 1 >= buckets.Length)
            {
                ReHash();
            }

            int index = Math.Abs(item.Key.GetHashCode() % buckets.Length);

            if (buckets[index] == null)
            {
                buckets[index] = new LinkedList<KeyValuePair<TKey, TValue>>();
            }

            buckets[index].AddLast(item);

            Count++;
        }

        private void ReHash()
        {
            var temp = new LinkedList<KeyValuePair<TKey, TValue>>[buckets.Length * 2];

            for (int i = 0; i < buckets.Length; i++)
            {
                foreach (var thing in buckets[i])
                {
                    int newIndex = Math.Abs(thing.GetHashCode() % temp.Length);

                    if (temp[newIndex] == null)
                    {
                        temp[newIndex] = new LinkedList<KeyValuePair<TKey, TValue>>();
                    }

                    temp[newIndex].AddLast(thing);
                }
            }

            buckets = temp;
        }

        public void Clear()
        {
            if (Count > 0)
            {
                Array.Clear(buckets, 0, buckets.Length);
            }
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            int index = item.Key.GetHashCode() % buckets.Length;

            throw new NotImplementedException();
        }

        public bool ContainsKey(TKey key)
        {
            int index = Math.Abs(key.GetHashCode() % buckets.Length);

            if (buckets[index] != null)
            {
                foreach (var item in buckets[index])
                {
                    if (EqualityComparer.Equals(item.Key, key))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }
        
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            for (int i = 0; i < buckets.Length; i++)
            {
                foreach (var item in buckets[i])
                {
                    yield return item;
                }
            }
        }

        public bool Remove(TKey key)
        {
            if (Count == 0)
            {
                return false;
            }

            int index = Math.Abs(key.GetHashCode() % buckets.Length);

            if (buckets[index] != null)
            {
                foreach (var item in buckets[index])
                {
                    if (EqualityComparer.Equals(item.Key, key))
                    {
                        buckets[index].Remove(item);
                        Count--;
                        return true;
                    }
                }
            }
            
            return false;
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return Remove(item.Key);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {            
            if (ContainsKey(key))
            {
                int index = Math.Abs(key.GetHashCode() % buckets.Length);

                foreach (var item in buckets[index])
                {
                    if (EqualityComparer.Equals(item.Key, key))
                    {
                        value = item.Value;
                        return true;
                    }
                }
            }

            value = default(TValue);
            return false;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
