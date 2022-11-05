// using System.Runtime.CompilerServices;

// TODO: 精度を決めて小数で探索

static class BinarySearch {
  /// <remarks>O(log N)</remarks>
  [MethodImpl(256)]
  public static int Search(int ok, int ng, Func<int, bool> condition) {
    while (Math.Abs(ng - ok) > 1) {
      var mid = (ok + ng) / 2;
      if (condition(mid)) ok = mid; else ng = mid;
    }
    return ok;
  }

  /// <remarks>O(log N)</remarks>
  [MethodImpl(256)]
  public static int Search(
    int ok, int ng, Func<int, int, int, bool> condition) {
    while (Math.Abs(ng - ok) > 1) {
      var mid = (ok + ng) / 2;
      if (condition(ok, mid, ng)) ok = mid; else ng = mid;
    }
    return ok;
  }

  /// <remarks>
  /// ソートされた配列に対する、新要素の挿入位置(のうち最も左)を探索 -
  /// O(log N)
  /// </remarks>
  [MethodImpl(256)]
  public static int LowerBound<T>(T item, ref T[] ary) {
    var r = Array.BinarySearch(ary, item); return r >= 0 ? r : ~r;
  }

  /// <remarks>
  /// ソートされたリストに対する、新要素の挿入位置(のうち最も左)を探索 -
  /// O(log N)
  /// </remarks>
  [MethodImpl(256)]
  public static int LowerBound<T>(T item, ref List<T> list) {
    var r = list.BinarySearch(item); return r >= 0 ? r : ~r;
  }
}
