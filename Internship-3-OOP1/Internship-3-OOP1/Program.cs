
using Internship_3_OOP1.Classes;
using Internship_3_OOP1.Status;
using System.Runtime.InteropServices;

namespace Internship_3_OOP1
{
    public static class Program
    {
        public static Dictionary<Project, List<ProjectTasks>> projects = new Dictionary<Project, List<ProjectTasks>>();
        public static void Main(string[] args)
        {
            
            var project1 = new Project("Projekt1", "projekt1 bla bla", new DateOnly(2024, 10, 02), new DateOnly(2024, 12, 31));
            var project2 = new Project("Projekt2", "projekt2 bla bla", new DateOnly(2024, 11, 10), new DateOnly(2025, 03, 04));

            var task1Project1 = new ProjectTasks("zadatak1", "zadatak1 bla bla", new DateOnly(2024, 10, 31), 300, project1.ProjectName, project1.getId());
            var task2Project1 = new ProjectTasks("zadatak2", "zadatak2 bla bla", new DateOnly(2024, 11, 20), 150, project1.ProjectName, project1.getId());

            projects[project1] = new List<ProjectTasks> { task1Project1, task2Project1 };

            Menu.MainMenu();
        }
    }
}