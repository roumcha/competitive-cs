public class MultiCounter<T> : Dictionary<T, long> {
  public bool RemoveZeros { get; init; } = true;
  [MI(R256)] public MultiCounter() { }
  [MI(R256)] public MultiCounter(int cap) : base(cap) { }
  [MI(R256)] public MultiCounter(IEqualityComparer<T> cmp) : base(cmp) { }
  [MI(R256)] public MultiCounter(int cap, IEqualityComparer<T> cmp) : base(cap, cmp) { }
  [MI(R256)] public MultiCounter(IEnumerable<KeyValuePair<T, long>> seq) : base(seq) { }
  [MI(R256)] public MultiCounter(IEnumerable<KeyValuePair<T, long>> seq, IEqualityComparer<T> cmp) : base(seq, cmp) { }
  [MI(R256)] public MultiCounter(IEnumerable<T> seq) { foreach (var x in seq) this[x]++; }

  public new long this[T k] {
    [MI(R256)]
    get { base.TryGetValue(k, out var v); return v; }
    [MI(R256)]
    set {
      if (value == 0 && this.RemoveZeros) base.Remove(k);
      else base[k] = value;
    }
  }

  [MI(R256)]
  public void Swap(T key1, T key2)
  => (this[key1], this[key2]) = (this[key2], this[key1]);

  [MI(R256)]
  public bool ChMax(T key, long value) {
    if (this[key] < value) { this[key] = value; return true; }
    return false;
  }

  [MI(R256)]
  public bool ChMin(T key, long value) {
    if (this[key] > value) { this[key] = value; return true; }
    return false;
  }
}
