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
            //AddDiagnoser(MemoryDiagnoser.Default);
            //AddDiagnoser(new InliningDiagnoser());
            //AddDiagnoser(new EtwProfiler());
            //AddDiagnoser(ThreadingDiagnoser.Default);
        }
    }

    //[MemoryDiagnoser]
    //[TailCallDiagnoser]
    //[EtwProfiler]
    //[ConcurrencyVisualizerProfiler]
    //[NativeMemoryProfiler]
    //[ThreadingDiagnoser]
    public class TestLoop
    {
        //private const int iterations = 50000000;
        private const int iterations = 500;
        readonly string Monkeys = "monkeys!";
        private readonly List<double> ListToForeach = new();
        private readonly List<Foo> ListFoo = new();
        private readonly List<Bar> ListBar = new();
        public TestLoop()
        {
            for (int i = 0; i < iterations; i++)
                ListToForeach.Append(i);
        }

        public void CleanupList<T>(List<T> list) => list.Clear();

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
        public void TestForBar()
        {
            for (int i = 0; i < iterations; i++)
            {
                Bar test2 = new Bar(i);
                ListBar.Add(test2);
            }
            CleanupList(ListBar);
        }        

        [Benchmark]
        public void TestForFoo()
        {
            for (int i = 0; i < iterations; i++)
            {
                Foo test = new Foo(i);
                ListFoo.Add(test);
            }
            CleanupList(ListFoo);
        }

        [Benchmark]
        public void TestForeachBar()
        {
            foreach (var item in ListToForeach)
            {
                Bar test2 = new Bar(item);
                ListBar.Add(test2);
            }
            CleanupList(ListBar);
        }

        [Benchmark]
        public void TesteForeachFoo()
        {
            foreach (var item in ListToForeach)
            {
                Foo test = new Foo(item);
                ListFoo.Add(test);
            }
            CleanupList(ListFoo);
        }

        [Benchmark]
        public void TestLinqBar()
        {
            ListBar.AddRange(from item in ListToForeach
                             let test2 = new Bar(item)
                             select test2);
            CleanupList(ListBar);
        }

        [Benchmark]
        public void TestLinqFoo()
        {
            ListFoo.AddRange(from item in ListToForeach
                             let test2 = new Foo(item)
                             select test2);
            CleanupList(ListFoo);
        }

        [Benchmark]
        public void TestLinqForeach()
        {
            ListToForeach.ForEach(item => ListFoo.Add(new Foo(item)));
            ListToForeach.ForEach(item => ListBar.Add(new Bar(item)));

            CleanupList(ListBar);
            CleanupList(ListFoo);
        }
    }
}
