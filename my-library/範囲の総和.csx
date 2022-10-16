// using System.Runtime.CompilerServices;

[MethodImpl(256)]
public static int RangeSum(int min, int max)
=> (max - min + 1) * (min + max) / 2;
[MethodImpl(256)]
public static long RangeSum(long min, long max)
=> (max - min + 1) * (min + max) / 2;
[MethodImpl(256)]
public static float RangeSum(float min, float max)
=> (max - min + 1) * (min + max) / 2;
[MethodImpl(256)]
public static double RangeSum(double min, double max)
=> (max - min + 1) * (min + max) / 2;
