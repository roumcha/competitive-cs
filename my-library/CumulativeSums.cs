public class CSum<T>
where T : IAdditionOperators<T, T, T>, ISubtractionOperators<T, T, T> {
  readonly List<T> _sums;
  [MI(R256)] public CSum(IEnumerable<T> a) { _sums = new() { default }; foreach (var x in a) _sums.Add(x + _sums[^1]); }
  [MI(R256)] public void Add(T v) => _sums.Add(v + _sums[^1]);
  [MI(R256)] public T Get(int fromIncl, int toExcl) => _sums[toExcl] - _sums[fromIncl];
}


public class CSum2D<T>
where T : IAdditionOperators<T, T, T>, ISubtractionOperators<T, T, T> {
  public readonly int H, W;
  readonly T[,] _sums;

  public CSum2D(T[,] a) {
    (H, W) = (a.GetLength(0), a.GetLength(1));
    _sums = new T[H + 1, W + 1];
    for (int i = 0; i < H; i++) {
      a.AsSpan().Slice(i * W, W)
        .CopyTo(_sums.AsSpan().Slice((i + 1) * (W + 1) + 1, W));
    }
    FirstScan(_sums);
  }

  public CSum2D(IList<T[]> a, int h, int w) {
    (H, W) = (h, w);
    _sums = new T[h + 1, w + 1];
    for (int i = 0; i < H; i++) {
      a[i].CopyTo(_sums.AsSpan().Slice((i + 1) * (W + 1) + 1, W));
    }
    FirstScan(_sums);
  }

  [MI(R512)]
  static void FirstScan(T[,] a) {
    int h = a.GetLength(0), w = a.GetLength(1);
    for (int i = 1; i < h; i++) {
      for (int j = 1; j <= w; j++) a[i + 1, j] += a[i, j];
    }
    for (int i = 1; i <= h; i++) {
      for (int j = 1; j < w; j++) a[i, j + 1] += a[i, j];
    }
  }

  [MI(R256)]
  public T Get((int x, int y) fromIncl, (int x, int y) toExcl)
  => _sums[toExcl.x, toExcl.y]
    - _sums[fromIncl.x, toExcl.y]
    - _sums[toExcl.x, fromIncl.y]
    + _sums[fromIncl.x, fromIncl.y];
}


public class CSum3D<T>
where T : IAdditionOperators<T, T, T>, ISubtractionOperators<T, T, T> {
  public readonly int H, W, D;
  readonly T[,,] _sums;

  public CSum3D(T[,,] a) {
    (H, W, D) = (a.GetLength(0), a.GetLength(1), a.GetLength(2));
    _sums = new T[H + 1, W + 1, D + 1];
    for (int i = 0; i < H; i++) {
      for (int j = 0; j < W; j++) {
        a.AsSpan().Slice(i * W + j * D, D).CopyTo(
          _sums.AsSpan().Slice((i + 1) * (W + 1) + (j + 1) * (D + 1) + 1, D)
        );
      }
    }
    FirstScan(_sums);
  }

  public CSum3D(IList<T[][]> a, int h, int w, int d) {
    (H, W, D) = (h, w, d);
    _sums = new T[h + 1, w + 1, d + 1];
    for (int i = 0; i < H; i++) {
      for (int j = 0; j < W; j++) {
        a[i][j].CopyTo(
          _sums.AsSpan().Slice((i + 1) * (W + 1) + (j + 1) * (D + 1) + 1, D)
        );
      }
    }
    FirstScan(_sums);
  }

  [MI(R512)]
  static void FirstScan(T[,,] a) {
    int h = a.GetLength(0), w = a.GetLength(1), d = a.GetLength(2);

    for (int i = 1; i < h; i++) {
      for (int j = 1; j <= w; j++) {
        for (int k = 1; k <= d; k++) a[i + 1, j, k] = a[i, j, k];
      }
    }
    for (int i = 1; i <= h; i++) {
      for (int j = 1; j < w; j++) {
        for (int k = 1; k <= d; k++) a[i, j + 1, k] = a[i, j, k];
      }
    }
    for (int i = 1; i <= h; i++) {
      for (int j = 1; j <= w; j++) {
        for (int k = 1; k < d; k++) a[i, j, k + 1] = a[i, j, k];
      }
    }
  }

  [MI(R256)]
  public T Get((int x, int y, int z) fromIncl, (int x, int y, int z) toExcl)
  => _sums[toExcl.x, toExcl.y, toExcl.z]
    - _sums[toExcl.x, toExcl.y, fromIncl.z]
    - _sums[toExcl.x, fromIncl.y, toExcl.z]
    - _sums[fromIncl.x, toExcl.y, toExcl.z]
    + _sums[fromIncl.x, fromIncl.y, toExcl.z]
    + _sums[fromIncl.x, toExcl.y, fromIncl.z]
    + _sums[toExcl.x, fromIncl.y, fromIncl.z]
    - _sums[fromIncl.x, fromIncl.y, fromIncl.z];
}
