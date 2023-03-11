public class Deque<T>
: IEnumerable<T>, IReadOnlyCollection<T>, ICollection<T> {
  public T[] data; public int mask, head, tail;

  public Deque() : this(8) { }
  public Deque(int capacity) {
    capacity =
      capacity <= 8 ? 8 : 1 << (CeilPow2(capacity + 1));
    data = new T[capacity];
    mask = capacity - 1;
  }

  public int Count => (tail - head) & mask;
  public ref T this[int i] => ref data[(head + i) & mask];
  public T First => data[head];
  public T Last => data[(tail - 1) & mask];
  static void ThrowDequeIsEmpty()
  => throw new InvalidOperationException("deque is empty");

  [MI(256)]
  public T PopFirst() {
    if (head == tail) ThrowDequeIsEmpty();
    var item = data[head];
    head = (head + 1) & mask;
    return item;
  }
  [MI(256)]
  public T PopLast() {
    if (head == tail) ThrowDequeIsEmpty();
    return data[tail = (tail - 1) & mask];
  }
  [MI(256)]
  public void AddFirst(T item) {
    data[head = (head - 1) & mask] = item;
    if (head == tail) Resize();
  }
  [MI(256)]
  public void AddLast(T item) {
    data[tail] = item;
    tail = (tail + 1) & mask;
    if (head == tail) Resize();
  }
  public void Resize() {
    var oldSize = data.Length;
    var newArray = new T[oldSize << 1];
    var hsize = oldSize - head;
    Array.Copy(data, head, newArray, 0, hsize);
    Array.Copy(data, 0, newArray, hsize, tail);
    data = newArray;
    mask = data.Length - 1;
    head = 0;
    tail = oldSize;
  }
  [MI(256)] public void Clear() => head = tail = 0;
  public void Add(T item) => this.AddLast(item);
  bool ICollection<T>.Contains(T item) {
    if (head == tail) return false;

    if (head < tail) {
      var ix = Array.IndexOf(data, item, head);
      return ix >= 0 && ix < tail;
    } else {
      var ix = Array.IndexOf(data, item, head);
      if (ix >= 0) return true;
      ix = Array.IndexOf(data, item);
      return (uint)ix < tail;
    }
  }

  public void CopyTo(T[] array, int arrayIndex) {
    if (head <= tail) {
      Array.Copy(data, head, array, arrayIndex, tail - head);
    } else {
      var hsize = data.Length - head;
      Array.Copy(data, head, array, arrayIndex, hsize);
      Array.Copy(data, 0, array, arrayIndex + hsize, tail);
    }
  }
  [MI(256)]
  public Enumerator Reversed() => new Enumerator(this, true);
  [MI(256)]
  public Enumerator GetEnumerator() => new Enumerator(this, false);
  bool ICollection<T>.IsReadOnly => false;
  bool ICollection<T>.Remove(T item)
  => throw new NotSupportedException();
  IEnumerator<T> IEnumerable<T>.GetEnumerator()
  => this.GetEnumerator();
  IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
  [MI(256)]
  static int CeilPow2(int n) {
    var un = (uint)n;
    return un <= 1
      ? 0 : System.Numerics.BitOperations.Log2(un - 1) + 1;
  }
  public struct Enumerator : IEnumerator<T>, IEnumerable<T> {
    readonly Deque<T> deque;
    readonly bool isReverse;
    int index;
    public readonly int last;
    public T Current => deque.data[index];
    public Enumerator(Deque<T> deque, bool isReverse) {
      this.deque = deque;
      this.isReverse = isReverse;
      if (isReverse) {
        index = deque.tail;
        last = deque.head;
      } else {
        index = deque.head - 1;
        last = (deque.tail - 1) & deque.mask;
      }
    }
    object IEnumerator.Current => Current;
    [MI(256)]
    public bool MoveNext() {
      if (index == last) return false;
      if (isReverse) --index; else ++index;
      index &= deque.mask;
      return true;
    }
    public void Reset() { throw new NotSupportedException(); }
    [MI(256)] public Enumerator GetEnumerator() => this;
    IEnumerator<T> IEnumerable<T>.GetEnumerator() => this;
    IEnumerator IEnumerable.GetEnumerator() => this;
    public void Dispose() { }
  }
}
