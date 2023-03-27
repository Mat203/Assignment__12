static void Main()
{
    LinkedList list = new LinkedList();
    list.Add(new KeyValuePair("apple", "red"));
    list.Add(new KeyValuePair("banana", "yellow"));
    list.Add(new KeyValuePair("orange", "orange"));


    list.Print();
    list.RemoveByKey("banana");
    
    Console.WriteLine("Linked list after remove");
    list.Print();

    KeyValuePair orangePair = list.GetItemWithKey("orange");
    Console.WriteLine($"Orange pair: {orangePair.Value}");
}

Main();
public class KeyValuePair
    {
        public string Key { get; }

        public string Value { get; }

        public KeyValuePair(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }

public class LinkedListNode
    {
        public KeyValuePair Pair { get; }
        
        public LinkedListNode Next { get; set; }

        public LinkedListNode(KeyValuePair pair, LinkedListNode next = null)
        {
            Pair = pair;
            Next = next;
        }
    }

public class LinkedList
    {
        private LinkedListNode _first;

        public void Add(KeyValuePair pair)
        {
            LinkedListNode addNode = new LinkedListNode(pair);
            addNode.Next = _first;
            _first = addNode;
        }

        public void RemoveByKey(string key)
        {
            LinkedListNode current = _first;
            LinkedListNode previous = null;

            while (current != null && current.Pair.Key != key)
            {
                previous = current;
                current = current.Next;
            }

            if (current != null)
            {
                if (previous == null)
                {
                    _first = current.Next;
                }
                else
                {
                    previous.Next = current.Next;
                }
            }
        }

        public KeyValuePair GetItemWithKey(string key)
        {
            LinkedListNode current = _first;

            while (current != null && current.Pair.Key != key)
            {
                current = current.Next;
            }

            if (current != null)
            {
                return current.Pair;
            }

            return null;
        }
        public void Print()
        {
            LinkedListNode current = _first;

            while (current != null)
            {
                Console.WriteLine($"{current.Pair.Key}: {current.Pair.Value}");
                current = current.Next;
            }
        }
    }


public class StringsDictionary<TKey, TValue>
{
    private const int InitialSize = 10;

    private LinkedList<KeyValuePair<TKey, TValue>>[] buckets;
    int CalculateHash(string key)
    {
        int hash = 0;
        foreach (char c in key)
        {
            hash += (int)c;
        }
        return hash;
    }
    void Get(string key)
    {
        int bucketIndex = CalculateHash(key);
        if (buckets[bucketIndex] != null)
        {
            {
                foreach (KeyValuePair<TKey, TValue> pair in buckets[bucketIndex])
                {
                    if (pair.Key.Equals(key))
                    {
                        var value = pair.Value;
                        return;
                    }

                }
            }
        }

    }

    public void Add(TKey key, TValue value)
    {
        int bucketIndex = Get(key);
        if (buckets[bucketIndex] == null)
        {
            buckets[bucketIndex] = new LinkedList<KeyValuePair<TKey, TValue>>();
        }

        foreach (KeyValuePair<TKey, TValue> pair in buckets[bucketIndex])
        {
            if (pair.Key.Equals(key))
            {
                throw new ArgumentException("An element with the same key already exists in the dictionary.");
            }

            buckets[bucketIndex].AddLast(new KeyValuePair<TKey, TValue>(key, value));
        }

        void Remove(string key)
        {

        }

        
        int Index(TKey key)
        {
            int hash = key.GetHashCode();
            return hash % buckets.Length;
        }

        
    }
    
}