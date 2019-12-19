using Microsoft.FamilyShowLib;
using SearchLib;

namespace RelationshipSearch
{
    public class GoalTest : IGoalTest
    {
        private Person person;
        public GoalTest(Person person)
        {
            this.person = person;
        }

        public bool IsGoal(IState state)
        {
            PersonState pState = (PersonState)state;
            if (pState.Person.Id == person.Id) return true;
            return false;
        }
    }
}
