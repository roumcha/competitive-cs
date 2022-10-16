// using System.Runtime.CompilerServices;

class List2D
{
  [MethodImpl(256)]
  public static void Fill<T>(T[,] array, int size1, int size2, T value)
  {
    for (int i = 0; i < size1; ++i)
      for (int j = 0; j < size2; ++j) array[i, j] = value;
  }

  [MethodImpl(256)]
  public static void Fill<T>(T[,] array, int size1, int size2, Func<int, int, T> generator)
  {
    for (int i = 0; i < size1; ++i)
      for (int j = 0; j < size2; ++j) array[i, j] = generator(i, j);
  }

  [MethodImpl(256)]
  public static void Fill<T>(IList<IList<T>> list, int size1, int size2, T value)
  {
    for (int i = 0; i < size1; ++i)
      for (int j = 0; j < size2; ++j) list[i][j] = value;
  }

  [MethodImpl(256)]
  public static void Fill<T>(IList<IList<T>> list, int size1, int size2, Func<int, int, T> generator)
  {
    for (int i = 0; i < size1; ++i)
      for (int j = 0; j < size2; ++j) list[i][j] = generator(i, j);
  }
}