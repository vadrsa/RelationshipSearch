using System.Collections.Generic;

namespace SearchLib
{

    public class GraphSearch : ISearch
    {
        private IFrontier frontier;

        public GraphSearch(IFrontier frontier)
        {
            this.frontier = frontier;
        }

        public IFrontier Frontier => this.frontier;

        public Node FindSolution(IState initialConfiguration, IGoalTest goalTest)
        {
            HashSet<IState> explored = new HashSet<IState>();
            frontier.Add(new Node(null, null, initialConfiguration));
            while (!frontier.IsEmpty())
            {
                Node node = frontier.Remove();
                if (goalTest.IsGoal(node.State))
                    return node;
                else
                {
                    explored.Add(node.State);
                    foreach (IAction action in node.State.GetApplicableActions())
                    {
                        IState newState = node.State.GetActionResult(action);
                        Node newNode = new Node(node, action, newState);
                        if (!explored.Contains(newState) && !frontier.Contains(newNode))
                            frontier.Add(newNode);
                    }
                }
            }
            return null;
        }

    }

}
