
class Program
{
    static void Main(string[] args)
    {
        object sync = new object();
        bool isThread1Turn = true;

        var thread1 = new Thread(() =>
        {
            lock (sync)
            {
                for (short i = 0; i < 10; i++)
                {
                    while (!isThread1Turn)
                    {
                        Monitor.Wait(sync);
                    }
                    Console.WriteLine("T1: " + i);
                    isThread1Turn = false;
                    Monitor.Pulse(sync);
                }
            }
        });

        var thread2 = new Thread(() =>
        {
            lock (sync)
            {
                for (short i = 0; i < 10; i++)
                {
                    while (isThread1Turn)
                    {
                        Monitor.Wait(sync);
                    }
                    Console.WriteLine("T2: " + i);
                    isThread1Turn = true;
                    Monitor.Pulse(sync);
                }
            }
        });

        thread1.Start();
        thread2.Start();

        thread1.Join();
        thread2.Join();
    }
}
