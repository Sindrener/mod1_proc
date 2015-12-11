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
            Semafor sem1 = new Semafor(5);
            Semafor sem2 = new Semafor(1);
            Interpreter inter = new Interpreter(ref planista);
            String cmd;
            Process nadzorca = new Process(0, 0, 0, ref proc);
            planista.addProcess(ref nadzorca);

            while (true)
            {
                cmd = Console.ReadLine();
                switch (cmd)
                {
                    case "showreg":
                        Console.WriteLine("R0: {0} R1: {1} R2: {2} R3: {3}", proc.getR0(), proc.getR1(), proc.getR2(), proc.getR3());
                        break;
                    case "setr0":
                        proc.setR0(Convert.ToInt16(Console.ReadLine()));
                        break;
                    case "showsem1":
                        Console.WriteLine("[SEMAFOR] Wartosc semafora 1: {0}", sem1.getSemStat());
                        break;
                    case "showsem2":
                        Console.WriteLine("[SEMAFOR] Wartosc semafora 2: {0}", sem2.getSemStat());
                        break;
                    case "setr1":
                        proc.setR1(Convert.ToInt16(Console.ReadLine()));
                        break;
                    case "setr2":
                        proc.setR2(Convert.ToInt16(Console.ReadLine()));
                        break;
                    case "setr3":
                        proc.setR3(Convert.ToInt16(Console.ReadLine()));
                        break;
                    case "showpcb":
                        Console.Write("PID: ");
                        int pcbpid = Convert.ToInt32(Console.ReadLine());
                        Process pcb = planista.findProcess(pcbpid);
                        Console.WriteLine("PID: {0}  Priorytet: {1}  Stan: {2}  IP: {3} ", pcb.getPID(), pcb.getPriority(), pcb.getState(), pcb.getIP() );
                        pcb.printProcessorState();
                        break;
                    case "showproc":
                        Console.WriteLine("[PLANISTA] Aktualnie Obsługiwany proces: PID: {0} Priorytet: {1} Stan: {2}", planista.getCurrentRunning().getPID(), planista.getCurrentPriority(), planista.getCurrentRunning().getState());
                        Console.WriteLine("[PLANISTA] Lista procesów:");
                        planista.printProcessList();
                        planista.printWaitingList();
                        break;
                    case "addproc":
                        Console.Write("PID: ");
                        int addpid = Convert.ToInt32(Console.ReadLine());
                        Console.Write("IP: ");
                        short ipc = Convert.ToInt16(Console.ReadLine());
                        Console.Write("Priorytet: ");
                        short pr = Convert.ToInt16(Console.ReadLine());
                        Process p = new Process(addpid, ipc, pr, ref proc);
                        planista.addProcess(ref p);
                        planista.findProcess(addpid).ready();
                        break;
                    case "remproc":
                        Console.Write("PID: ");
                        int rempid = Convert.ToInt32(Console.ReadLine());
                        planista.removeProcess(rempid);
                        break;
                    case "opp":
                        int oprpid;
                        Console.Write("Semafor (1-2): ");
                        String s = Console.ReadLine();
                        if(s == "sem1")
                        {
                            Console.Write("PID: ");
                            oprpid = Convert.ToInt32(Console.ReadLine());
                            sem1.op_p(planista.findProcess(oprpid));
                        }
                        else if(s == "sem2")
                        {
                            Console.Write("PID: ");
                            oprpid = Convert.ToInt32(Console.ReadLine());
                            sem2.op_p(planista.findProcess(oprpid));
                        }
                        break;
                    case "opv":
                        Console.Write("Semafor (1-2): ");
                        String s1 = Console.ReadLine();
                        if ( s1 == "sem1")
                        {
                            sem1.op_v(planista);
                        }
                        else if (s1 == "sem2")
                        {
                            sem2.op_v(planista);
                        }
                        break;
                    case "c":
                        planista.nextTick();
                        inter.runProcess();
                        break;
                    case "q":
                        goto Finish;
                    default:
                        Console.WriteLine("Wybrano niepoprawna funkcje.");
                        break;

                }
            }
        Finish:;
        }
    }
}
