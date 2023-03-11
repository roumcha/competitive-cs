public class Set<T> {
  Node root; readonly IComparer<T> comparer; readonly Node nil;
  public bool IsMultiSet { [MI(256)] get; [MI(256)] set; }
  [MI(256)]
  public Set(IComparer<T> comparer) {
    nil = new Node(default(T));
    root = nil;
    this.comparer = comparer;
  }
  [MI(256)] public Set(Comparison<T> comaprison) : this(Comparer<T>.Create(comaprison)) { }
  [MI(256)] public Set() : this(Comparer<T>.Default) { }
  /// <remarks>O(log N)</remarks>
  [MI(256)] public bool Add(T v) => insert(ref root, v);
  /// <remarks>O(log N)</remarks>
  [MI(256)] public bool Remove(T v) => remove(ref root, v);
  /// <remarks>O(log N)</remarks>
	public T this[int index] { [MI(256)] get { return find(root, index); } }
  public int Count { [MI(256)] get { return root.Count; } }
  [MI(256)]
  public void RemoveAt(int k) {
    if (k < 0 || k >= root.Count) throw new ArgumentOutOfRangeException();
    removeAt(ref root, k);
  }
  /// <remarks>O(N)</remarks>
  public T[] Items {
    [MI(256)]
    get {
      var ret = new T[root.Count];
      var k = 0;
      walk(root, ret, ref k);
      return ret;
    }
  }
  [MI(256)]
  void walk(Node t, T[] a, ref int k) {
    if (t.Count == 0) return;
    walk(t.lst, a, ref k);
    a[k++] = t.Key;
    walk(t.rst, a, ref k);
  }

  [MI(256)]
  bool insert(ref Node t, T key) {
    if (t.Count == 0) { t = new Node(key); t.lst = t.rst = nil; t.Update(); return true; }
    var cmp = comparer.Compare(t.Key, key);
    bool res;
    if (cmp > 0)
      res = insert(ref t.lst, key);
    else if (cmp == 0) {
      if (IsMultiSet) res = insert(ref t.lst, key);
      else return false;
    } else res = insert(ref t.rst, key);
    balance(ref t);
    return res;
  }
  [MI(256)]
  bool remove(ref Node t, T key) {
    if (t.Count == 0) return false;
    var cmp = comparer.Compare(key, t.Key);
    bool ret;
    if (cmp < 0) ret = remove(ref t.lst, key);
    else if (cmp > 0) ret = remove(ref t.rst, key);
    else {
      ret = true;
      var k = t.lst.Count;
      if (k == 0) { t = t.rst; return true; }
      if (t.rst.Count == 0) { t = t.lst; return true; }

      t.Key = find(t.lst, k - 1);
      removeAt(ref t.lst, k - 1);
    }
    balance(ref t);
    return ret;
  }
  [MI(256)]
  void removeAt(ref Node t, int k) {
    var cnt = t.lst.Count;
    if (cnt < k) removeAt(ref t.rst, k - cnt - 1);
    else if (cnt > k) removeAt(ref t.lst, k);
    else {
      if (cnt == 0) { t = t.rst; return; }
      if (t.rst.Count == 0) { t = t.lst; return; }

      t.Key = find(t.lst, k - 1);
      removeAt(ref t.lst, k - 1);
    }
    balance(ref t);
  }
  [MI(256)]
  void balance(ref Node t) {
    var balance = t.lst.Height - t.rst.Height;
    if (balance == -2) {
      if (t.rst.lst.Height - t.rst.rst.Height > 0) { rotR(ref t.rst); }
      rotL(ref t);
    } else if (balance == 2) {
      if (t.lst.lst.Height - t.lst.rst.Height < 0) rotL(ref t.lst);
      rotR(ref t);
    } else t.Update();
  }

  [MI(256)]
  T find(Node t, int k) {
    if (k < 0 || k > root.Count) throw new ArgumentOutOfRangeException();
    for (; ; )
    {
      if (k == t.lst.Count) return t.Key;
      else if (k < t.lst.Count) t = t.lst;
      else { k -= t.lst.Count + 1; t = t.rst; }
    }
  }
  /// <remarks>O(log N)</remarks>
  [MI(256)]
  public int LowerBound(T v) {
    var k = 0;
    var t = root;
    for (; ; )
    {
      if (t.Count == 0) return k;
      if (comparer.Compare(v, t.Key) <= 0) t = t.lst;
      else { k += t.lst.Count + 1; t = t.rst; }
    }
  }
  /// <remarks>O(log N)</remarks>
  [MI(256)]
  public int UpperBound(T v) {
    var k = 0;
    var t = root;
    for (; ; )
    {
      if (t.Count == 0) return k;
      if (comparer.Compare(t.Key, v) <= 0) { k += t.lst.Count + 1; t = t.rst; } else t = t.lst;
    }
  }

  [MI(256)]
  void rotR(ref Node t) {
    var l = t.lst;
    t.lst = l.rst;
    l.rst = t;
    t.Update();
    l.Update();
    t = l;
  }
  [MI(256)]
  void rotL(ref Node t) {
    var r = t.rst;
    t.rst = r.lst;
    r.lst = t;
    t.Update();
    r.Update();
    t = r;
  }

  class Node {
    [MI(256)] public Node(T key) { Key = key; }
    public int Count { [MI(256)] get; [MI(256)] private set; }
    public sbyte Height { [MI(256)] get; [MI(256)] private set; }
    public T Key { [MI(256)] get; [MI(256)] set; }
    public Node lst, rst;
    [MI(256)]
    public void Update() {
      Count = 1 + lst.Count + rst.Count;
      Height = (sbyte)(1 + Math.Max(lst.Height, rst.Height));
    }
    [MI(256)]
    public override string ToString()
      => string.Format("Count = {0}, Key = {1}", Count, Key);
  }
}
