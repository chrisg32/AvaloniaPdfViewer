namespace AvaloniaPdfViewer.Internals;

/// <summary>
/// Custom queue implementation using a linked list. This allows for O(1) removal of elements.
/// </summary>
/// <typeparam name="T"></typeparam>
internal class LinkedListQueue<T>
{
    private readonly LinkedList<T> _list = new();

    public void Enqueue(T item)
    {
        _list.AddLast(item);
    }

    public T Dequeue()
    {
        if (_list.Count == 0)
            throw new InvalidOperationException("The queue is empty.");

        T value = _list.First!.Value;
        _list.RemoveFirst();
        return value;
    }

    public bool Remove(T item)
    {
        return _list.Remove(item);
    }
    
    public void Clear()
    {
        _list.Clear();
    }

    public int Count => _list.Count;
}