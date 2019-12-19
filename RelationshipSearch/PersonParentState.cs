using Microsoft.FamilyShowLib;
using SearchLib;
using System.Collections.Generic;
using System.Linq;

namespace RelationshipSearch
{
    public class PersonParentState : PersonState
    {
        public PersonParentState(Person person) : base(person)
        {
        }

        public override IState GetActionResult(IAction action)
        {
            Relationship relationship = (Relationship)action;
            return new PersonParentState(relationship.RelationTo);
        }

        public override List<IAction> GetApplicableActions()
        {
            return Person.Relationships.Where(r => r.RelationshipType == RelationshipType.Parent).Select(r => (IAction)r).ToList();
        }
    }
}
