using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchLib
{
    public interface ISearch
    {

        IFrontier Frontier { get; }

        Node FindSolution(IState initialConfiguration, IGoalTest goalTest);
    }
}
