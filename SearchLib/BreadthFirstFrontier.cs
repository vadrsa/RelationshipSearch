using System.Collections.Generic;

namespace SearchLib
{

    public class BreadthFirstFrontier : IFrontier
    {
        // the underlying queue
        Queue<Node> queue;

        public BreadthFirstFrontier()
        {
            // initialize the queue
            queue = new Queue<Node>();
        }

        public void Add(Node node)
        {
            // add the node to the queue
            queue.Enqueue(node);
        }

        public Node Remove()
        {
            // if not empty remove
            if (!IsEmpty())
            {
                return queue.Dequeue();
            }
            // else return null
            return null;
        }

        public void Clear()
        {
            queue.Clear();
        }

        public bool IsEmpty()
        {
            return queue.Count == 0;
        }

        public bool Contains(Node node)
        {
            return queue.Contains(node);
        }
    }
}
