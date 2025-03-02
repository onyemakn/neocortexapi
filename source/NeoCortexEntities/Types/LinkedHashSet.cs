﻿// Copyright (c) Damir Dobric. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NeoCortexApi.Entities;

namespace NeoCortexApi.Types
{
    //[Serializable]
    public class LinkedHashSet<T> : ISet<T>, ISerializable
    {
       // [field: NonSerializedAttribute()]
        private IDictionary<T, LinkedListNode<T>> dict;

       // [field: NonSerializedAttribute()]
        private LinkedList<T> list;

        public LinkedHashSet(int initialCapacity)
        {
            this.dict = new Dictionary<T, LinkedListNode<T>>(initialCapacity);
            this.list = new LinkedList<T>();
        }

        public LinkedHashSet()
        {
            this.dict = new Dictionary<T, LinkedListNode<T>>();
            this.list = new LinkedList<T>();
        }

        public LinkedHashSet(IEnumerable<T> e) : this()
        {
            addEnumerable(e);
        }

        public LinkedHashSet(int initialCapacity, IEnumerable<T> e) : this(initialCapacity)
        {
            addEnumerable(e);
        }

        private void addEnumerable(IEnumerable<T> e)
        {
            foreach (T t in e)
            {
                Add(t);
            }
        }

        //
        // ISet implementation
        //

        public bool Add(T item)
        {
            if (this.dict.ContainsKey(item))
            {
                return false;
            }
            LinkedListNode<T> node = this.list.AddLast(item);
            this.dict[item] = node;
            return true;
        }

        public void ExceptWith(IEnumerable<T> other)
        {
            if (other == null)
            {
                throw new ArgumentNullException("other cannot be null");
            }
            foreach (T t in other)
            {
                Remove(t);
            }
        }

        public void IntersectWith(IEnumerable<T> other)
        {
            if (other == null)
            {
                throw new ArgumentNullException("other cannot be null");
            }
            T[] ts = new T[Count];
            CopyTo(ts, 0);
            foreach (T t in ts)
            {
                if (!System.Linq.Enumerable.Contains(other, t))
                {
                    Remove(t);
                }
            }
        }

        public bool IsProperSubsetOf(IEnumerable<T> other)
        {
            if (other == null)
            {
                throw new ArgumentNullException("other cannot be null");
            }
            int contains = 0;
            int noContains = 0;
            foreach (T t in other)
            {
                if (Contains(t))
                {
                    contains++;
                }
                else
                {
                    noContains++;
                }
            }
            return contains == Count && noContains > 0;
        }

        public bool IsProperSupersetOf(IEnumerable<T> other)
        {
            if (other == null)
            {
                throw new ArgumentNullException("other cannot be null");
            }
            int otherCount = System.Linq.Enumerable.Count(other);
            if (Count <= otherCount)
            {
                return false;
            }
            int contains = 0;
            int noContains = 0;
            foreach (T t in this)
            {
                if (System.Linq.Enumerable.Contains(other, t))
                {
                    contains++;
                }
                else
                {
                    noContains++;
                }
            }
            return contains == otherCount && noContains > 0;
        }

        public bool IsSubsetOf(IEnumerable<T> other)
        {
            if (other == null)
            {
                throw new ArgumentNullException("other cannot be null");
            }
            foreach (T t in this)
            {
                if (!System.Linq.Enumerable.Contains(other, t))
                {
                    return false;
                }
            }
            return true;
        }

        public bool IsSupersetOf(IEnumerable<T> other)
        {
            if (other == null)
            {
                throw new ArgumentNullException("other cannot be null");
            }
            foreach (T t in other)
            {
                if (!Contains(t))
                {
                    return false;
                }
            }
            return true;
        }

        public bool Overlaps(IEnumerable<T> other)
        {
            if (other == null)
            {
                throw new ArgumentNullException("other cannot be null");
            }
            foreach (T t in other)
            {
                if (Contains(t))
                {
                    return true;
                }
            }
            return false;
        }

        public bool SetEquals(IEnumerable<T> other)
        {
            if (other == null)
            {
                throw new ArgumentNullException("other cannot be null");
            }
            int otherCount = System.Linq.Enumerable.Count(other);
            if (Count != otherCount)
            {
                return false;
            }
            return IsSupersetOf(other);
        }

        public void SymmetricExceptWith(IEnumerable<T> other)
        {
            if (other == null)
            {
                throw new ArgumentNullException("other cannot be null");
            }
            T[] ts = new T[Count];
            CopyTo(ts, 0);
            HashSet<T> otherList = new HashSet<T>(other);
            foreach (T t in ts)
            {
                if (otherList.Contains(t))
                {
                    Remove(t);
                    otherList.Remove(t);
                }
            }
            foreach (T t in otherList)
            {
                Add(t);
            }
        }

        public void UnionWith(IEnumerable<T> other)
        {
            if (other == null)
            {
                throw new ArgumentNullException("other cannot be null");
            }
            foreach (T t in other)
            {
                Add(t);
            }
        }

        //
        // ICollection<T> implementation
        //

        public int Count
        {
            get
            {
                return this.dict.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return this.dict.IsReadOnly;
            }
        }

        void ICollection<T>.Add(T item)
        {
            Add(item);
        }

        public void Clear()
        {
            this.dict.Clear();
            this.list.Clear();
        }

        public bool Contains(T item)
        {
            return this.dict.ContainsKey(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            this.list.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            LinkedListNode<T> node;
            if (!this.dict.TryGetValue(item, out node))
            {
                return false;
            }
            this.dict.Remove(item);
            this.list.Remove(node);
            return true;
        }

        //
        // IEnumerable<T> implementation
        //

        public IEnumerator<T> GetEnumerator()
        {
            return this.list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.list.GetEnumerator();
        }

        public override string ToString()
        {
            return $"{this.Count}";
        }

        #region Serialization
        public void Serialize(StreamWriter writer)
        {
            HtmSerializer2 ser = new HtmSerializer2();

            ser.SerializeBegin(nameof(LinkedHashSet<T>), writer);

            //ser.SerializeValue(this.dict, writer);
            //ser.SerializeValue(this.list, writer);

            ser.SerializeEnd(nameof(LinkedHashSet<T>), writer);
        }

        public void Serialize(object obj, string name, StreamWriter sw)
        {
            HtmSerializer2.Serialize(this.list, null, sw);
        }

        public static object Deserialize(StreamReader sr, string name)
        {
            var list = HtmSerializer2.Deserialize<List<T>>(sr, null);
            return new LinkedHashSet<T>(list);
        }
        #endregion

    }

}
