using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mod1_proc
{
    class Scheduler
    {
        private LinkedList<Process>[] process_list;
        private SortedList<int, int> processes_waiting;
        private short current_priority;
        private short n;

        public Scheduler()
        {
            Console.WriteLine("[PLANISTA] Inicjalizacja planisty.");
            n = 0;
            process_list = new LinkedList<Process>[8];
            processes_waiting = new SortedList<int, int>();
            for( int i = 0; i < 8; i++)
            {
                process_list[i] = new LinkedList<Process>();
            }
            current_priority = this.getHighestPriority();
            Console.WriteLine("[PLANISTA] Planista zainicjalizowany pomyslnie.)");
        }

        public short getCurrentPriority()
        {
            return current_priority;
        }

        public int getProcessCount()
        {
            int n = 0;
            foreach (LinkedList<Process> p in process_list)
            {
                n += p.Count();
            }
            return n;
        }

        public void removeProcess(Process p)
        {
            processes_waiting.Remove(p.getPID());
            process_list[current_priority].Remove(p);
            process_list[p.getPriority()].Remove(p);
            if(this.getHighestPriority() < current_priority)
            {
                n = 0;
                current_priority = this.getHighestPriority();
            }
            process_list[current_priority].First().loadProcessorState();
            Console.WriteLine("[PLANISTA] Usuniecie procesu {0}.", p.getPID());
;        }
        public void removeProcess(int pid)
        {
            Process p = findProcess(pid);
            removeProcess(p);
        }

        public void printProcessList()
        {
            for (int i = 0; i < 8; i++)
            {
                Console.WriteLine("Priorytet: {0}", i);
                foreach (Process p in process_list[i])
                {
                    Console.WriteLine("[ PID: {0} Stan: {1} ] ", p.getPID(), p.getState());
                }
            }
        }

        public void printWaitingList()
        {
            foreach (KeyValuePair<int, int> key in processes_waiting)
            {
                Console.WriteLine("[ PID: {0} Licznik: {1}", key.Key, key.Value);
            }
        }

        public Process findProcess(int PID)
        {
            for (int i = 0; i < 8; i++)
            {
                foreach (Process p in process_list[i])
                {
                    if(p.getPID() == PID)
                    {
                        return p;
                    }
                }
            }
            return null;
        }

        public void addProcess(ref Process p)
        {

            processes_waiting.Add(p.getPID(), 0);
            process_list[p.getPriority()].AddLast(p);
            if(this.getHighestPriority() > current_priority)
            {
                this.getCurrentRunning().saveProcessorState();
                this.getCurrentRunning().ready();
                n = 0;
                current_priority = this.getHighestPriority();
                this.getCurrentRunning().loadProcessorState();
            }
            Console.WriteLine("[PLANISTA] Dodano proces {0}", p.getPID());
        }
        


        public void nextTick()
        {
            this.getCurrentRunning().saveProcessorState();
            
            bool hunger = false;
            SortedList<int, int> proc_wait_copy = new SortedList<int, int>(this.processes_waiting);
            if (this.current_priority < this.getHighestPriority())
            {
                this.getCurrentRunning().saveProcessorState();
                n = 0;
                current_priority = this.getHighestPriority();
                this.getCurrentRunning().loadProcessorState();
            }
            foreach (KeyValuePair<int, int> pair in proc_wait_copy)
            {
                Process p = this.findProcess(pair.Key);
                if ( p.getPriority() < 4 && p.getPID() != this.getCurrentRunning().getPID() && p.getPriority() != this.current_priority && p.getPriority() != 0 && !process_list[4].Contains(p) && p.getState() != 1 )
                {
                   processes_waiting[pair.Key] =  pair.Value +1;
                    if(processes_waiting[pair.Key] > 6)
                    {
                        this.getCurrentRunning().ready();

                        this.removeProcess(p);
                        process_list[4].AddLast(p);
                        if (this.getHighestPriority() > current_priority)
                        {

                            hunger = true;

                            current_priority = this.getHighestPriority();
                        }
                        processes_waiting[pair.Key] = 0;
                        this.getCurrentRunning().run();
                    }
                }
            }
            if (hunger)
            {

                n = 0;
            }
            n++;
            if(n > 3 || this.getCurrentRunning().getState() == 1)
            {
                moveQueue();
                n = 1;
            }
            Console.WriteLine("[PLANISTA] Wykonywanie nastepnego cyklu.");
            Console.WriteLine("[PLANISTA] Aktualnie wykonywany proces {0}, kwant czasu = {1}", this.getCurrentRunning().getPID(), n);
            
            this.getCurrentRunning().loadProcessorState();
            this.getCurrentRunning().run();
        }
        public Process getCurrentRunning()
        {
            return process_list[current_priority].First();
        }

        private void moveQueue()
        {
            process_list[current_priority].First().saveProcessorState();
          
            Console.WriteLine("[PLANISTA] Przejscie do nastepnego procesu w kolejce.");
            if (process_list[current_priority].First().getState() == 1)
            {
                while (process_list[current_priority].First().getState() == 1)
                {
                    Console.WriteLine("[PLANISTA] Omijanie procesu w stanie waiting.");
                    process_list[process_list[current_priority].First().getPriority()].AddLast(process_list[current_priority].First());
                    process_list[current_priority].RemoveFirst();
                    current_priority = this.getHighestPriority();
                    n = 0;
                }
            }
            else
            {
                Console.WriteLine("[PLANISTA] Przesuniecie kolejki.");
                process_list[current_priority].First().ready();
                process_list[process_list[current_priority].First().getPriority()].AddLast(process_list[current_priority].First());
                process_list[current_priority].RemoveFirst();
                current_priority = this.getHighestPriority();
                n = 0;
            }
            process_list[current_priority].First().loadProcessorState();
            
        }
        public void updatePriority()
        {
            this.current_priority = getHighestPriority();
        }
        private short getHighestPriority()
        {
            for (short i = 7; i >= 0; i--)
            {
                if (process_list[i].Count() > 0)
                {
                    foreach (Process p in process_list[i])
                    {
                        if(p.getState() == 2 || p.getState() == 0)
                        {
                            return i;
                        }
                    }     
                }
            }
            return 0;
        }
    }
}
