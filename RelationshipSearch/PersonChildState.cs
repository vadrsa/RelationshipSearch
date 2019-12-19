using Microsoft.FamilyShowLib;
using SearchLib;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RelationshipSearch
{
    public class PersonChildState : PersonState
    {
        public PersonChildState(Person person) : base(person)
        {
        }

        public override IState GetActionResult(IAction action)
        {
            Relationship relationship = (Relationship)action;
            return new PersonChildState(relationship.RelationTo);
        }

        public override List<IAction> GetApplicableActions()
        {
            return Person.Relationships.Where(r => r.RelationshipType == RelationshipType.Child).Select(r => (IAction)r).ToList();
        }
    }
}
