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

  #region fields_and_props

  Node? _root;
  readonly Comparer<T> _comparer;
  public int Count { [MI(R256)] get => _root?.Count ?? 0; }
  public object SyncRoot { [MI(R256)] get; } = new();
  public bool IsMultiSet { [MI(R256)] get; [MI(R256)] set; } = false;
  public bool IsReadOnly { [MI(R256)] get => false; }
  public bool IsSynchronized { [MI(R256)] get => false; }

  #endregion fields_and_props


  #region constractors

  /// <remarks>O(1)</remarks>
  [MI(R256)] public _Set(Comparer<T> cmp) { _comparer = cmp; }
  /// <remarks>O(1)</remarks>
  [MI(R256)] public _Set() : this(Comparer<T>.Default) { }
  /// <remarks>O(1)</remarks>
  [MI(R256)] public _Set(Comparison<T> cmp) : this(Comparer<T>.Create(cmp)) { }

  #endregion constractors


  #region public_methods

  /// <remarks>O(log N)</remarks>
  [MI(R256)] public bool Add(T value) => this.Insert(ref _root, value) != null;
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

  /// <summary><paramref name="subtree"/> 以下に <paramref name="value"/> を挿入します。</summary>
  /// <returns>
  ///   <c>Node</c>: 挿入された場合はそのノード、挿入されなかった場合は <c>null</c>。 <c>Balanced</c>: 上の木の確認・修正が必要か否か。
  /// </returns>
  (Node? Node, bool Balanced) Insert(ref Node? subtree, T value) {
    // NIL を発見したらそこへ挿入して、バランス修正を開始
    if (subtree == null) {
      subtree = new Node {
        Color = Color.Red,
        Value = value,
        Left = null, Right = null,
        Count = 1
      };
      return (subtree, false);
    }

    // 今のノードと比較。小さいなら左へ、大きいなら右へ降りる。
    Node? node;
    bool balanced;
    switch (_comparer.Compare(value, subtree.Value)) {
      case < 0:
        (node, balanced) = this.Insert(ref subtree.Left, value);
        break;
      case > 0:
        (node, balanced) = this.Insert(ref subtree.Right, value);
        break;
      case 0:
        // 同一の値がすでにある場合、マルチセットなら左へ降りて続行、そうでなければ中止。
        // 検索時も左にしか降りないようにするので、右に変更禁止。
        if (this.IsMultiSet) {
          (node, balanced) = this.Insert(ref subtree.Left, value);
        } else {
          return (null, true);
        }
        break;
    }

    subtree.Update();
    return node;
  }

  /// <summary><paramref name="node"/> 以下の部分木で、値 <paramref name="value"/> を持つノードを検索します。</summary>
  /// <returns>見つかったノード（の1つ）。なければ <c>null</c> 。</returns>
  [MI(R256)]
  Node? Find(Node? node, T value) {
    while (node != null) {
      int cmp = _comparer.Compare(value, node.Value);
      if (cmp == 0) return node;
      node = cmp < 0 ? node.Left : node.Right;
    }
    return null;
  }

  /// <summary><paramref name="node"/> 以下の部分木で、値 <paramref name="value"/> を持つノードを数えます。</summary>
  [MI(R256)]
  private int CountNodes(Node? node, T value) {
    if (node == null) return 0;
    int cmp = _comparer.Compare(value, node.Value);
    if (cmp == 0) return 1 + this.CountNodes(node.Left, value);
    return this.CountNodes(cmp < 0 ? node.Left : node.Right, value);
  }

  /// <summary>左の子を自身の位置に移動する、左回転を行います。</summary>
  /// <remarks>プロパティ更新なし。 O(1)。</remarks>
  /// <returns>回転後に自身の位置に来るノード</returns>
  [MI(R256)]
  static void RotateLeft(ref Node node) {
    Debug.Assert(node.Right is not null);
    Node child = node.Right!;
    node.Right = child.Left;
    child.Left = node;
    node = child;
  }

  /// <summary>左の子の位置を左回転をしたのち、自身の位置を右回転します。</summary>
  /// <remarks>プロパティ更新なし。 O(1)。</remarks>
  /// <returns>回転後に自身の位置に来るノード</returns>
  [MI(R256)]
  static void RotateLeftRight(ref Node node) {
    Debug.Assert(node.Left != null);
    Debug.Assert(node.Left.Right != null);
    Node child = node.Left!, grandChild = child.Right!;
    node.Left = grandChild.Right;
    grandChild.Right = node;
    child.Right = grandChild.Left;
    grandChild.Left = child;
    node = grandChild;
  }

  /// <summary>右の子を自身の位置に移動する、右回転を行います。</summary>
  /// <remarks>プロパティ更新なし。 O(1)。</remarks>
  /// <returns>回転後に自身の位置に来るノード</returns>
  [MI(R256)]
  static void RotateRight(ref Node node) {
    Debug.Assert(node.Left != null);
    Node child = node.Left!;
    node.Left = child.Right;
    child.Right = node;
    node = child;
  }

  /// <summary>右の子の位置を右回転したのち、自身の位置を左回転します。</summary>
  /// <remarks>プロパティ更新なし。 O(1)。</remarks>
  /// <returns>回転後に自身の位置に来るノード</returns>
  [MI(R256)]
  static void RotateRightLeft(ref Node node) {
    Debug.Assert(node.Right != null);
    Debug.Assert(node.Right.Left != null);
    Node child = node.Right!, grandChild = child.Left!;
    node.Right = grandChild.Left;
    grandChild.Left = node;
    child.Left = grandChild.Right;
    grandChild.Right = child;
    node = grandChild;
  }

  #endregion private_methods


  sealed class Node {
    public required Color Color;
    public required T Value;
    public required Node? Left, Right;
    public required int Count;

    /// <summary>各種プロパティを更新します。</summary>
    [MI(R256)]
    public void Update() {
      Count = (Left?.Count ?? 0) + (Right?.Count ?? 0) + 1;
    }
  }
}
