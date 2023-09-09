class MultiCounter<T> : Dictionary<T, long> {
  [MI(256)] public MultiCounter() { }
  [MI(256)] public MultiCounter(int cap) : base(cap) { }
  [MI(256)] public MultiCounter(IEqualityComparer<T> cmp) : base(cmp) { }
  [MI(256)] public MultiCounter(int cap, IEqualityComparer<T> cmp) : base(cap, cmp) { }
  [MI(256)] public MultiCounter(IEnumerable<KeyValuePair<T, long>> seq) : base(seq) { }
  [MI(256)] public MultiCounter(IEnumerable<KeyValuePair<T, long>> seq, IEqualityComparer<T> cmp) : base(seq, cmp) { }
  [MI(256)] public MultiCounter(IEnumerable<T> seq) { foreach (var x in seq) this[x]++; }

  public new long this[T k] {
    [MI(256)]
    get { return base.TryGetValue(k, out var v) ? v : 0; }
    [MI(256)]
    set { base[k] = value; }
  }

  [MI(256)] public void EnsureContains(T key) { if (base.ContainsKey(key)) base[key] = 0; }

  [MI(256)]
  public void Swap(T key1, T key2) {
    var b1 = base.TryGetValue(key1, out var v1);
    var b2 = base.TryGetValue(key2, out var v2);
    if (b1 && b2) {
      this[key1] = v2; this[key2] = v1;
    } else if (b1) {
      base.Remove(key1); this[key2] = v1;
    } else if (b2) {
      base.Remove(key2); this[key1] = v1;
    }
  }

  [MI(256)]
  public bool ChMax(T key, long value) {
    if (!base.TryGetValue(key, out var v)) {
      this[key] = Math.Max(0, value); return value > 0;
    }
    if (v < value) { this[key] = value; return true; }
    return false;
  }

  [MI(256)]
  public bool ChMin(T key, long value) {
    if (!base.TryGetValue(key, out var v)) {
      this[key] = Math.Min(0, value); return value < 0;
    }
    if (v > value) { this[key] = value; return true; }
    return false;
  }
}
