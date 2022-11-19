// using System.Runtime.CompilerServices;

// TODO: next_permutation, prev_permutation

[MethodImpl(256)]
static long Combination(int n, int r) {
  if (r < 0 || n < r) return 0;
  long x = 1;
  for (int i = 0; i < r; i++) x *= n - i;
  for (int i = 1; i <= r; i++) x /= i;
  return x;
}

[MethodImpl(256)]
static long Permutation(int n, int r) {
  if (r < 0 || n < r) return 0;
  long x = 1; while (n > r) x *= n--; return x;
}
