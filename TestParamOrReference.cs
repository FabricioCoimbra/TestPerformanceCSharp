using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            

            soma5(myFoo);
            soma5Bar(myBar);
            Console.WriteLine($"myFoo = {myFoo.y} myBar = {myBar.y}");
        }

        private static void soma5(Foo ParamFoo)
        {
            ParamFoo.y += 5;
        }

        private static void soma5Bar(Bar ParamBar)
        {
            ParamBar.y += 5;
        }
    }
}
