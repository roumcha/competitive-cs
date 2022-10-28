// using System.Runtime.CompilerServices;

[MethodImpl(256)]
public static void Swap<T>(ref T a, ref T b) {
  T t = a; a = b; b = t;
}
