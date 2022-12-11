// using System.Runtime.InteropServices;
// using System.Runtime.CompilerServices;

static class List3D {
  [MethodImpl(256)]
  public static Span<T> AsSpan<T>(T[,,] arr)
  => MemoryMarshal.CreateSpan(ref arr[0, 0, 0], arr.Length);

  [MethodImpl(256)]
  public static void Fill<T>(T[,,] arr, T value)
  => AsSpan(arr).Fill(value);

  [MethodImpl(256)]
  public static void Fill<T>(
    IList<IList<IList<T>>> list, int s1, int s2, int s3,
    T value) {
    for (int i = 0; i < s1; ++i)
      for (int j = 0; j < s2; ++j)
        for (int k = 0; k < s3; ++k) list[i][j][k] = value;
  }

  [MethodImpl(256)]
  public static void Init<T>(
    T[,,] array, int s1, int s2, int s3,
    Func<int, int, int, T> generator) {
    for (int i = 0; i < s1; ++i)
      for (int j = 0; j < s2; ++j)
        for (int k = 0; k < s3; ++k)
          array[i, j, k] = generator(i, j, k);
  }

  [MethodImpl(256)]
  public static void Init<T>(
    IList<IList<IList<T>>> list, int s1, int s2, int s3,
    Func<int, int, int, T> generator) {
    for (int i = 0; i < s1; ++i)
      for (int j = 0; j < s2; ++j)
        for (int k = 0; k < s3; ++k)
          list[i][j][k] = generator(i, j, k);
  }
}
