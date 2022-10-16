// using System.Runtime.CompilerServices;

[MethodImpl(256)]
public static long PowN(long x, int n)
{
  long p = 1;
  while (n > 0) { if ((n & 1) == 1) p *= x; x *= x; n >>= 1; }
  return p;
}

[MethodImpl(256)]
public static double PowN(double x, int n)
{
  double p = 1;
  while (n > 0) { if ((n & 1) == 1) p *= x; x *= x; n >>= 1; }
  return p;
}
