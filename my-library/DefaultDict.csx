// using System.Runtime.CompilerServices;

class DefaultDict<K, V> : Dictionary<K, V>
{
  private Func<V> _d = () => default(V);
  [MethodImpl(256)]
  public DefaultDict() : base() { }
  [MethodImpl(256)]
  public DefaultDict(int cap) : base(cap) { }
  [MethodImpl(256)]
  public DefaultDict(Func<V> def) : base() { _d = def; }
  [MethodImpl(256)]
  public DefaultDict(int cap, Func<V> def) : base(cap) { _d = def; }

  public new V this[K k]
  {
    [MethodImpl(256)]
    get
    { return this.TryGetValue(k, out var v) ? v : base[k] = _d(); }
    [MethodImpl(256)]
    set { base[k] = value; }
  }
}