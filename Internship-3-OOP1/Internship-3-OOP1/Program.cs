
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
            //dodat konstruktor za i unos sa statusom
            var project1 = new Project("Projekt1", "projekt1 bla bla", new DateOnly(2024, 10, 02), new DateOnly(2024, 12, 31));
            var project2 = new Project("Projekt2", "projekt2 bla bla", new DateOnly(2024, 11, 10), new DateOnly(2025, 03, 04));
            var project3 = new Project("Projekt3", "projekt3 bla bla", new DateOnly(2023, 11, 10), new DateOnly(2024, 03, 04), Status.ProjectStatus.Pending);

            var task1Project1 = new ProjectTasks("zadatak1", "zadatak1 bla bla", new DateOnly(2024, 10, 31), 300, project1.ProjectName, project1.getId());
            var task2Project1 = new ProjectTasks("zadatak2", "zadatak2 bla bla", new DateOnly(2024, 11, 20), 150, project1.ProjectName, project1.getId());

            var task1Project2 = new ProjectTasks("zadatak1", "zdk1 bla bla", new DateOnly(2024, 11, 24), 300, project2.ProjectName, project2.getId());
            var task2Project2 = new ProjectTasks("zadatak2", "zadatak2 bla bla", new DateOnly(2025, 02, 04), 150, project2.ProjectName, project2.getId());
            var task3Project2 = new ProjectTasks("zadatak3", "zadatak3 bla bla", new DateOnly(2024, 11, 12), 200, project2.ProjectName, project2.getId(), Status.StatusTask.Finished, Priority.High);
            var task4Project2 = new ProjectTasks("zadatak4", "zadatak4 bla bla", new DateOnly(2024, 12, 31), 400, project2.ProjectName, project2.getId(), Status.StatusTask.Finished, Priority.Low);
            
            projects[project1] = new List<ProjectTasks> { task1Project1, task2Project1 };
            projects[project2] = new List<ProjectTasks> { task1Project2, task2Project2, task3Project2, task4Project2 };
            projects[project3] = new List<ProjectTasks> { };
            Menu.MainMenu();
        }
    }
}