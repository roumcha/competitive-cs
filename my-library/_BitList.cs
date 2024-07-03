// せっかくやし .NET 8 の Vector512 が来てから作るか。

public class BitSet
: IEnumerable<bool>, IList<bool>,
  IEqualityComparer<BitSet>, IEquatable<BitSet>,
  IEqualityOperators<BitSet, BitSet, bool> {
  private int _count;
  private ulong[] _ints;

  public int Count { [MI(R256)] get => _count; }
  public int Capacity { [MI(R256)] get => _ints.Length * sizeof(ulong); }
  public bool IsReadOnly { [MI(R256)] get => false; }

  public bool this[int index] {
    [MI(R256)]
    get => throw new NotImplementedException();
    [MI(R256)]
    set => throw new NotImplementedException();
  }

  [MI(R256)]
  public void And(BitSet other) {
    throw new NotImplementedException();
  }

  [MI(R256)]
  public void Or(BitSet other) {
    throw new NotImplementedException();
  }

  [MI(R256)]
  public void Xor(BitSet other) {
    throw new NotImplementedException();
  }

  [MI(R256)] public void Not() { for (int i = 0; i < _ints.Length; i++) _ints[i] = ~_ints[i]; }

  [MI(R256)]
  public void LeftShift(int count) {
    throw new NotImplementedException();
  }

  [MI(R256)]
  public void RightShift(int count) {
    throw new NotImplementedException();
  }

  [MI(R256)]
  public int PopCount() {
    int res = 0;
    foreach (var x in _ints) res += BitOperations.PopCount(x);
    return res;
  }

  [MI(R256)]
  public void Fill(bool value) {
    throw new NotImplementedException();
  }

  [MI(R256)] public void Add(bool item) { this.EnsureCapacity(_count + 1); this[_count++] = item; }

  [MI(R256)]
  public void AddRange(nuint set) {
    _count += sizeof(ulong);
    throw new NotImplementedException();
  }

  [MI(R256)] public void AddRange(IEnumerable<nuint> set) { foreach (var x in set) this.AddRange(x); }

  [MI(R256)]
  public void Clear() {
    throw new NotImplementedException();
  }

  [MI(R256)]
  public bool Contains(bool item) {
    throw new NotImplementedException();
  }

  [MI(R256)]
  public void CopyTo(bool[] array, int arrayIndex) {
    throw new NotImplementedException();
  }

  [MI(R256)]
  public int IndexOf(bool item) {
    throw new NotImplementedException();
  }

  [MI(R256)]
  public void Insert(int index, bool item) {
    throw new NotImplementedException();
  }

  [MI(R256)]
  public bool Remove(bool item) {
    throw new NotImplementedException();
  }

  [MI(R256)]
  public void RemoveAt(int index) {
    throw new NotImplementedException();
  }

  [MI(R256)] public bool PopLast() => this[--_count];

  [MI(R256)]
  public int EnsureCapacity(int count) {
    throw new NotImplementedException();
  }

  [MI(R256)]
  public void TrimExcess() {
    throw new NotImplementedException();
  }

  [MI(R256)]
  public IEnumerator<bool> GetEnumerator() {
    throw new NotImplementedException();
  }

  [MI(R256)] IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

  [MI(R256)]
  public bool Equals(BitSet other) {
    throw new NotImplementedException();
  }

  [MI(R256)] public bool Equals(BitSet x, BitSet y) => x.Equals(y);
  [MI(R256)] public override bool Equals(object obj) => this.Equals(obj as BitSet);
  [MI(R256)] public static bool operator ==(BitSet left, BitSet right) => left.Equals(right);
  [MI(R256)] public static bool operator !=(BitSet left, BitSet right) => !left.Equals(right);

  [MI(R256)]
  public override int GetHashCode() {
    throw new NotImplementedException();
  }

  [MI(R256)] public int GetHashCode(BitSet obj) => obj.GetHashCode();

  [MI(R256)]
  public override string ToString() {
    throw new NotImplementedException();
  }
}

public static class BitListExt {
  [MI(R256)]
  public static BitSet ToBitList(this BitArray bitArray) {
    throw new NotImplementedException();
  }

  [MI(R256)]
  public static BitArray ToBitArray(this BitSet bitArray) {
    throw new NotImplementedException();
  }
}
