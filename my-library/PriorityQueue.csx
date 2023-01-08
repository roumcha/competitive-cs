// using System.Runtime.CompilerServices;

// kzrnm/ac-library-csharp から一部変更

/// <remarks>デフォルトは小さい順</remarks>
class PriorityQueue_2<T> : PriorityQueueOp<T, ComparableComparer<T>>
  where T : IComparable<T> {
  public PriorityQueue_2() : base(new ComparableComparer<T>()) { }
  public PriorityQueue_2(int capacity)
    : base(capacity, new ComparableComparer<T>()) { }
  public PriorityQueue_2(IComparer<T> comparer)
    : base(new ComparableComparer<T>(comparer)) { }
  public PriorityQueue_2(int capacity, IComparer<T> comparer)
    : base(capacity, new ComparableComparer<T>(comparer)) { }
}

class PriorityQueueOp<T, TOp> : IPriorityQueueOp<T>
  where TOp : IComparer<T> {
  protected T[] data;
  protected readonly TOp _comparer;
  internal const int DefaultCapacity = 16;
  public PriorityQueueOp() : this(default(TOp)) { }
  public PriorityQueueOp(int capacity)
    : this(capacity, default(TOp)) { }
  public PriorityQueueOp(TOp comparer)
    : this(DefaultCapacity, comparer) { }
  public PriorityQueueOp(int capacity, TOp comparer) {
    if (comparer == null)
      throw new ArgumentNullException(nameof(comparer));
    data = new T[Math.Max(capacity, DefaultCapacity)];
    _comparer = comparer;
  }
  [DebuggerBrowsable(0)]
  public int Count { get; private set; } = 0;

  public T Peek => data[0];
  [MethodImpl(256)]
  internal void Resize() { Array.Resize(ref data, data.Length << 1); }
  [MethodImpl(256)]
  public void Enqueue(T value) {
    if (Count >= data.Length) Resize();
    data[Count++] = value;
    UpdateUp(Count - 1);
  }
  [MethodImpl(256)]
  public bool TryDequeue(out T result) {
    if (Count == 0) { result = default(T); return false; }
    result = Dequeue(); return true;
  }
  [MethodImpl(256)]
  public T Dequeue() {
    var res = data[0];
    data[0] = data[--Count];
    UpdateDown(0);
    return res;
  }
  /// <summary>
  /// <paramref name="value"/> を Enqueue(T) してから Dequeue します。
  /// </summary>
  [MethodImpl(256)]
  public T EnqueueDequeue(T value) {
    var res = data[0];
    if (_comparer.Compare(value, res) <= 0) return value;
    data[0] = value;
    UpdateDown(0);
    return res;
  }
  /// <summary>
  /// Dequeue した値に <paramref name="func"/> を適用して Enqueue(T) します。
  /// </summary>
  [MethodImpl(256)]
  public void DequeueEnqueue(Func<T, T> func) { data[0] = func(data[0]); UpdateDown(0); }
  [MethodImpl(256)]
  protected internal void UpdateUp(int i) {
    var tar = data[i];
    while (i > 0) {
      var p = (i - 1) >> 1;
      if (_comparer.Compare(tar, data[p]) >= 0) break;
      data[i] = data[p]; i = p;
    }
    data[i] = tar;
  }
  [MethodImpl(256)]
  protected internal void UpdateDown(int i) {
    var tar = data[i];
    int n = Count, child = 2 * i + 1;
    while (child < n) {
      if (child != n - 1 &&
        _comparer.Compare(data[child], data[child + 1]) > 0) child++;
      if (_comparer.Compare(tar, data[child]) <= 0) break;
      data[i] = data[child]; i = child; child = 2 * i + 1;
    }
    data[i] = tar;
  }
  [MethodImpl(256)] public void Clear() => Count = 0;

  public ReadOnlySpan<T> Unorderd() => data.AsSpan(0, Count);
  private class DebugView {
    private readonly PriorityQueueOp<T, TOp> pq;
    public DebugView(PriorityQueueOp<T, TOp> pq) { this.pq = pq; }
    [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
    public T[] Items {
      get {
        var arr = pq.Unorderd().ToArray();
        Array.Sort(arr, pq._comparer);
        return arr;
      }
    }
  }
}

interface IPriorityQueueOp<T> {
  int Count { get; }
  T Peek { get; }
  void Enqueue(T value);
  T Dequeue();
  bool TryDequeue(out T result);
  void Clear();
}

struct ComparableComparer<T>
  : IComparer<T>, IEquatable<ComparableComparer<T>>
  where T : IComparable<T> {
  private IComparer<T> Comparer { get; }
  public ComparableComparer(IComparer<T> comparer) { Comparer = comparer; }
  [MethodImpl(256)]
  public int Compare(T x, T y)
    => Comparer?.Compare(x, y) ?? x.CompareTo(y);
  #region Equatable
  public override bool Equals(object obj)
    => obj is ComparableComparer<T>
      && Equals((ComparableComparer<T>)obj);
  public bool Equals(ComparableComparer<T> other) {
    if (Comparer == null) return other.Comparer == null;
    return Comparer.Equals(other.Comparer);
  }
  public override int GetHashCode() => Comparer?.GetHashCode() ?? 0;
  public static bool operator ==(
    ComparableComparer<T> left, ComparableComparer<T> right)
    => left.Equals(right);
  public static bool operator !=(
    ComparableComparer<T> left, ComparableComparer<T> right)
    => !left.Equals(right);
  #endregion Equatable
}
