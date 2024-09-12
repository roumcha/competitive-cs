public static partial class Mylib {
  [MI(R256)]
  public static T[,] Transpose<T>(this T[,] src) {
    var res = new T[src.GetLength(1), src.GetLength(0)];
    for (int i = 0; i < src.GetLength(1); i++)
      for (int j = 0; j < src.GetLength(0); j++) res[i, j] = src[j, i]; return res;
  }

  [MI(R256)]
  public static T[,] Transpose<T>(this List<List<T>> src) {
    var res = new T[src[0].Count, src.Count];
    for (int i = 0; i < src[0].Count; i++)
      for (int j = 0; j < src.Count; j++) res[i, j] = src[j][i]; return res;
  }

  [MI(R256)]
  public static T[,] RotateLeft90<T>(this T[,] src) {
    int len0 = src.GetLength(0), len1 = src.GetLength(1);
    var res = new T[len1, len0];
    for (int j = 0; j < len0; j++)
      for (int i = 0; i < len1; i++) res[i, j] = src[j, len1 - 1 - i];
    return res;
  }

  [MI(R256)]
  public static T[,] RotateRight90<T>(this T[,] src) {
    int len0 = src.GetLength(0), len1 = src.GetLength(1);
    var res = new T[len1, len0];
    for (int i = 0; i < len0; i++)
      for (int j = 0; j < len1; j++) res[j, len0 - 1 - i] = src[i, j];
    return res;
  }
}
