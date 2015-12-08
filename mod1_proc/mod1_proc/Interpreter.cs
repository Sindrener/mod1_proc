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

        public Interpreter(ref Scheduler p)
        {
            planista = p;
        }
        public void runProcess()
        {
            // Obsluga wykonywania rozkazu.
            // Dla ulatwienia testowania kodu zmniejszalem IP (przy 0 konczenie pracy procesu) w normalnym przypadku IP powinno rosnac z kazda instrukcja (lub skakac, zaleznie od rozkazu (np call, jmp lub podobne) )
            if (planista.getCurrentRunning().getPriority() != 0)
            {
                short ip = planista.getCurrentRunning().getIP();
                ip -= 1;
                Console.WriteLine("[INTERPRETER] IP wynosi : {0}", ip);
                planista.getCurrentRunning().setIP(ip);
                if (ip <= 0)
                {
                    planista.removeProcess(planista.getCurrentRunning());
                    planista.updatePriority();
                }
            }
        }
    }
}
