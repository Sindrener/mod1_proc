using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mod1_proc
{
    class Node
    {
        public Process p;
        public Node parent;
        public List<Node> childrens;


        public Node(ref Node p, ref Process proc, ref ProcessTree tree)
        {
            this.parent = p;
            this.p = proc;
            p.childrens.Add(this);
            tree.addNode(this);
        }

        public Process getProcess()
        {
            return p;
        }
        public Node getParent()
        {
            return parent;
        }
        public List<Node> getChildrens()
        {
            return childrens;
        }
    }
}
