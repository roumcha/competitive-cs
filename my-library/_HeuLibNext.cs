// namespace HeuLibNext;


// public readonly record struct Input(

// ) {

//   public static Input FromCIn(CIn @in) {
//     return new();
//   }

// };


// public static class Annealing {

//   public interface IState<TSelf, TAConsts>
//   where TSelf : IState<TSelf, TAConsts> {
//     public double Modify(in TAConsts consts);
//     public double RollBack(in TAConsts consts);
//     public double GetScore(in TAConsts consts);
//     public TSelf DeepCopy(in TAConsts consts);
//   }

//   /// <summary>焼きなましを行う。</summary>
//   /// <remarks>軽量バージョン: 途中の最善状態を記録しない</remarks>
//   public static void Run<TState, TConsts>(
//     ref TState state,
//     in TConsts consts,
//     bool maxOrMin,
//     long endTime,
//     int startTemp,
//     int endTemp
//   )
//   where TState : IState<TState, TConsts> {
//     long startTime = GetTime();

//     int i, time;
//     double score = state.GetScore(consts);
//     for (i = 0; ; i++) {
//       // 16 回ごとに時間を確認・更新
//       if ((i & 0b1111) == 0 && (time = GetTime()) > endTime) break;

//       // 遷移
//       double nextScore = state.Modify(consts);
//       double improvedAmt = nextScore - score;

//       if (improvedAmt > 0 || IsWorseningCommittable(improvedAmt, time)) {
//         if (!ATCODER) cerr.Log($"improved by {improvedAmt}, applied.");
//         score = nextScore;
//       } else {
//         if (!ATCODER) cerr.Log($"improved by {improvedAmt}, rejected.");
//         state.RollBack(consts);
//       }
//     }
//   }

//   /// <summary>改善量と温度から、悪化を確定するかを決める</summary>
//   /// <param name="improved_amt">改善量（負の値）</param>
//   /// <remarks>悪化時用。改善したなら即採用すればよい。</remarks>
//   [MI(R256)]
//   readonly bool IsWorseningCommittable(
//     double improved_amt,
//     long startTime,
//     long endTime,
//     int currTime) {
//     double timeRatio = (currTime - startTime) / (double)(endTime - startTime);

//     double temp =
//       this.temp_from + (this.temp_to - this.temp_from) * timeRatio;

//     double prob = Exp(improved_amt / temp);
//     double rand = Rng.NextDouble();

//     if (!ATCODER) cerr.Log($"IsWorseningCommittable? improved={improved_amt:F1}, temp={temp:F1}, exp(amt/temp)={prob}, rand={rand:F1} => {rand < prob}");

//     return rand < prob;
//   }

// }


// public readonly struct AConsts {
//   // TODO: 焼きなまし中に変化しないデータはここに置く

//   public AConsts() { }
// }


// public struct AState : Annealing.IState<AState, AConsts> {
//   // TODO: 焼きなまし中に変化するデータはここに置く

//   public AState() { }

//   /// <summary>状態を変更する</summary>
//   public double Modify(in AConsts consts) {
//     throw new NotImplementedException();
//   }

//   /// <summary>変更前の状態に戻る</summary>
//   public double RollBack(in AConsts consts) {
//     throw new NotImplementedException();
//   }

//   /// <summary>スコアを取得</summary>
//   public double GetScore(in AConsts consts) {
//     throw new NotImplementedException();
//   }

//   /// <summary>現在の状態を複製する</summary>
//   public readonly AState DeepCopy(in AConsts consts) {
//     // TODO: 参照型が含まれる場合は複製処理を書く
//     return this with { };
//   }

// }
