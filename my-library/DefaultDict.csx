class DefaultDict<K, V> : Dictionary<K, V> {
  Func<V> _d = () => default(V);
  [MI(256)] public DefaultDict() : base() { }
  [MI(256)] public DefaultDict(int cap) : base(cap) { }
  [MI(256)] public DefaultDict(Func<V> def) : base() { _d = def; }
  [MI(256)]
  public DefaultDict(int cap, Func<V> def) : base(cap) { _d = def; }

  public new V this[K k] {
    [MI(256)]
    get { return this.TryGetValue(k, out var v) ? v : base[k] = _d(); }
    [MI(256)]
    set { base[k] = value; }
  }
}
