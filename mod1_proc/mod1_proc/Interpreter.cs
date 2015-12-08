using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mod1_proc
{
    class Interpreter
    {
        private Scheduler planista;
        private Semafor sem;

        public Interpreter(ref Scheduler p, ref Semafor s)
        {
            planista = p;
            sem = s;
        }
        public void runProcess()
        {
            // Obsluga wykonywania rozkazu.
            // Dla ulatwienia testowania kodu zmniejszalem IP (przy 0 konczenie pracy procesu) w normalnym przypadku IP powinno rosnac z kazda instrukcja (lub skakac, zaleznie od rozkazu (np call, jmp lub podobne) )
            if (planista.getCurrentRunning().getPriority() != 0)
            {
                Process p = planista.getCurrentRunning();
                short ip = p.getIP();
                ip -= 1;
                Console.WriteLine("[INTERPRETER] IP wynosi : {0}", ip);
                if(p.getPID() == 20)
                {
                    sem.op_p(ref p);
                }
                if(p.getPID() == 18)
                {
                    sem.op_p(ref p);
                }
                if(ip == 50)
                {
                    sem.op_v();
                }
                p.setIP(ip);
                if (ip <= 0)
                {
                    planista.removeProcess(p);
                    planista.updatePriority();
                }
            }
        }
    }
}
