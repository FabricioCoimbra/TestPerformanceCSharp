using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Diagnostics.Windows;
using BenchmarkDotNet.Diagnostics.Windows.Configs;
using System.Linq;

namespace TestPerformanceCSharp
{
    public struct Foo
    {
        public Foo(double arg) { this.y = arg; }
        public double y;
    }
    public class Bar
    {
        public Bar(double arg) { this.y = arg; }
        public double y;
    }
    public class Config : ManualConfig
    {
        public Config()
        {
            AddDiagnoser(MemoryDiagnoser.Default);
            //AddDiagnoser(new InliningDiagnoser());
            //AddDiagnoser(new EtwProfiler());
            //AddDiagnoser(ThreadingDiagnoser.Default);
        }
    }

    [MemoryDiagnoser]
    //[TailCallDiagnoser]
    //[EtwProfiler]
    //[ConcurrencyVisualizerProfiler]
    //[NativeMemoryProfiler]
    //[ThreadingDiagnoser]
    public class TestLoop
    {
        //private const int iterations = 50000000;
        private const int iterations = 50;
        string Monkeys = "monkeys!";
        private readonly List<string> ListToForeach = new();
        private readonly List<Foo> ListFoo = new();
        private readonly List<Bar> ListBar = new();
        public TestLoop()
        {
            for (int i = 0; i < iterations; i++)
                ListToForeach.Append(Monkeys);
        }

        [Benchmark]
        public void TestForSimple()
        {
            TestForFoo();
            TestForBar();
        }

        [Benchmark]
        public void TestForeach()
        {
            TesteForeachFoo();
            TestForeachBar();
        }

        [Benchmark]
        public void TestLinqBar()
        {
            ListBar.AddRange(from item in ListToForeach
                             let test2 = new Bar(3.14)
                             select test2);
            PrintAndClear(ListBar);
        }

        [Benchmark]
        public void TestLinqFoo()
        {
            ListFoo.AddRange(from item in ListToForeach
                             let test2 = new Foo(3.14)
                             select test2);
            PrintAndClear(ListFoo);
        }

        [Benchmark]
        public void TestLinqForeach()
        {
            ListToForeach.ForEach(item => ListFoo.Add(new Foo(3.14)));

            PrintAndClear(ListFoo);
        }

        [Benchmark]
        public void TestForBar()
        {
            for (int i = 0; i < iterations; i++)
            {
                Bar test2 = new Bar(3.14);
                ListBar.Add(test2);
            }
            PrintAndClear(ListBar);
        }

        public void PrintAndClear<T>(List<T> list)
        {
            //Console.WriteLine(list.Count);
            list.Clear();
        }

        [Benchmark]
        public void TestForFoo()
        {
            for (int i = 0; i < iterations; i++)
            {
                Foo test = new Foo(3.14);
                ListFoo.Add(test);
            }
            PrintAndClear(ListFoo);
        }

        [Benchmark]
        public void TestForeachBar()
        {
            foreach (var item in ListToForeach)
            {
                Bar test2 = new Bar(3.14);
                ListBar.Add(test2);
            }
            PrintAndClear(ListBar);
        }

        [Benchmark]
        public void TesteForeachFoo()
        {
            foreach (var item in ListToForeach)
            {
                Foo test = new Foo(3.14);
                ListFoo.Add(test);
            }
            PrintAndClear(ListFoo);
        }
    }
}
