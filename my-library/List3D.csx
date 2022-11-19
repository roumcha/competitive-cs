// using System.Runtime.CompilerServices;

static class List3D {
  [MethodImpl(256)]
  public static void Fill<T>(
    T[,,] array, int size1, int size2, int size3, T value) {
    for (int i = 0; i < size1; ++i)
      for (int j = 0; j < size2; ++j)
        for (int k = 0; k < size3; ++k) array[i, j, k] = value;
  }

  [MethodImpl(256)]
  public static void Fill<T>(
    T[,,] array, int size1, int size2, int size3,
    Func<int, int, int, T> generator) {
    for (int i = 0; i < size1; ++i)
      for (int j = 0; j < size2; ++j)
        for (int k = 0; k < size3; ++k)
          array[i, j, k] = generator(i, j, k);
  }

  [MethodImpl(256)]
  public static void Fill<T>(
    IList<IList<IList<T>>> list, int size1, int size2, int size3,
    T value) {
    for (int i = 0; i < size1; ++i)
      for (int j = 0; j < size2; ++j)
        for (int k = 0; k < size3; ++k) list[i][j][k] = value;
  }

  [MethodImpl(256)]
  public static void Fill<T>(
    IList<IList<IList<T>>> list, int size1, int size2, int size3,
    Func<int, int, int, T> generator) {
    for (int i = 0; i < size1; ++i)
      for (int j = 0; j < size2; ++j)
        for (int k = 0; k < size3; ++k)
          list[i][j][k] = generator(i, j, k);
  }
}
