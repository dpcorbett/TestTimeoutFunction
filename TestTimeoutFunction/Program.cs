using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTimeoutFunction
{
    class Program
    {
        class Foo
        {
            public string blah = "blah";
            public int num1 = 1;
        }

        class TaskThing
        {
            public Foo DoSomething(Foo foo)
            {
                var task = Task.Run(() => LongRunningMethod(foo));
                if (task.Wait(TimeSpan.FromSeconds(5)))
                    return task.Result;
                else
                    throw new TimeoutException();//the method timed-out
            }

            public Foo LongRunningMethod(Foo foo)
            {
                System.Threading.Thread.Sleep(6000);

                foo.blah = "fooBlah";
                foo.num1 = 5;

                return foo;
            }
        }

        static void Main(string[] args)
        {
            var taskThing = new TaskThing();
            var newFoo = new Foo();

            var changedFoo = taskThing.DoSomething  (newFoo);
        }


    }
}
