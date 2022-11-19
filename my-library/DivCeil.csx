// using System.Runtime.CompilerServices;

[MethodImpl(256)]
public static int DivCeil(int divided, int divisor)
=> (int)(((long)divided + divisor - 1) / divisor);
[MethodImpl(256)]
public static long DivCeil(long divided, long divisor)
=> (divided + divisor - 1) / divisor;
[MethodImpl(256)]
public static float DivCeil(float divided, float divisor)
=> (divided + divisor - 1) / divisor;
[MethodImpl(256)]
public static double DivCeil(double divided, double divisor)
=> (divided + divisor - 1) / divisor;
