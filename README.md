I implemented [Rob Pike's tiny regex matcher][0] in [three][1] [different][2] [ways][3] in C# and compared their
performance to the standard library's [`Regex`][4] class.

```
BenchmarkDotNet=v0.13.5, OS=macOS Ventura 13.4.1 (22F2083) [Darwin 22.5.0]
Apple M2, 1 CPU, 8 logical and 8 physical cores
.NET SDK=7.0.305
  [Host]     : .NET 7.0.8 (7.0.823.31807), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 7.0.8 (7.0.823.31807), Arm64 RyuJIT AdvSIMD
```

| Method    | Parameters            |        Mean |    Error |   StdDev | Ratio | RatioSD |   Gen0 |   Gen1 | Allocated | Alloc Ratio |
| --------- | --------------------- | ----------: | -------: | -------: | ----: | ------: | -----: | -----: | --------: | ----------: |
| Unsafe    | (^a.\*e$, argyle)     |    14.44 ns | 0.012 ns | 0.010 ns |  0.18 |    0.00 |      - |      - |         - |        0.00 |
| Substring | (^a.\*e$, argyle)     |    63.84 ns | 0.064 ns | 0.057 ns |  0.79 |    0.00 | 0.0324 |      - |     272 B |        1.31 |
| Stdlib    | (^a.\*e$, argyle)     |    80.37 ns | 0.067 ns | 0.056 ns |  1.00 |    0.00 | 0.0248 |      - |     208 B |        1.00 |
| Span      | (^a.\*e$, argyle)     |    25.27 ns | 0.100 ns | 0.093 ns |  0.31 |    0.00 |      - |      - |         - |        0.00 |
|           |                       |             |          |          |       |         |        |        |           |             |
| Unsafe    | (^a\*.(...)iii9) [72] |   493.59 ns | 2.893 ns | 2.706 ns |  2.20 |    0.01 |      - |      - |         - |        0.00 |
| Substring | (^a\*.(...)iii9) [72] | 4,837.20 ns | 7.284 ns | 5.687 ns | 21.55 |    0.03 | 4.0207 | 0.0076 |   33640 B |      161.73 |
| Stdlib    | (^a\*.(...)iii9) [72] |   224.44 ns | 0.204 ns | 0.171 ns |  1.00 |    0.00 | 0.0248 |      - |     208 B |        1.00 |
| Span      | (^a\*.(...)iii9) [72] | 1,127.82 ns | 2.100 ns | 1.753 ns |  5.03 |    0.01 |      - |      - |         - |        0.00 |

[0]: https://www.cs.princeton.edu/courses/archive/spr09/cos333/beautiful.html
[1]: MiniGrep.Benchmarks/Implementations/Unsafe.cs
[2]: MiniGrep.Benchmarks/Implementations/Substring.cs
[3]: MiniGrep.Benchmarks/Implementations/Span.cs
[4]: MiniGrep.Benchmarks/Implementations/Stdlib.cs
