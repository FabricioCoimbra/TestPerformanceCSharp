
using BenchmarkDotNet.Attributes;

namespace TestPerformanceCSharp
{
    public class TestSerializeObject
    {
        private List<Foo> foos = new();
        private List<Bar> bars = new();
        private int iterations = 500;

        public TestSerializeObject()
        {
            for (int i = 0; i < iterations; i++)
            {
                foos.Add(new Foo() { y = i});
                bars.Add(new Bar() { y = i});
            }
        }
        [Benchmark]
        public void SerliazeNative()
        {
            string valueFooSerialized = System.Text.Json.JsonSerializer.Serialize(foos);
            string valueBarSerialized = System.Text.Json.JsonSerializer.Serialize(bars);

            var newfoo = System.Text.Json.JsonSerializer.Deserialize<List<Foo>>(valueFooSerialized);
            var newbar = System.Text.Json.JsonSerializer.Deserialize<List<Bar>>(valueBarSerialized);
        }
        [Benchmark]
        public void SerializeNewtonsoft()
        {
            string valueFooSerialized = Newtonsoft.Json.JsonConvert.SerializeObject(bars);
            string valueBarSerialized = Newtonsoft.Json.JsonConvert.SerializeObject(bars);

            var newfoo = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Foo>>(valueFooSerialized);
            var newbar = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Bar>>(valueBarSerialized);
        }
    }
}
