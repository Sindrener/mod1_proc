using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mod1_proc
{
    class Process
    {
        private Processor p;
        private int PID; 
        private short ip;
        private short priority;
        private short state; // 0 - running, 1 - waiting, 2 - ready
        private short[] processor_state;

        public Process(int pid, short ipc, short pr, ref Processor proc)
        {
            this.p = proc;
            PID = pid;
            ip = ipc;
            priority = pr;
            state = 0;
            processor_state = new short[4];

        }
        public short getPriority()
        {
            return priority;
        }
        public int getPID()
        {
            return PID;
        }
        public short getState()
        {
            return state;
        }
        public short getIP()
        {
            return ip;
        }
        public void setIP(short i)
        {
            ip = i;
        }

        public void wait()
        {
            state = 1;
        }
        public void ready()
        {
            state = 2;
        }
        public void run()
        {
            state = 0;
        }

        public void printProcessorState()
        {
            for(int i = 0; i < 4; i++)
            {
                Console.WriteLine("Rejestr: {0} Wartosc: {1}", i, processor_state[i]);
            }
        }

        public void loadProcessorState()
        {
            p.setR0(processor_state[0]);
            p.setR1(processor_state[1]);
            p.setR2(processor_state[2]);
            p.setR3(processor_state[3]);
        }
        public void saveProcessorState()
        {
            processor_state[0] = p.getR0();
            processor_state[1] = p.getR1();
            processor_state[2] = p.getR2();
            processor_state[3] = p.getR3();
        }
    }
}
