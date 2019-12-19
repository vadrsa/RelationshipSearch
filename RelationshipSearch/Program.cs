using Microsoft.FamilyShowLib;
using SearchLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelationshipSearch
{
    class Program
    {
        static void Main(string[] args)
        {
            var people = new PeopleCollection();
            new GedcomImport().Import(people, @"C:\Users\davit\source\repos\RelationshipSearch\hayden.ged");
            Person from = people[0];
            Person to = people[163];
            var solution = new GraphSearch(new BreadthFirstFrontier()).FindSolution(new PersonChildState(from), new GoalTest(to));
            if (solution == null)
                solution = new GraphSearch(new BreadthFirstFrontier()).FindSolution(new PersonParentState(from), new GoalTest(to));
            if(solution == null)
            {
                solution = new GraphSearch(new BreadthFirstFrontier()).FindSolution(new PersonCousinState(from, true), new GoalTest(to));
            }
            var html = HtmlHelper.ConvertToHtml(solution);
            File.WriteAllText("D:/temp/test.html", html);
        }
    }
}
