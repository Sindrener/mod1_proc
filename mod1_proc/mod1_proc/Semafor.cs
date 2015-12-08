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
            return s;
        }

        public void op_p (ref Process p)
        {
            if (s > 0)
            {
                p.ready();
            }
            else
            {
                Console.WriteLine("[SEMAFOR] Operacja P zatrzymanie procesu: {0}", p.getPID());
                Console.WriteLine("[SEMAFOR] Wartosc semafora: {0}", this.s);
                p.wait();
                queue.AddLast(p);
            }
            s--;
        }

        public void op_v()
        {
            if(queue.Count() <= 0)
            {
                // semafor jest pusty;
                // ogolnie to glupie bo sygnalizacja nie powinna zachodzic dla pustego semafora ? jakis error tutaj czy chuj wie co;
            }
            else
            {
                queue.First().ready();
                queue.RemoveFirst();
            }
            s++; 
        }
        
    }
}
