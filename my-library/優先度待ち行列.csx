// using System.Runtime.CompilerServices;

/// <summary>
/// Dequeue, First, Peek は順序通り
/// デフォルトは大きい順
/// </summary>
class MyPriorityQueue<T> : IEnumerable<T>
{
  private List<T> _heap;
  private int _size = 0;
  private readonly Func<T, T, int> Compare;

  public int Count { [MethodImpl(256)] get { return _size; } }
  [MethodImpl(256)] public IEnumerator<T> GetEnumerator() => _heap.GetEnumerator();
  [MethodImpl(256)] System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => _heap.GetEnumerator();

  [MethodImpl(256)]
  public MyPriorityQueue(int capacity = 0, Func<T, T, int> compare = null)
  {
    _heap = capacity > 0 ? new List<T>(capacity) : new List<T>();
    Compare = compare ?? Comparer<T>.Default.Compare;
  }

  public T First => _heap[0];
  [MethodImpl(256)]
  public void TrimExcess()
  {
    if (_size < _heap.Count * 0.9f)
      _heap.RemoveRange(_size, _heap.Count - _size);
  }

  [MethodImpl(256)]
  private void Up(int pos)
  {
    var elem = _heap[pos];
    while (pos > 0)
    {
      int parentPos = (pos - 1) / 2;

      if (Compare(elem, _heap[parentPos]) <= 0) break;
      _heap[pos] = _heap[parentPos];
      pos = parentPos;
    }
    _heap[pos] = elem;
  }

  [MethodImpl(256)]
  private void Down(int pos)
  {
    var elem = _heap[pos];
    int childPos;
    while ((childPos = pos * 2 + 1) < _size)
    {
      if (childPos + 1 < _size)
        childPos = Compare(_heap[childPos], _heap[childPos + 1]) >= 0
          ? childPos
          : childPos + 1;

      if (Compare(_heap[pos], _heap[childPos]) >= 0) break;
      _heap[pos] = _heap[childPos];
      pos = childPos;
    }
    _heap[pos] = elem;
  }

  [MethodImpl(256)]
  public void Enqueue(T element)
  {
    var pos = _size;
    if (_heap.Count <= _size) _heap.Add(element); else _heap[pos] = element;
    _size++;
    Up(pos);
  }

  [MethodImpl(256)]
  public T Dequeue()
  {
    var res = _heap[0];
    _heap[0] = _heap[_size - 1];
    _size--;
    Down(0);
    return res;
  }
}