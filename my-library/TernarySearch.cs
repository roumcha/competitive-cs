public static partial class Mylib {
  /// <param name="cmp">比較関数; 下に凸なら <c>f(a) - f(b)</c></param>
  /// <remarks>
  ///   <para>狭義凸関数でないと、傾きが 0 になる位置の一つを返す可能性がある。</para>
  ///   <para>桁あふれに対して 3 倍程度の余裕が必要。</para>
  /// </remarks>
  public static T TernarySearch<T>(T l, T r, Comparison<T> cmp)
  where T : IBinaryInteger<T>, ISignedNumber<T> {
    T t2 = T.One + T.One, t3 = t2 + T.One;
    while (r - l > t2) {
      T m1 = (l + l + r) / t3;
      T m2 = (l + r + r) / t3;
      if (cmp.Invoke(m1, m2) < 0) r = m2;
      else l = m1;
    }
    T res = l;
    for (T i = l + T.One; i <= r; i++) {
      if (cmp.Invoke(l, i) > 0) res = i;
    }
    return res;
  }

  /// <param name="cmp">比較関数; 下に凸なら <c>f(a) - f(b)</c></param>
  /// <remarks>
  ///   <para>狭義凸関数でないと、傾きが 0 になる位置の一つを返す可能性がある。</para>
  ///   <para>桁あふれに対して 3 倍程度の余裕が必要。</para>
  /// </remarks>
  public static T TernarySearch<T>(T l, T r, T precision, Comparison<T> cmp)
  where T : INumber<T>, ISignedNumber<T> {
    T t2 = T.One + T.One, t3 = t2 + T.One;
    while (r - l > precision) {
      T m1 = (l + l + r) / t3;
      T m2 = (l + r + r) / t3;
      if (cmp.Invoke(m1, m2) < 0) r = m2;
      else l = m1;
    }
    return (l + r) / t2;
  }
}
