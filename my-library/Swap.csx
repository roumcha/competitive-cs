// using System.Runtime.CompilerServices;

[MethodImpl(256)]
public static void Swap<T>(ref T a, ref T b) => (a, b) = (b, a);