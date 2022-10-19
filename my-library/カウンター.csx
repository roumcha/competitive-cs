// using System.Runtime.CompilerServices;

class MultiCounter<T> : Dictionary<T, int>
{
  [MethodImpl(256)] public MultiCounter() { }
  [MethodImpl(256)] public MultiCounter(int cap) : base(cap) { }
  [MethodImpl(256)]
  public MultiCounter(IEqualityComparer<T> cmp) : base(cmp) { }
  [MethodImpl(256)]
  public MultiCounter(int cap, IEqualityComparer<T> cmp)
    : base(cap, cmp) { }
  [MethodImpl(256)]
  public MultiCounter(IEnumerable<KeyValuePair<T, int>> seq)
    : base(seq) { }
  [MethodImpl(256)]
  public MultiCounter(
    IEnumerable<KeyValuePair<T, int>> seq, IEqualityComparer<T> cmp)
    : base(seq, cmp) { }
  public new int this[T k]
  {
    [MethodImpl(256)]
    get { return this.TryGetValue(k, out var v) ? v : base[k] = 0; }
    [MethodImpl(256)]
    set { base[k] = value; }
  }
}
