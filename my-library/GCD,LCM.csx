[MI(256)]
public static int GCD(int x, int y) {
  while (y != 0) (x, y) = (y, x % y); return x;
}
[MI(256)]
public static long GCD(long x, long y) {
  while (y != 0) (x, y) = (y, x % y); return x;
}
[MI(256)] public static int LCM(int x, int y) => x / GCD(x, y) * y;
[MI(256)] public static long LCM(long x, long y) => x / GCD(x, y) * y;
