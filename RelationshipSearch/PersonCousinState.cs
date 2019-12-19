using Microsoft.FamilyShowLib;
using SearchLib;
using System.Collections.Generic;
using System.Linq;

namespace RelationshipSearch
{
    public class PersonCousinState : PersonState
    {
        private bool first;
        public PersonCousinState(Person person, bool first = false) : base(person)
        {
            this.first = first;
        }

        public override IState GetActionResult(IAction action)
        {
            Relationship relationship = (Relationship)action;
            return new PersonCousinState(relationship.RelationTo);
        }

        public override List<IAction> GetApplicableActions()
        {
            if(first)
                return Person.Relationships.Where(r => r.RelationshipType == RelationshipType.Parent ).Select(r => (IAction)r).ToList();
            return Person.Relationships.Where(r => r.RelationshipType == RelationshipType.Parent || r.RelationshipType == RelationshipType.Child).Select(r => (IAction)r).ToList();
        }
    }
}
