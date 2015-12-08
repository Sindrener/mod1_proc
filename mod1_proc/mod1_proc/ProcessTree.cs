using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mod1_proc
{
 
    class ProcessTree
    {
        public List<Node> allNodes;

        public ProcessTree()
        {
            allNodes = new List<Node>();
        }

        public void addNode(Node n)
        {
            allNodes.Add(n);
        }
        public Node findNode(int PID)
        {
            foreach ( Node na in allNodes)
            {
                if(na.getProcess().getPID() == PID)
                {
                    return na;
                }
            }
            return null;
        }
        public void removeNode(Node n)
        {
            n.getParent().getChildrens().Remove(n);
            allNodes.Remove(n);
        }
    }
}
