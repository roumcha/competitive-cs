public static class _Iota {
  public static void Iota<T>(Span<T> span, T start)
    where T : unmanaged, IBinaryNumber<T>
      => Iota(span, start, T.One);

  public static unsafe void Iota<T>(Span<T> span, T start, T step)
  where T : unmanaged, IBinaryNumber<T> {
    const int bytes = 32;
    int tsize = sizeof(T),
      vecCount = Vector256<T>.Count,
      pre_len = span.Length % vecCount;

    start = IotaNoOpt(span[..pre_len], start, step);
    span = span[pre_len..];
    if (span.Length == 0) return;

    Span<T> ag = stackalloc T[vecCount];
    T next = IotaNoOpt(ag, start, step);
    var agv = Vector256.Create<T>(ag);
    var adv = Vector256.Create(next - start);

    ref var ptr = ref MemoryMarshal.GetReference(span);
    ref var end = ref Unsafe.AddByteOffset(ref ptr, (nint)tsize * span.Length);
    agv.StoreUnsafe(ref ptr);
    ptr = ref Unsafe.AddByteOffset(ref ptr, bytes);

    while (Unsafe.IsAddressLessThan(ref ptr, ref end)) {
      (agv += adv).StoreUnsafe(ref ptr);
      ptr = ref Unsafe.AddByteOffset(ref ptr, bytes);
    }
  }

  static T IotaNoOpt<T>(Span<T> span, T start, T step)
  where T : unmanaged, IBinaryNumber<T> {
    for (int i = 0; i < span.Length; i++) {
      span[i] = start;
      start += step;
    }
    return start;
  }
}
