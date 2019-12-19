using Microsoft.FamilyShowLib;
using SearchLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelationshipSearch
{
    public abstract class PersonState : IState
    {

        public PersonState(Person person)
        {
            this.person = person;
        }

        private Person person;
        public Person Person => person;

        public abstract IState GetActionResult(IAction action);

        public abstract List<IAction> GetApplicableActions();

        public override string ToString()
        {
            return person.Name;
        }

    }
}
