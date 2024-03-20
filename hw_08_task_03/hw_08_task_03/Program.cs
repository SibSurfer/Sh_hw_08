public class FooBar
{
    private int n;
    public FooBar(int n)
    {
        this.n = n;
    }
    public void Foo(Action printFoo)
    {

        for (int i = 0; i < n; i++)
        {

            printFoo();
        }
    }
    public void Bar(Action printBar)
    {

        for (int i = 0; i < n; i++)
        {

            printBar();
        }
    }
}

class Pogram
{
    static void Main()


    {
        bool first = false;
        bool second = false;
        var foobar = new FooBar(5);
        Object synchronA = new Object();
        Object synchronB = new Object();

        

        var T1 = new Thread(() =>
        {
            foobar.Foo(() =>
            {
                lock (synchronA)
                {
                    Console.Write("foo");
                    first = true;
                    Monitor.Pulse(synchronA);
                }
                lock (synchronB)
                {
                    while (!second)
                    {
                        Monitor.Wait(synchronB);
                    }
                    second = false;
                }
            });
        });

        var T2 = new Thread(() =>
        {
            foobar.Bar(() =>
            {
                lock (synchronA)
                {
                    while (!first)
                    {
                        Monitor.Wait(synchronA);
                    }
                    first = false;
                }
                lock (synchronB)
                {
                    Console.Write("bar");
                    second = true;
                    Monitor.Pulse(synchronB);
                }
            });
        });

        T2.Start();
        Thread.Sleep(1000);
        T1.Start();

        T1.Join();
        T2.Join();
    }
}