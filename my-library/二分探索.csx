// using System.Runtime.CompilerServices;

static class BinarySearch
{
  /// <remarks>O(log N)</remarks>
  [MethodImpl(256)]
  public static int Search(int ok, int ng, Func<int, bool> condition)
  {
    while (Math.Abs(ng - ok) > 1)
    {
      var mid = (ok + ng) / 2;
      if (condition(mid)) ok = mid; else ng = mid;
    }
    return ok;
  }

  /// <remarks>O(log N)</remarks>
  [MethodImpl(256)]
  public static int Search(
    int ok, int ng, Func<int, int, int, bool> condition)
  {
    while (Math.Abs(ng - ok) > 1)
    {
      var mid = (ok + ng) / 2;
      if (condition(ok, mid, ng)) ok = mid; else ng = mid;
    }
    return ok;
  }

  /// <remarks>
  /// ソートされた配列に対する、新要素の挿入位置(のうち最も左)を探索;
  /// O(log N)
  /// </remarks>
  [MethodImpl(256)]
  public static int IndexLeft<T>(ref T item, ref T[] ary)
  => System.Array.BinarySearch(ary, item) switch
  {
    var res when res >= 0 => res,
    var res => ~res
  };

  /// <remarks>
  /// ソートされたリストに対する、新要素の挿入位置(のうち最も左)を探索;
  /// O(log N)
  /// </remarks>
  [MethodImpl(256)]
  public static int IndexLeft<T>(ref T item, ref List<T> list)
  => list.BinarySearch(item) switch
  {
    var res when res >= 0 => res,
    var res => ~res
  };
}