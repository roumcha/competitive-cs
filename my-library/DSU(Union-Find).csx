// using System.Diagnostics;
// using System.Runtime.CompilerServices;

// keymoon/ac-library-cs から一部変更

class DSU {
  int _count; int[] _parentOrSize;

  [MethodImpl(256)]
  public DSU(int count) {
    _count = count; _parentOrSize = new int[count];
    Array.Fill(_parentOrSize, -1);
  }

  [MethodImpl(256)]
  public int Leader(int a) {
    Debug.Assert(0 <= a && a < _count);
    var ps = _parentOrSize[a];
    return ps < 0 ? a : (_parentOrSize[a] = this.Leader(ps));
  }

  [MethodImpl(256)]
  public int Merge(int a, int b) {
    Debug.Assert((0 <= a && a < _count) || (0 <= b && b < _count));
    int x = this.Leader(a), y = this.Leader(b);
    if (x == y) return x;
    if (-_parentOrSize[x] < -_parentOrSize[y]) Swap(ref x, ref y);
    _parentOrSize[x] += _parentOrSize[y];
    return _parentOrSize[y] = x;
  }

  [MethodImpl(256)]
  public bool Same(int a, int b) {
    Debug.Assert((0 <= a && a < _count) || (0 <= b && b < _count));
    return this.Leader(a) == this.Leader(b);
  }

  [MethodImpl(256)]
  public int Size(int a) {
    Debug.Assert(0 <= a && a < _count);
    return -_parentOrSize[this.Leader(a)];
  }

  public int[][] Groups() {
    int[] leaderBuf = new int[_count], id = new int[_count];
    for (int i = 0; i < _count; i++) leaderBuf[i] = this.Leader(i);
    var res = new int[_count][];
    var groupCnt = 0;
    for (int i = 0; i < _count; i++) {
      if (i != leaderBuf[i]) continue;
      id[i] = groupCnt++;
      res[id[i]] = new int[-_parentOrSize[i]];
    }
    var idx = new int[groupCnt];
    Array.Resize(ref res, groupCnt);
    for (int i = 0; i < _count; i++) {
      var leaderID = id[leaderBuf[i]];
      res[leaderID][idx[leaderID]] = i;
      idx[leaderID]++;
    }
    return res;
  }

  [MethodImpl(256)]
  static void Swap<T>(ref T a, ref T b) {
    T t = a; a = b; b = t;
  }
}
