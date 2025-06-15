public static partial class Mylib {
  public static T[,] ToTransposed<T>(this T[,] src) {
    var res = new T[src.GetLength(1), src.GetLength(0)];
    for (int i = 0; i < src.GetLength(1); i++)
      for (int j = 0; j < src.GetLength(0); j++) res[i, j] = src[j, i]; return res;
  }

  public static T[,] ToTransposed<T>(this List<List<T>> src) {
    var res = new T[src[0].Count, src.Count];
    for (int i = 0; i < src[0].Count; i++)
      for (int j = 0; j < src.Count; j++) res[i, j] = src[j][i]; return res;
  }

  public static void RotateLeft90<T>(this T[,] src) {
    int n = src.GetLength(0), m = src.GetLength(1);
    if (n != m) throw new ArgumentException($"width ({n}) must be the same as height ({m})");
    for (int i = 0; i < n / 2; i++) {
      for (int j = i; j < n - i - 1; j++) {
        T t = src[i, j];
        src[i, j] = src[j, n - 1 - i];
        src[j, n - 1 - i] = src[n - 1 - i, n - 1 - j];
        src[n - 1 - i, n - 1 - j] = src[n - 1 - j, i];
        src[n - 1 - j, i] = t;
      }
    }
  }

  public static void RotateRight90<T>(this T[,] src) {
    int n = src.GetLength(0), m = src.GetLength(1);
    if (n != m) throw new ArgumentException($"width ({n}) must be the same as height ({m})");
    for (int i = 0; i < n / 2; i++) {
      for (int j = i; j < n - i - 1; j++) {
        T t = src[i, j];
        src[i, j] = src[n - 1 - j, i];
        src[n - 1 - j, i] = src[n - 1 - i, n - 1 - j];
        src[n - 1 - i, n - 1 - j] = src[j, n - 1 - i];
        src[j, n - 1 - i] = t;
      }
    }
  }

  public static T[,] ToRotatedLeft90<T>(this T[,] src) {
    int len0 = src.GetLength(0), len1 = src.GetLength(1);
    var res = new T[len1, len0];
    for (int j = 0; j < len0; j++)
      for (int i = 0; i < len1; i++) res[i, j] = src[j, len1 - 1 - i];
    return res;
  }

  public static T[,] ToRotatedRight90<T>(this T[,] src) {
    int len0 = src.GetLength(0), len1 = src.GetLength(1);
    var res = new T[len1, len0];
    for (int i = 0; i < len0; i++)
      for (int j = 0; j < len1; j++) res[j, len0 - 1 - i] = src[i, j];
    return res;
  }

  public static void Scan2DInPlace<T>(this T[,] array, Func<T, T, T> func) {
    int h = array.GetLength(0), w = array.GetLength(1);
    for (int i = 0; i < h; i++) {
      for (int j = 0; j < w - 1; j++) {
        array[i, j + 1] = func(array[i, j], array[i, j + 1]);
      }
    }
    for (int i = 0; i < h - 1; i++) {
      for (int j = 0; j < w; j++) {
        array[i + 1, j] = func(array[i, j], array[i + 1, j]);
      }
    }
  }
}
