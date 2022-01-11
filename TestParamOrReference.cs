namespace TestPerformanceCSharp
{
    public class TestParamOrReference
    {
        public static void Test()
        {
            Foo myFoo = new() { y = 10.0 };
            //Bar myBar = new() { y = 10.0};//do not work. Empty constructor do not exists
            Bar myBar = new(10);

            Console.WriteLine($"myFoo = {myFoo.y} myBar = {myBar.y}");            

            Soma5(myFoo);
            Soma5Bar(myBar);
            Console.WriteLine($"myFoo = {myFoo.y} myBar = {myBar.y}");
        }

        private static void Soma5(Foo ParamFoo) => ParamFoo.y += 5;

        private static void Soma5Bar(Bar ParamBar) => ParamBar.y += 5;
    }
}
