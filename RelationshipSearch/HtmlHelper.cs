using Microsoft.FamilyShowLib;
using SearchLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RelationshipSearch
{
    public static class HtmlHelper
    {
        private const string COUSIN_BOTH_LINES = "<div class='clear'></div><div class='v-line-rin'></div><div class='v-line-kin'></div><div class='clear'></div>";
        private const string COUSIN_LEFT_LINE = "<div class='clear'></div> <div class='v-line-rin-solo'></div><div class='clear'></div>";
        private const string COUSIN_RIGHT_LINE = "<div class='clear'></div> <div class='v-line-kin-solo'></div><div class='clear'></div>";
        private const string STYLE_RIGHT = "style='float:right; margin-right:3%;'";
        private const string STYLE_LEFT = "style='float:left; margin-left:3%;'";
        
        public static string ConvertToHtml(Node solution)
        {
            string template = File.ReadAllText(@"C:\Users\davit\source\repos\RelationshipSearch\layout.html");
            Guid guid = Guid.NewGuid();
            template = template.Replace("{0}", guid.ToString());
            template = template.Replace("{", "{{");
            template = template.Replace("}", "}}");
            template = template.Replace(guid.ToString(), "{0}");
            string res = "";
            Node temp = solution;
            res += "<div id='container'>";
            List<Relationship> relationships = new List<Relationship>();
            List<Person> persons = new List<Person>();
            while (temp != null)
            {
                Relationship rel = (Relationship)temp.Action;
                if (rel != null)
                {
                    persons.Add(rel.RelationTo);
                    relationships.Add(rel);
                }
                else
                {
                    persons.Add(((PersonState)temp.State).Person);
                }
                temp = temp.Parent;
            }
            if (relationships.Count != 0 &&
                relationships[0].RelationshipType == RelationshipType.Child &&
                relationships.FirstOrDefault(r => r.RelationshipType == RelationshipType.Parent) != null)
            {

                Person commonAncestor = relationships.FirstOrDefault(r => r.RelationshipType == RelationshipType.Parent).RelationTo;
                Queue<Person> left = new Queue<Person>();
                Queue<Person> right = new Queue<Person>();
                bool passed = false;
                foreach(Relationship rel in relationships)
                {
                    if (rel.RelationTo.Id == commonAncestor.Id) {
                        passed = true;
                        continue;
                    }
                    if (!passed)
                        left.Enqueue(rel.RelationTo);
                    else
                        right.Enqueue(rel.RelationTo);
                }
                right.Enqueue(persons[persons.Count - 1]);
                res += ConvertToHtml(commonAncestor);
                res += "<div class='v-line'></div> <div class='h-line'></div> <div class='clear'></div> <div class='v-line-rin'> </div> <div class='v-line-kin'> </div> <div class='clear'></div>";
                while(true)
                {
                    res += ConvertToHtml(left.Dequeue(), true);
                    res += ConvertToHtml(right.Dequeue(), false);
                    if (left.Count == 0 && right.Count == 0) break;
                    else if (left.Count == 0)
                    {
                        res += COUSIN_RIGHT_LINE;
                        break;
                    }
                    else if (right.Count == 0)
                    {
                        res += COUSIN_LEFT_LINE;
                        break;
                    }
                    else
                        res += COUSIN_BOTH_LINES;
                }
                while(left.Count != 0)
                {
                    res += ConvertToHtml(left.Dequeue(), true);
                    if (left.Count != 0)
                        res += COUSIN_LEFT_LINE;
                }
                while (right.Count != 0)
                {
                    res += ConvertToHtml(right.Dequeue(), false);
                    if(right.Count != 0)
                        res += COUSIN_RIGHT_LINE;
                }
            }
            else
            {
                bool reverse = false;
                if (relationships.Count != 0 && relationships[0].RelationshipType == RelationshipType.Child) reverse = true;
                if (reverse) persons.Reverse();
                for (int i = 0; i < persons.Count; i++)
                {
                    Person person = persons[i];
                    res += ConvertToHtml(person);
                    if (i != persons.Count - 1)
                        res += "<div class='clear'></div><div class='v-line'></div>";
                }
            }

            res += "</div>";
            return string.Format(template, res);
        }

        public static string ConvertToHtml(Person person, bool? left = null)
        {
            string style = "";
            if (left.HasValue)
            {
                if (left.Value)
                    style = STYLE_LEFT;
                else
                    style = STYLE_RIGHT;
            }
            return $"<div class=\"box\" {style}> <strong> <ul id=\"RIN6722\"> <p class=\"boxName\"> <a>{person.Name}</a> </p> </ul> </strong> </div>";
        }
    }
}
