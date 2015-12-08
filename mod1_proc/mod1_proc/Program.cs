using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mod1_proc
{
    class Program
    {
        static void Main(string[] args)
        {
            Processor proc = new Processor();
            Scheduler planista = new Scheduler();
            Semafor sem = new Semafor(0);
            Interpreter inter = new Interpreter(ref planista, ref sem);
            String cmd;
            Process nadzorca = new Process(0, 0, 0, ref proc);

            Process a = new Process(12, 120, 3, ref proc);
         //   Process b = new Process(13, 10, 2, ref proc);
            Process c = new Process(18, 10, 2, ref proc);
            Process d = new Process(20, 50, 1, ref proc);
       //     Process e = new Process(16, 15, 1, ref proc);

            planista.addProcess(ref nadzorca);
            planista.addProcess(ref a);
         //   planista.addProcess(ref b);
            planista.addProcess(ref c);
            planista.addProcess(ref d);
         //   planista.addProcess(ref e);

            while (true)
            {
                planista.nextTick();
                inter.runProcess();
                cmd = Console.ReadLine();

            }
        }
    }
}
