// using System.Runtime.CompilerServices;

struct P
{
  public int X, Y;
  [MethodImpl(256)] public P(int x, int y) { X = x; Y = y; }

  [MethodImpl(256)]
  public double Dist(P p)
  {
    double dx = (double)this.X - p.X, dy = (double)this.Y - p.Y;
    return Math.Sqrt(dx * dx + dy * dy);
  }

  [MethodImpl(256)]
  public long DistM(P p)
  => Math.Abs((long)this.X - p.X) + Math.Abs((long)this.Y - p.Y);
}
