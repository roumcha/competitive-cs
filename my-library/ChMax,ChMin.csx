[MI(256)]
public static bool ChMax<T>(ref T a, T b) where T : IComparable { if (a.CompareTo(b) < 0) { a = b; return true; } return false; }
[MI(256)]
public static bool ChMin<T>(ref T a, T b) where T : IComparable { if (a.CompareTo(b) > 0) { a = b; return true; } return false; }
