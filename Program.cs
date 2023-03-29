static void Main()
{
    StringsDictionary<string, string> dictionary = new StringsDictionary<string, string>();
    foreach (var line in File.ReadAllLines("dict.txt"))
    {
        string[] elements = line.Split("; ");
        string key = elements[0];
        string value = String.Join("; ", elements[1..]);
        dictionary.Add(key, value);
    }
    while (true)
    {
        Console.Write("Write a word or /break to stop: ");
        string user_key = Console.ReadLine();
        if (user_key == "/break")
        {
            break;
        }
        string user_value = dictionary.GetValue(user_key);
        Console.WriteLine(user_value);
    }
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
    private int count;
    private LinkedList<KeyValuePair<TKey, TValue>>[] buckets = new LinkedList<KeyValuePair<TKey, TValue>>[InitialSize];

    public int GetHashCode(string key)
    {
        int hash = 0;
        foreach (char c in key)
        {
            hash += (int)c;
        }

        return hash;
    }

    public TValue GetValue(TKey key)
    {
        int bucketIndex = Index(key);
        if (buckets[bucketIndex] != null)
        {
            foreach (KeyValuePair<TKey, TValue> pair in buckets[bucketIndex])
            {
                if (pair.Key.Equals(key))
                {
                    return pair.Value;
                }
            }
        }
        throw new KeyNotFoundException("The given key was not present in the dictionary.");
    }

    public void Add(TKey key, TValue value)
    {
        int bucketIndex = Index(key);
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
        }

        buckets[bucketIndex].AddLast(new KeyValuePair<TKey, TValue>(key, value));
        count++;
    }

    public bool Remove(TKey key)
    {
        int bucketIndex = Index(key);
        if (buckets[bucketIndex] != null)
        {
            LinkedList<KeyValuePair<TKey, TValue>> bucket = buckets[bucketIndex];

            for (LinkedListNode<KeyValuePair<TKey, TValue>>? node = bucket.First; node != null; node = node.Next)
            {
                if (node.Value.Key.Equals(key))
                {
                    bucket.Remove(node);
                    count--;
                    if (bucket.Count == 0)
                    {
                        buckets[bucketIndex] = null;
                    }
                    return true;
                }
            }
        }

        return false;
    }

    int Index(TKey key)
    {
        int hash = (key is string strKey) ? GetHashCode(strKey) : key.GetHashCode();
        int bucketIndex = hash % InitialSize;
        return bucketIndex;
    }
}

