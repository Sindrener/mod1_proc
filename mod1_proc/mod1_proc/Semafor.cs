using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mod1_proc
{
    class Semafor
    {
        private int s;
        private LinkedList<Process> queue;
        public Semafor(int i)
        {
            s = i;
            queue = new LinkedList<Process>();
        }

        public int getSemStat()
        {
            Console.WriteLine("[SEMAFOR] Wartosc semafora: {0}", this.s);
            return s;
        }

        public void op_p (Process p)
        {
            if (s > 0)
            {
                Console.WriteLine("[SEMAFOR] Semafor jest pusty, przepuszczanie procesu: {0}", p.getPID());
                p.ready();
            }
            else
            {
                Console.WriteLine("[SEMAFOR] Operacja P zatrzymanie procesu: {0}", p.getPID());

                p.wait();
                queue.AddLast(p);
            }
            s--;
        }

        public void op_v(Scheduler p)
        {
            if(queue.Count() <= 0)
            {
               
            }
            else
            {
                Console.WriteLine("[SEMAFOR] Operacja V uruchamianie procesu: {0}", queue.First().getPID());
                p.getCurrentRunning().ready();
                queue.First().ready();
                queue.RemoveFirst();
            }
            s++; 
        }
        
    }
}
