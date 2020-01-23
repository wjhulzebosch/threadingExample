using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace threadingExample
{
    class Program
    {
        public bool wasKeyPressed = false;

        static void Main(string[] args)
        {
            Program program = new Program();
            //program.StartMultipleThread();

            Thread keyPress = new Thread(() => program.keyPress());
            keyPress.Start();

            while ( true ) {
                if(program.wasKeyPressed)
                {
                    Console.WriteLine("A key was pressed");
                }
                else
                {
                    Console.WriteLine("No key was pressed");
                }
            }

            keyPress.stop();
        }

        ConsoleKey keyPress()
        {
            ConsoleKey keyPressed = Console.ReadKey().Key;
            wasKeyPressed = true;

            return keyPressed;
        }

        void StartMultipleThread()
        {
            DateTime startTime = DateTime.Now;

            Thread t1 = new Thread(() =>
            {
                int numberOfSeconds = 0;
                while (numberOfSeconds < 5)
                {
                    Thread.Sleep(1000);

                    numberOfSeconds++;
                }

                Console.WriteLine("T1 ran for 5 seconds");
            });

            Thread t2 = new Thread(() =>
            {
                int numberOfSeconds = 0;
                while (numberOfSeconds < 8)
                {
                    Thread.Sleep(1000);

                    numberOfSeconds++;
                }

                Console.WriteLine("T2 ran for 8 seconds");
            });


            //parameterized thread
            Thread t3 = new Thread(p =>
            {
                int numberOfSeconds = 0;
                while (numberOfSeconds < Convert.ToInt32(p))
                {
                    Thread.Sleep(1000);

                    numberOfSeconds++;
                }

                Console.WriteLine("T3 ran for {0} seconds", numberOfSeconds);
            });

            t1.Start();
            t2.Start();
            //passing parameter to parameterized thread
            t3.Start(2);

            //wait for t1 to fimish
            t1.Join();

            //wait for t2 to finish
            t2.Join();

            //wait for t3 to finish
            t3.Join();


            Console.WriteLine("All Threads Exited in {0} secods", (DateTime.Now - startTime).TotalSeconds);
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
