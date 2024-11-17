using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship_3_OOP1.Classes
{
    public class TaskFunctions
    {
        public static void CreateTask(Project newProject)
        {
            List<ProjectTasks> tasks = new List<ProjectTasks>();

            Console.WriteLine("Unesite broj zadataka koji zelite unijeti za odabrani projekt");
            int taskNumber = GetTaskNumber();
            
            Console.WriteLine("Ako zelite promijeniti broj zadataka pritisnite 0, za nastavak pritisnite bilo koje slovo");
            char continueChar = Console.ReadKey().KeyChar;
            if (continueChar == '0')
            {
                CreateTask(newProject);
                return;
            }
            for (int i = 1; i <= taskNumber; i++)
            {
                Console.WriteLine($"Unesite ime {i}.zadatka, ime ne smije biti prazno");
                string nameOfTask = GetTaskName();

                Console.WriteLine("Unesite opis zadatka, opis ne smije biti prazan");
                string descriptionOfTask = GetDescription();
                
                Console.WriteLine("Unesite datum do kojeg zadatak treba biti zavrsen(format: dd/MM/yyyy)");
                DateOnly deadlineDate = DateOfDeadline(newProject);
               
                Console.WriteLine("Unesite koliko minuta je potrebno za zavrsiti zadatak");
                int timeToFinish = GetTimeToFinish();

                var newTask = new ProjectTasks(nameOfTask, descriptionOfTask, deadlineDate, timeToFinish, newProject.ProjectName, newProject.getId());
                tasks.Add(newTask);
                Console.WriteLine("Zadatak uspjesno kreiran i dodan u listu zadataka");
            }
            Program.projects[newProject] = tasks;
            Console.WriteLine("Projekt sa svojim zadacima uspjesno kreiran");
        }
        private static int GetTaskNumber()
        {
            var taskNumber = Console.ReadLine();
            while (true)
            {
                int numOfTasks;
                if (!int.TryParse(taskNumber, out numOfTasks))
                {
                    Console.WriteLine("Unesite isparavan broj zadataka");
                }
                return numOfTasks;
            }
        }
        private static string GetTaskName()
        {
            List<ProjectTasks> tasks = new List<ProjectTasks>();
            string nameOfTask = "";
            while (true)
            {
                nameOfTask = Console.ReadLine();
                if (!string.IsNullOrEmpty(nameOfTask))
                {
                    if (!FunctionalityFunctions.CheckIfNameExists(nameOfTask, "zadatak", tasks))
                        return nameOfTask;
                    Console.WriteLine("Zadatak s istim imenom u ovom projektu vec postoji. Uneiste opet ime");
                    continue;
                }
                Console.WriteLine("ne ispravan unos, ime ne smije biti prazno");
            }
        }
        private static string GetDescription()
        {
            string descriptionOfTask = "";
            while (true)
            {
                descriptionOfTask = Console.ReadLine();
                if (!string.IsNullOrEmpty(descriptionOfTask))
                    return descriptionOfTask;
                Console.WriteLine("opis ne moze biti prazan, unesite opet");
            }
        }
        private static DateOnly DateOfDeadline(Project newProject)
        {
            DateOnly deadlineDate;
            while (true)
            {
                var date = Console.ReadLine();
                if (DateOnly.TryParse(date, out deadlineDate) && deadlineDate <= newProject.DateOfEnd && deadlineDate >= newProject.DateOfStart)
                    return deadlineDate;
                Console.WriteLine("unesen neispravan format datuma ili ste unijeli datum za zavrsetak zadatka nakon planiranog datuma zavrsetka projekta ili prije pocetka projekta, unseite opet");
            }
        }
        private static int GetTimeToFinish()
        {
            int timeToFinish;
            while (true)
            {
                var time = Console.ReadLine();
                if (int.TryParse(time, out timeToFinish))
                    return timeToFinish;
                Console.WriteLine("krivi unos, unesite broj minuta");
            }
        }
        public static void ShowAllTasksDeadLine7()
        {
            DateOnly dateNow = DateOnly.FromDateTime(DateTime.Now);
            bool isFound = false;
            Console.WriteLine("Sljedeci zadaci imaju rok za izvrsavanje u iducih 7 dana");
            
            foreach (var project in Program.projects)
            {
                foreach (var task in project.Value)
                {
                    var difference = (new DateTime(dateNow.Year, dateNow.Month, dateNow.Day) - new DateTime(task.DeadLine.Year, task.DeadLine.Month, task.DeadLine.Day)).Days;
                    if (difference <= 7 && task.DeadLine <= dateNow.AddDays(7))
                    {
                        Console.WriteLine($"Projekt: {project.Key.ProjectName}\n\t zadatak: {task.ProjectName}, rok za zavrsetak: {task.DeadLine}, opis zadatka: {task.DescriptionOfTask}");
                        isFound = true;
                    }
                }
            }
            if (!isFound)
                Console.WriteLine("Nije pronaden ni jedan zadatak koji treba biti zavrsen u iducih 7 dana");

        }
        private static void PrintAllTasks()
        {
            string nameOfProject = ProjectFunctions.getNameOfProject(false);
            var project = FunctionalityFunctions.FindProject(nameOfProject);
            if (project == null)
            {
                Console.WriteLine("Nije pronaden projekt s unesenim imenom");
                return;
            }
            var projectTasks = Program.projects[project];
            bool isFound = false;
            foreach (var task in projectTasks)
            {
                if (task == null)
                    break;
                Console.WriteLine($"Zadatak: {task.ProjectName}, rok za zavrsetak: {task.DeadLine}, opis zadatka: {task.DescriptionOfTask}, status zadatka: {task.Status}, ocekivano trajanje: {task.ExpectedTimeToFinih}");
            }
            if (!isFound)
            {
                Console.WriteLine("Projekt nema zadataka");
            }
        }
        public static void GetPrintAllTasks()
        {
            PrintAllTasks();
        }
    }
}
