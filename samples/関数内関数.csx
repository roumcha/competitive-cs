using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
  static void Main()
  {
    // https://ufcpp.net/study/csharp/functional/fun_localfunctions/#local-function
    // ローカル関数は書ける場所が限られるものの、機能的には通常のメソッドと同程度に何でも書ける。逆に、匿名関数はどこにでも書ける代わりに、いくつか機能的に制限がある
    Func<int, int> add2 = x => { return x + 1; };

    int Add(int x)
    {
      return x + 1;
    }

    System.Console.WriteLine(Add(1));
    System.Console.WriteLine(add2(1));
  }
}