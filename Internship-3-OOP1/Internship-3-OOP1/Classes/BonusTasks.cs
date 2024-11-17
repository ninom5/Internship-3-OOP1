using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Internship_3_OOP1.Classes;

namespace Internship_3_OOP1.Classes
{
    public static class BonusTasks
    {
        public static void ShortToLong() 
        {
            string projectName = ProjectFunctions.getNameOfProject(false);
            var project = FunctionalityFunctions.FindProject(projectName);
            if (project == null)
            {
                Console.WriteLine("Ne postoji projekt s unesenim imenom");
                return;
            }
            SortTasks(projectName, project);
        }
        private static void SortTasks(string projectName, Project project)
        {
            var taskList = Program.projects[project];
            var sortedTasks = taskList
                .OrderBy(expextedTime => expextedTime.ExpectedTimeToFinih).ToList();
            FunctionalityFunctions.GetPrinted(sortedTasks);
        }
        public static void PrintSortedByPriority()
        {
            string projectName = ProjectFunctions.getNameOfProject(false);
            var project = FunctionalityFunctions.FindProject(projectName);
            if (project == null)
            {
                Console.WriteLine("Ne postoji projekt s unesenim imenom");
                return;
            }
            SortTasksByPriority(projectName, project);
        }
        private static void SortTasksByPriority(string projectName, Project project)
        {
            var taskList = Program.projects[project];
            var sortedTasks = taskList
                .OrderBy(priority => priority.Priority).ToList();
            FunctionalityFunctions.GetPrinted(sortedTasks);
        }
    }
}
