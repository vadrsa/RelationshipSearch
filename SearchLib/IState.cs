using System.Collections.Generic;

namespace SearchLib
{
    public interface IState
    {
        List<IAction> GetApplicableActions();
        IState GetActionResult(IAction action);
    }
}
