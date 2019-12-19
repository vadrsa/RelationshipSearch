namespace SearchLib
{
    public class Node
    {

        public readonly Node Parent;
        public readonly IAction Action;
        public readonly IState State;

        public Node(Node parent, IAction action, IState state)
        {
            this.Parent = parent;
            this.Action = action;
            this.State = state;
        }

        public int depth()
        {
            if (Parent == null) return 1;
            return Parent.depth() + 1;
        }
    }
}
