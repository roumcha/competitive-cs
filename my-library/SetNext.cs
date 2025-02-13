#nullable enable

/// <summary>赤黒木を使った順序付き集合</summary>
public class _Set<T> :
  ICollection,
  ICollection<T>,
  IReadOnlyCollection<T>,
  IEnumerable,
  IEnumerable<T>,
  ISet<T>,
  IReadOnlySet<T> {

  enum Color : byte { Black, Red, }

  Node? _root;
  readonly Comparer<T> _comparer;
  public int Count { [MI(R256)] get => _root?.Count ?? 0; }
  public object SyncRoot { [MI(R256)] get; } = new();
  public bool IsMultiSet { [MI(R256)] get; [MI(R256)] set; } = false;
  public bool IsReadOnly { [MI(R256)] get => false; }
  public bool IsSynchronized { [MI(R256)] get => false; }


  #region constractors

  /// <remarks>O(1)</remarks>
  [MI(R256)] public _Set(Comparer<T> cmp) { _comparer = cmp; }
  /// <remarks>O(1)</remarks>
  [MI(R256)] public _Set() : this(Comparer<T>.Default) { }
  /// <remarks>O(1)</remarks>
  [MI(R256)] public _Set(Comparison<T> cmp) : this(Comparer<T>.Create(cmp)) { }

  /// <param name="orderedItems">
  ///   初期要素の列。<paramref name="cmp"/> 順でソート済みであること。
  /// </param>
  /// <remarks>O(N)</remarks>
  [MI(R256)]
  public _Set(ReadOnlySpan<T> orderedItems, Comparer<T> cmp) {
    _root = Node.CreateBlackOnlyTree(orderedItems);
    _comparer = cmp;
  }

  /// <param name="orderedItems">
  ///   初期要素の列。ソート済みであること。
  /// </param>
  /// <remarks>O(N)</remarks>
  [MI(R256)] public _Set(ReadOnlySpan<T> orderedItems) : this(orderedItems, Comparer<T>.Default) { }

  /// <param name="orderedItems">
  ///   初期要素の列。 <paramref name="cmp"/> 順でソート済みであること。
  /// </param>
  /// <remarks>O(N)</remarks>
  [MI(R256)] public _Set(ReadOnlySpan<T> orderedItems, Comparison<T> cmp) : this(orderedItems, Comparer<T>.Create(cmp)) { }

  #endregion constractors


  #region public_methods

  /// <remarks>O(log N)</remarks>
  [MI(R256)] public bool Add(T value) => this.Insert(ref _root, value);
  /// <remarks>O(log N)</remarks>
  public bool Contains(T item) { throw new NotImplementedException(); }
  /// <remarks>O(log N)</remarks>
  public int CountOf(T value) => throw new NotImplementedException();
  /// <remarks>O(log N)</remarks>
  public bool Remove(T value) => throw new NotImplementedException();
  /// <remarks>O(log N)</remarks>
  public bool RemoveAt(T index) => throw new NotImplementedException();
  /// <remarks>O(log N)</remarks>
  public bool LowerBound(T value) => throw new NotImplementedException();
  /// <remarks>O(log N)</remarks>
  public bool UpperBound(T value) => throw new NotImplementedException();
  /// <remarks>O(log N)</remarks>
  public T this[int index] { [MI(R256)] get { throw new NotImplementedException(); } }
  /// <remarks>O(1)</remarks>
  public void Clear() { throw new NotImplementedException(); }
  /// <remarks>O(log N)</remarks>
  void ICollection<T>.Add(T item) => this.Add(item);
  public bool IsProperSubsetOf(IEnumerable<T> other) { throw new NotImplementedException(); }
  public bool IsProperSupersetOf(IEnumerable<T> other) { throw new NotImplementedException(); }
  public bool IsSubsetOf(IEnumerable<T> other) { throw new NotImplementedException(); }
  public bool IsSupersetOf(IEnumerable<T> other) { throw new NotImplementedException(); }
  public bool Overlaps(IEnumerable<T> other) { throw new NotImplementedException(); }
  public bool SetEquals(IEnumerable<T> other) { throw new NotImplementedException(); }
  public void ExceptWith(IEnumerable<T> other) { throw new NotImplementedException(); }
  public void IntersectWith(IEnumerable<T> other) { throw new NotImplementedException(); }
  public void SymmetricExceptWith(IEnumerable<T> other) { throw new NotImplementedException(); }
  public void UnionWith(IEnumerable<T> other) { throw new NotImplementedException(); }
  /// <remarks>O(N)</remarks>
  public void CopyTo(T[] array, int arrayIndex) { throw new NotImplementedException(); }
  public IEnumerator<T> GetEnumerator() { throw new NotImplementedException(); }
  IEnumerator IEnumerable.GetEnumerator() { return this.GetEnumerator(); }
  public void CopyTo(Array array, int index) { throw new NotImplementedException(); }

  #endregion public_methods


  #region private_methods

  bool Insert(ref Node? node, T value) {
    // NIL を発見したらそこへ挿入
    if (node == null) {
      node = new Node {
        Color = Color.Red,
        Value = value,
        Left = null, Right = null,
        Count = 1
      };
      return true;
    }

    // 今のノードと比較。小さいなら左へ、大きいなら右へ降りる。
    bool res;
    switch (_comparer.Compare(value, node.Value)) {
      case < 0:
        res = this.Insert(ref node.Left, value);
        break;
      case > 0:
        res = this.Insert(ref node.Right, value);
        break;
      case 0:
        // 同一の値がすでにある場合、マルチセットなら挿入、そうでなければ挿入しない。
        if (this.IsMultiSet) res = this.Insert(ref node.Left, value);
        else return false;
        break;
    }

    node.Update();
    return res;
  }

  #endregion private_methods


  sealed class Node {
    public required Color Color;
    public required T Value;
    public required Node? Left, Right;
    public required int Count;

    /// <summary>左回転して、あらたに自分の位置に来るノードを返す</summary>
    /// <remarks>プロパティ更新なし; O(1)</remarks>
    [MI(R256)]
    public Node RotateLeft() {
      Debug.Assert(Right is not null);
      Node child = Right!;
      Right = child.Left;
      child.Left = this;
      return child;
    }

    /// <summary>右回転して、あらたに自分の位置に来るノードを返す</summary>
    /// <remarks>プロパティ更新なし; O(1)</remarks>
    [MI(R256)]
    public Node RotateRight() {
      Debug.Assert(Left is not null);
      Node child = Left!;
      Left = child.Right;
      child.Right = this;
      return child;
    }

    [MI(R256)]
    public void Update() {
      Count = Left?.Count ?? 0 + Right?.Count ?? 0 + 1;
    }

    /// <summary>黒ノードのみで木を初期化</summary>
    /// <remarks>O(N)</remarks>
    public static Node? CreateBlackOnlyTree(ReadOnlySpan<T> span) {
      int count = span.Length, mid = count >> 1;
      if (count == 0) return null;

      Node? left = CreateBlackOnlyTree(span[..mid]);
      Node? right = CreateBlackOnlyTree(span[(mid + 1)..]);

      return new() {
        Color = Color.Black,
        Value = span[count / 2],
        Left = left, Right = right,
        Count = count
      };
    }
  }
}
