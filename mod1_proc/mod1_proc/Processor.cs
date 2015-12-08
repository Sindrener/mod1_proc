using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mod1_proc
{

    class Processor
    {
        private short r0;
        private short r1;
        private short r2;
        private short r3;

        public Processor()
        {
            r0 = 0;
            r1 = 0;
            r2 = 0;
            r3 = 0;
        }

        public short getR0()
        {
            return r0;
        }
        public short getR1()
        {
            return r1;
        }
        public short getR2()
        {
            return r2;
        }
        public short getR3()
        {
            return r3;
        }

        public void setR0(short arg)
        {
            r0 = arg;
        }

        public void setR1(short arg)
        {
            r1 = arg;
        }

        public void setR2(short arg)
        {
            r2 = arg;
        }

        public void setR3(short arg)
        {
            r3 = arg;
        }

    }
}
