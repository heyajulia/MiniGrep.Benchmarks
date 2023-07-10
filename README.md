I implemented [Rob Pike's tiny regex matcher][0] in [three][1] [different][2] [ways][3] in C# and compared their
performance to the standard library's [`Regex`][4] class.

```
BenchmarkDotNet=v0.13.5, OS=macOS Ventura 13.4.1 (22F2083) [Darwin 22.5.0]
Apple M2, 1 CPU, 8 logical and 8 physical cores
.NET SDK=7.0.305
  [Host]     : .NET 7.0.8 (7.0.823.31807), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 7.0.8 (7.0.823.31807), Arm64 RyuJIT AdvSIMD
```

| Method    | Regexp               | Text                 |        Mean |     Error |     StdDev | Ratio | RatioSD |   Gen0 |   Gen1 | Allocated | Alloc Ratio |
| --------- | -------------------- | -------------------- | ----------: | --------: | ---------: | ----: | ------: | -----: | -----: | --------: | ----------: |
| Unsafe    | ^a.\*e$              | argyle               |    14.48 ns |  0.025 ns |   0.021 ns |  0.17 |    0.00 |      - |      - |         - |        0.00 |
| Substring | ^a.\*e$              | argyle               |    64.44 ns |  0.492 ns |   0.461 ns |  0.78 |    0.01 | 0.0324 |      - |     272 B |        1.31 |
| Stdlib    | ^a.\*e$              | argyle               |    82.81 ns |  0.199 ns |   0.177 ns |  1.00 |    0.00 | 0.0248 |      - |     208 B |        1.00 |
| Span      | ^a.\*e$              | argyle               |    26.07 ns |  0.456 ns |   0.404 ns |  0.31 |    0.00 |      - |      - |         - |        0.00 |
|           |                      |                      |             |           |            |       |         |        |        |           |             |
| Unsafe    | ^a*.b(...).j*.$ [32] | aaa1b(...)8iii9 [36] |   500.25 ns |  7.827 ns |   7.321 ns |  2.20 |    0.04 |      - |      - |         - |        0.00 |
| Substring | ^a*.b(...).j*.$ [32] | aaa1b(...)8iii9 [36] | 4,960.44 ns | 79.568 ns | 100.628 ns | 21.88 |    0.60 | 4.0207 | 0.0076 |   33640 B |      161.73 |
| Stdlib    | ^a*.b(...).j*.$ [32] | aaa1b(...)8iii9 [36] |   227.92 ns |  2.223 ns |   2.080 ns |  1.00 |    0.00 | 0.0248 |      - |     208 B |        1.00 |
| Span      | ^a*.b(...).j*.$ [32] | aaa1b(...)8iii9 [36] | 1,163.79 ns | 19.941 ns |  18.653 ns |  5.11 |    0.10 |      - |      - |         - |        0.00 |

[0]: https://www.cs.princeton.edu/courses/archive/spr09/cos333/beautiful.html
[1]: MiniGrep.Benchmarks/Implementations/Unsafe.cs
[2]: MiniGrep.Benchmarks/Implementations/Substring.cs
[3]: MiniGrep.Benchmarks/Implementations/Span.cs
[4]: MiniGrep.Benchmarks/Implementations/Stdlib.cs
