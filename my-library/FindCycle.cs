static class Cycle {
  [MI(R256)]
  public static List<int> FindCycle(int n, List<int>[] nexts, int start) {
    var res = new List<int>();
    FindCycle_Inner(start, new int[n], nexts, res);
    res.Reverse();
    return res;
  }

  [MI(R256)]
  static bool FindCycle_Inner(int now, int[] seen, List<int>[] nexts, List<int> res) {
    if (++seen[now] == 2) return true;
    foreach (var next in nexts[now]) {
      if (FindCycle_Inner(next, seen, nexts, res)) {
        res.Add(now);
        return seen[now] == 1;
      }
    }
    return false;
  }
}
