
// Created: 7th June 2019
// Updated: 8th June 2019

namespace Clasp.Internal
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;

    internal sealed class OrderedDict<T_key, T_value>
        : IDictionary<T_key, T_value>
        , ICollection<KeyValuePair<T_key, T_value>>
        , IEnumerable<KeyValuePair<T_key, T_value>>
        , IDictionary
        , ICollection
        , IEnumerable
#if NON_EXISTENT

        , ISerializable
        , IDeserializationCallback
#endif
    {
        #region fields

        private readonly IOrderedDictionary m_od;
        #endregion

        #region construction

        public OrderedDict()
        {
            m_od = new OrderedDictionary();
        }
        #endregion

        #region IDictionary<T_key, T_value>

        public void Add(T_key key, T_value value)
        {
            m_od.Add(key, value);
        }

        public bool ContainsKey(T_key key)
        {
            return m_od.Contains(key);
        }

        public ICollection<T_key> Keys
        {
            get
            {
                var keys = new List<T_key>(m_od.Keys.Count);

                foreach(T_key key in m_od.Keys)
                {
                    keys.Add(key);
                }

                return keys;
            }
        }

        public bool Remove(T_key key)
        {
            if(m_od.Contains(key))
            {
                m_od.Remove(key);

                return true;
            }

            return false;
        }

        public bool TryGetValue(T_key key, out T_value value)
        {
            if(m_od.Contains(key))
            {
                value = (T_value)m_od[key];

                return true;
            }
            else
            {
                value = default(T_value);

                return false;
            }
        }

        public ICollection<T_value> Values
        {
            get
            {
                var values = new List<T_value>(m_od.Values.Count);

                foreach(T_value value in m_od.Values)
                {
                    values.Add(value);
                }

                return values;
            }
        }

        public T_value this[T_key key]
        {
            get
            {
                return (T_value)m_od[key];
            }
            set
            {
                m_od[key] = value;
            }
        }

        public void Add(KeyValuePair<T_key, T_value> item)
        {
            m_od.Add(item.Key, item.Value);
        }

        public void Clear()
        {
            m_od.Clear();
        }

        public bool Contains(KeyValuePair<T_key, T_value> item)
        {
            T_value value;

            if(!TryGetValue(item.Key, out value))
            {
                return false;
            }

            if(!Object.Equals(item.Value, value))
            {
                return false;
            }

            return true;
        }

        public void CopyTo(KeyValuePair<T_key, T_value>[] array, int arrayIndex)
        {
            m_od.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get
            {
                return m_od.Count;    
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public bool Remove(KeyValuePair<T_key, T_value> item)
        {
            if(!m_od.Contains(item.Key))
            {
                return false;
            }

            if(!Object.Equals(m_od[item.Key], item.Value))
            {
                return false;
            }

            m_od.Remove(item.Key);

            return true;
        }

        public IEnumerator<KeyValuePair<T_key, T_value>> GetEnumerator()
        {
            return GetEnumerator_();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator_();
        }
        #endregion

        #region IDictionary

        public void Add(object key, object value)
        {
            m_od.Add(key, value);
        }

        public bool Contains(object key)
        {
            return m_od.Contains(key);
        }

        IDictionaryEnumerator IDictionary.GetEnumerator()
        {
            return m_od.GetEnumerator();
        }

        public bool IsFixedSize
        {
            get
            {
                return false;
            }
        }

        ICollection IDictionary.Keys
        {
            get
            {
                return m_od.Keys;
            }
        }

        public void Remove(object key)
        {
            m_od.Remove(key);
        }

        ICollection IDictionary.Values
        {
            get
            {
                return m_od.Values;
            }
        }

        public object this[object key]
        {
            get
            {
                return m_od[key];
            }
            set
            {
                m_od[key] = value;
            }
        }

        public void CopyTo(Array array, int index)
        {
            m_od.CopyTo(array, index);
        }

        public bool IsSynchronized
        {
            get
            {
                return m_od.IsSynchronized;
            }
        }

        public object SyncRoot
        {
            get
            {
                return m_od.SyncRoot;
            }
        }
        #endregion

        #region implementation

        private IEnumerator<KeyValuePair<T_key, T_value>> GetEnumerator_()
        {
            foreach(DictionaryEntry pair in m_od)
            {
                yield return new KeyValuePair<T_key, T_value>((T_key)pair.Key, (T_value)pair.Value);
            }
        }
        #endregion
    }
}
