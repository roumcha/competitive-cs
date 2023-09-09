public record struct P(int X, int Y) : IEquatable<P>
{
  [MI(256)] public static implicit operator P((int X, int Y) t) => new(t.X, t.Y);
  [MI(256)] public static P operator +(P a, P b) => new(a.X + b.X, a.Y + b.Y);
  [MI(256)] public static P operator -(P a, P b) => new(a.X - b.X, a.Y - b.Y);
  [MI(256)] public static P operator *(P a, int b) => new(a.X * b, a.Y * b);
  [MI(256)] public static P operator /(P a, int b) => new(a.X / b, a.Y / b);
  [MI(256)] public bool In_Closed(int min_x, int max_x, int min_y, int max_y) => min_x <= X && X <= max_x && min_y <= Y && Y <= max_y;
  [MI(256)] public bool In_Closed(P a, P b) => Min(a.X, b.X) <= X && X <= Max(a.X, b.X) && Min(a.Y, b.Y) <= Y && Y <= Max(a.Y, b.Y);
  [MI(256)] public readonly double DistE(P p) { double dx = (double)X - p.X, dy = (double)Y - p.Y; return Math.Sqrt(dx * dx + dy * dy); }
  [MI(256)] public readonly long DistE2(P p) { long dx = (long)X - p.X, dy = (long)Y - p.Y; return dx * dx + dy * dy; }
  [MI(256)] public readonly long DistM(P p) => Math.Abs((long)X - p.X) + Math.Abs((long)Y - p.Y);
  [MI(256)] public override readonly string ToString() => $"{X} {Y}";
  [MI(256)] public override readonly int GetHashCode() => base.GetHashCode();
}
