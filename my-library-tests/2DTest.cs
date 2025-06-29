namespace MyLibrary.Tests;

using System.Collections.ObjectModel;
using template;
using static template.MyLib;

public class _2DTest(ITestOutputHelper _output) {

  [Fact]
  public void ToTransposed_2D_Valid() {
    var init = new[,] { { 0, 1, 2, 3 }, { 4, 5, 6, 7 }, { 8, 9, 10, 11 } };
    var src = new[,] { { 0, 1, 2, 3 }, { 4, 5, 6, 7 }, { 8, 9, 10, 11 } };

    var actual = src.ToTransposed();
    var expected = new[,] { { 0, 4, 8 }, { 1, 5, 9 }, { 2, 6, 10 }, { 3, 7, 11 } };

    for (int i = 0; i < actual.GetLength(0); i++) {
      for (int j = 0; j < actual.GetLength(1); j++) {
        actual[i, j].ShouldBe(expected[i, j]);
      }
    }

    // src shouldn't be changed
    for (int i = 0; i < src.GetLength(0); i++) {
      for (int j = 0; j < src.GetLength(1); j++) {
        src[i, j].ShouldBe(init[i, j]);
      }
    }
  }

  [Fact]
  public void ToTransposed_Jagged_Valid() {
    var init = new List<List<int>>() { new() { 0, 1, 2, 3 }, new() { 4, 5, 6, 7 }, new() { 8, 9, 10, 11 } };
    var src = new List<List<int>>() { new() { 0, 1, 2, 3 }, new() { 4, 5, 6, 7 }, new() { 8, 9, 10, 11 } };

    var actual = src.ToTransposed();
    var expected = new[,] { { 0, 4, 8 }, { 1, 5, 9 }, { 2, 6, 10 }, { 3, 7, 11 } };

    for (int i = 0; i < actual.GetLength(0); i++) {
      for (int j = 0; j < actual.GetLength(1); j++) {
        actual[i, j].ShouldBe(expected[i, j]);
      }
    }

    // src shouldn't be changed
    for (int i = 0; i < src.Count; i++) {
      for (int j = 0; j < src[i].Count; j++) {
        src[i][j].ShouldBe(init[i][j]);
      }
    }
  }

  [Fact]
  public void RotateLeft90_Valid() {
    var src = new[,] {
      { 0, 1, 2 },
      { 4, 5, 6 },
      { 8, 9, 10 }
    };
    var expected = new[,] {
      { 2, 6, 10 },
      { 1, 5, 9 },
      { 0, 4, 8 }
    };

    src.RotateLeft90();

    for (int i = 0; i < src.GetLength(0); i++) {
      for (int j = 0; j < src.GetLength(1); j++) {
        src[i, j].ShouldBe(expected[i, j]);
      }
    }
  }

  [Fact]
  public void RotateRight90_Valid() {
    var src = new[,] {
      { 0, 1, 2 },
      { 4, 5, 6 },
      { 8, 9, 10 }
    };
    var expected = new[,] {
      { 8, 4, 0 },
      { 9, 5, 1 },
      { 10, 6, 2 }
    };

    src.RotateRight90();

    for (int i = 0; i < src.GetLength(0); i++) {
      for (int j = 0; j < src.GetLength(1); j++) {
        src[i, j].ShouldBe(expected[i, j]);
      }
    }
  }

  [Fact]
  public void ToRotatedLeft90_Valid() {
    var init = new[,] { { 0, 1, 2, 3 }, { 4, 5, 6, 7 }, { 8, 9, 10, 11 } };
    var src = new[,] { { 0, 1, 2, 3 }, { 4, 5, 6, 7 }, { 8, 9, 10, 11 } };

    var actual = src.ToRotatedLeft90();
    var expected = new[,] { { 3, 7, 11 }, { 2, 6, 10 }, { 1, 5, 9 }, { 0, 4, 8 } };

    for (int i = 0; i < actual.GetLength(0); i++) {
      for (int j = 0; j < actual.GetLength(1); j++) {
        actual[i, j].ShouldBe(expected[i, j]);
      }
    }

    // src shouldn't be changed
    for (int i = 0; i < src.GetLength(0); i++) {
      for (int j = 0; j < src.GetLength(1); j++) {
        src[i, j].ShouldBe(init[i, j]);
      }
    }
  }

  [Fact]
  public void ToRotatedRight90_Valid() {
    var init = new[,] { { 0, 1, 2, 3 }, { 4, 5, 6, 7 }, { 8, 9, 10, 11 } };
    var src = new[,] { { 0, 1, 2, 3 }, { 4, 5, 6, 7 }, { 8, 9, 10, 11 } };

    var actual = src.ToRotatedRight90();
    var expected = new[,] { { 8, 4, 0 }, { 9, 5, 1 }, { 10, 6, 2 }, { 11, 7, 3 } };

    for (int i = 0; i < actual.GetLength(0); i++) {
      for (int j = 0; j < actual.GetLength(1); j++) {
        actual[i, j].ShouldBe(expected[i, j]);
      }
    }

    // src shouldn't be changed
    for (int i = 0; i < src.GetLength(0); i++) {
      for (int j = 0; j < src.GetLength(1); j++) {
        src[i, j].ShouldBe(init[i, j]);
      }
    }
  }

}
