static class List2D {
  [MI(256)]
  public static Span<T> AsSpan<T>(T[,] arr)
  => MemoryMarshal.CreateSpan(ref arr[0, 0], arr.Length);

  [MI(256)]
  public static void Fill<T>(T[,] arr, T value)
  => AsSpan(arr).Fill(value);

  [MI(256)]
  public static void Fill<T>(
    IList<IList<T>> list, int s1, int s2, T value
  ) {
    for (int i = 0; i < s1; ++i)
      for (int j = 0; j < s2; ++j) list[i][j] = value;
  }

  [MI(256)]
  public static void Init<T>(T[,] arr, Func<int, int, T> generator) {
    int s1 = arr.GetLength(0), s2 = arr.GetLength(1);
    for (int i = 0; i < s1; ++i)
      for (int j = 0; j < s2; ++j) arr[i, j] = generator(i, j);
  }

  [MI(256)]
  public static void Init<T>(
    IList<IList<T>> list, int s1, int s2,
    Func<int, int, T> generator
  ) {
    for (int i = 0; i < s1; ++i)
      for (int j = 0; j < s2; ++j) list[i][j] = generator(i, j);
  }
}
