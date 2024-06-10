public static class Combination {
  public static T[,] PascalsTriangle<T>(int maxIncl) where T : INumberBase<T> {
    var res = new T[maxIncl + 1, maxIncl + 1];
    res[0, 0] = T.One;
    for (int i = 0; i <= maxIncl; i++) {
      for (int j = 0; j <= i; j++) {
        if (j == 0 || j == i) res[i, j] = T.One;
        else res[i, j] = res[i - 1, j - 1] + res[i - 1, j];
      }
    }
    return res;
  }
}
