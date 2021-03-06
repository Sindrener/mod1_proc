﻿using System;
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
            if (planista.getCurrentRunning().getPriority() != 0)
            {
                Process p = planista.getCurrentRunning();
                short ip = p.getIP();
                ip += 1;
                Console.WriteLine("[INTERPRETER] IP wynosi : {0}", ip);
                p.setIP(ip);
            }
        }
    }
}
