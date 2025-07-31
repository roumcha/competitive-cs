public class FloydWarshall<T>
  where T : struct, INumberBase<T>, IComparable<T> {

  public int N { get; init; }
  public T Inf { get; init; }
  readonly T[,] _dist;
  readonly HashSet<Edge> _pending = new();

  public bool IsPending => _pending.Count > 0;
  public T this[int from, int to] { [MI(R256)] get => _dist[from, to]; }
  public T[,] AsArray() => _dist;
  public Span<T> AsSpan() => _dist.AsSpan();


  /// <remarks>O(N^2)</remarks>
  [MI(R256)]
  public FloydWarshall(int n, T inf) {
    this.N = n;
    this.Inf = inf;
    _dist = new T[n, n];
    _dist.AsSpan().Fill(inf);
    for (int i = 0; i < n; i++) _dist[i, i] = T.Zero;
  }


  /// <summary>chmin して、全体に反映する</summary>
  /// <remarks>O(N^2)</remarks>
  [MI(R256)]
  public bool AddEdge_Immediate(int from, int to, T cost) {
    if (_dist[from, to].ChMin(cost)) {
      this.UpdateEdge(from, to, cost);
      return true;
    }
    return false;
  }


  /// <summary>chmin するが、全体への反映は保留する</summary>
  /// <remarks>O(1)</remarks>
  [MI(R256)]
  public bool AddEdge_Pending(int from, int to, T cost) {
    if (_dist[from, to].ChMin(cost)) {
      _pending.Add((from, to));
      return true;
    }
    return false;
  }


  /// <summary>単に chmin する。全体の更新は利用側に委ねられる。</summary>
  /// <remarks>O(1)</remarks>
  [MI(R256)]
  public bool AddEdge_Dangerous(int from, int to, T cost) {
    return _dist[from, to].ChMin(cost);
  }


  /// <remarks>O(N^3)</remarks>
  public void UpdateAll() {
    for (int k = 0; k < this.N; k++) {
      for (int i = 0; i < this.N; i++) {
        for (int j = 0; j < this.N; j++) {
          _dist[i, j].ChMin(_dist[i, k] + _dist[k, j]);
        }
      }
    }
    _pending.Clear();
  }


  /// <remarks>O(N^2 * <c>_waiting.Count</c>)</remarks>
  [MI(R256)]
  public void UpdatePendingEdges() {
    foreach (var (from, to) in _pending) this.UpdateEdge(from, to, _dist[from, to]);
    _pending.Clear();
  }


  /// <remarks>O(N^2)</remarks>
  public void UpdateEdge(int from, int to, T cost) {
    for (int i = 0; i < this.N; i++) {
      for (int j = 0; j < this.N; j++) {
        _dist[i, j].ChMin(_dist[i, from] + cost + _dist[to, j]);
      }
    }
  }


  readonly record struct Edge(int From, int To) {
    [MI(R256)] public static implicit operator Edge((int From, int To) t) => new(t.From, t.To);
    [MI(R256)] public void Deconstruct(out int from, out int to) { from = this.From; to = this.To; }
    [MI(R256)] public override int GetHashCode() => HashCode.Combine(this.From, this.To);
  }

}
