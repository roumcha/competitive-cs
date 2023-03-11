static class BinarySearch {
  /// <remarks>O(log N)</remarks>
  [MI(256)]
  public static long Search(
    long ok, long ng, Func<long, bool> condition
  ) {
    while (Math.Abs(ng - ok) > 1) {
      var m = (ok + ng) >> 1; if (condition(m)) ok = m; else ng = m;
    }
    return ok;
  }
  /// <remarks>O(log N)</remarks>
  [MI(256)]
  public static long Search(
    long ok, long ng, Func<long, long, long, bool> condition
  ) {
    while (Math.Abs(ng - ok) > 1) {
      var m = (ok + ng) >> 1;
      if (condition(ok, m, ng)) ok = m; else ng = m;
    }
    return ok;
  }
  /// <remarks>O(log N)</remarks>
  [MI(256)]
  public static double Search(
    double ok, double ng, double prec, Func<double, bool> condition
  ) {
    while (Math.Abs(ng - ok) > prec) {
      var m = (ok + ng) / 2; if (condition(m)) ok = m; else ng = m;
    }
    return ok;
  }
  /// <remarks>O(log N)</remarks>
  [MI(256)]
  public static double Search(
    double ok, double ng, double prec,
    Func<double, double, double, bool> condition
  ) {
    while (Math.Abs(ng - ok) > prec) {
      var m = (ok + ng) / 2;
      if (condition(ok, m, ng)) ok = m; else ng = m;
    }
    return ok;
  }
}

class LowerBound<T> : IComparer<T> where T : IComparable<T> {
  [MI(256)]
  public int Compare(T x, T y) { return 0 <= x.CompareTo(y) ? 1 : -1; }
}
class UpperBound<T> : IComparer<T> where T : IComparable<T> {
  [MI(256)]
  public int Compare(T x, T y) { return 0 < x.CompareTo(y) ? 1 : -1; }
}
