using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship_3_OOP1.Classes
{
    public class TaskFunctions
    {
        public static void TaskManagement()
        {
            Console.WriteLine("\n\ta) Prikaz detalja odabranog zadatka\n\tb) Uređivanje statusa zadatka");
            while (true)
            {
                char option = Console.ReadKey().KeyChar;
                switch (option)
                {
                    case 'a':
                        ShowDetailsOfTask();
                        return;
                    case 'b':
                        EditTaskStatus();
                        return;
                    default:
                        Console.WriteLine("Krivi unos, unesite opet");
                        break;
                }
            }
        }
        public static void EditTaskStatus()
        {
            string nameOfProject = ProjectFunctions.getNameOfProject(false);
            var project = FunctionalityFunctions.FindProject(nameOfProject);
            if (project == null)
            {
                Console.WriteLine("Ne postoji projekt s tim imenom");
                return;
            }
            var listOfTasks = Program.projects[project];
            Console.WriteLine("unesite ime zadatka kod kojeg zelite promijeniti status");
            var nameOfTask = Console.ReadLine();
            if (string.IsNullOrEmpty(nameOfTask))
            {
                Console.WriteLine("ne moze biti prazno");
                EditTaskStatus();
                return;
            }
            ChangeStatusOfTask(nameOfTask, listOfTasks);
            CheckIfAllTasksAreFinished(project);
        }
        private static void CheckIfAllTasksAreFinished(Project project)
        {
            var listOfTasks = Program.projects[project];
            foreach (var task in listOfTasks)
            {
                if(task.Status != Status.StatusTask.Finished)
                    return;
            }
            project.Status = Status.ProjectStatus.Finished;
            Console.WriteLine("\nSvi zadaci zavrseni => projekt postavljen na zavrsen");
        }
        private static void ChangeStatusOfTask(string nameOfTask, List<ProjectTasks> listOfTasks)
        {
            string newStatus = "";
            bool isFound = false;
            
            foreach (var task in listOfTasks)
            {
                if (task.NameOfTask == nameOfTask)
                {
                    newStatus = ChooseNewStatus();
                    SetNewStatus(newStatus, task);
                    isFound = true;
                }
            }
            if (!isFound)
            {
                Console.WriteLine("Zadatak nije pronaden");
                return;
            }
        }
        private static void SetNewStatus(string newStatus, ProjectTasks projectTask)
        {
            if (newStatus == "Active")
                projectTask.Status = Status.StatusTask.Active;
            else if (newStatus == "Finished")
                projectTask.Status = Status.StatusTask.Finished;
            else
                projectTask.Status = Status.StatusTask.Delayed;
        }
        private static string ChooseNewStatus()
        {
            while (true)
            {
                Console.WriteLine("\ta) Aktivan\n\tb) Zavrsen\n\tc) Na cekanju");
                char chooseTypeOfProjectStatus = Console.ReadKey().KeyChar;
                switch (chooseTypeOfProjectStatus)
                {
                    case 'a':
                        return "Active";
                    case 'b':
                        return "Finished";
                    case 'c':
                        return "Delayed";
                    default:
                        Console.WriteLine("ne ispravan unos, unesite opet");
                        break;
                }
            }
        }
        public static void ShowDetailsOfTask()
        {
            //string projectName = ProjectFunctions.getNameOfProject(false);
            //var foundProject = FunctionalityFunctions.FindProject(projectName);
            //if(foundProject == null)
            //{
            //    Console.WriteLine("projekt s odabranim imenom nije pronaden");
            //    return;
            //}
            string projectName = PrintAllTasks();
            if (string.IsNullOrEmpty(projectName))
                return;

            Console.WriteLine("Unesite ime zadatka kod kojeg zelite vidjeti vise detalja");
            var taskToSee = Console.ReadLine();

            if (string.IsNullOrEmpty(taskToSee))
            {
                Console.WriteLine("ime ne moze biti prazno");
                ShowDetailsOfTask();
                return;
            }

            Project projectWhereIsTask = FunctionalityFunctions.FindProject(projectName);
            if(projectWhereIsTask == null)
                return;

            var listOfTasks = Program.projects[projectWhereIsTask];
            bool isFound = false;

            foreach (var task in listOfTasks)
            {
                if(task.NameOfTask == taskToSee)
                {
                    Console.WriteLine($"Zadatak: {task.NameOfTask}, rok za zavrsetak: {task.DeadLine}, opis zadatka: {task.DescriptionOfTask}, status zadatka: {task.Status}, ocekivano trajanje: " +
                        $"{task.ExpectedTimeToFinih}, ime projekta kojemu pripada: {task.GetProjectName()}, id projekta: {task.GetGuid()}");
                    isFound = true;
                }
            }
            if (!isFound)
            {
                Console.WriteLine("zadataka nije pronaden");
                return;
            }
        }
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
            while (true)
            {
                var taskNumber = Console.ReadLine();
                int numOfTasks;
                if (!int.TryParse(taskNumber, out numOfTasks))
                {
                    Console.WriteLine("Unesite isparavan broj zadataka");
                }
                else
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
                        Console.WriteLine($"Projekt: {project.Key.ProjectName}\n\t zadatak: {task.GetProjectName()}, rok za zavrsetak: {task.DeadLine}, opis zadatka: {task.DescriptionOfTask}");
                        isFound = true;
                    }
                }
            }
            if (!isFound)
                Console.WriteLine("Nije pronaden ni jedan zadatak koji treba biti zavrsen u iducih 7 dana");

        }
        private static string PrintAllTasks()
        {
            string nameOfProject = ProjectFunctions.getNameOfProject(false);
            var project = FunctionalityFunctions.FindProject(nameOfProject);
            if (project == null)
            {
                Console.WriteLine("Nije pronaden projekt s unesenim imenom");
                return "";
            }
            var projectTasks = Program.projects[project];
            bool isFound = false;
            foreach (var task in projectTasks)
            {
                if (task == null)
                    break;
                isFound = true;
                Console.WriteLine($"Zadatak: {task.NameOfTask}, rok za zavrsetak: {task.DeadLine}, opis zadatka: {task.DescriptionOfTask}, status zadatka: {task.Status}, ocekivano trajanje: {task.ExpectedTimeToFinih}");
            }
            if (!isFound)
            {
                Console.WriteLine("Projekt nema zadataka");
            }
            return nameOfProject;
        }
        public static void GetPrintAllTasks()
        {
            PrintAllTasks();
        }
        public static void DeleteTasksFromProjects()
        {
            string projectName = PrintAllTasks();
            if (string.IsNullOrEmpty(projectName))
                return;

            Console.WriteLine("Odaberite koji zadatak zelite izbrisati(po imenu)");
            var taskToDelete = Console.ReadLine();
            if (string.IsNullOrEmpty(taskToDelete))
            {
                Console.WriteLine("ime zadatka ne moze biti prazno");
                DeleteTasksFromProjects();
                return;
            }

            Project project = FindTask(projectName, taskToDelete);
            DeleteTask(taskToDelete, project);
        }
        private static Project FindTask(string nameOfProject, string taskToFind)
        {
            var project = FunctionalityFunctions.FindProject(nameOfProject);
            if(project == null)
            {
                Console.WriteLine("Projekt nije pronaden");
                return null;
            }
            return project;
        }
        private static void DeleteTask(string taskToFind, Project project)
        {
            var projectTasks = Program.projects[project];
            bool isFound = false;
            foreach (var task in projectTasks)
            {
                if (task.NameOfTask == taskToFind)
                {
                    isFound = true;
                    char isConfirmed = FunctionalityFunctions.getCharConfirmation();
                    if (isConfirmed == 'y')
                    {
                        projectTasks.Remove(task);
                        Console.WriteLine("Zadatak uspjesno izbrisan");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("\nOdustali set od brisanja zadatka");
                        return;
                    }
                }
            }
            if (!isFound)
            {
                Console.WriteLine("Zadatak nije pronaden");
                return;
            }
        }
        public static void SumExpectedTime()
        {
            double sumOfMinutes = 0;
            string nameOfProject = ProjectFunctions.getNameOfProject(false);
            var project = FunctionalityFunctions.FindProject(nameOfProject);
            if(project == null)
            {
                Console.WriteLine("Projekt nije pronaden");
                return;
            }

            var onlyActiveTasks = Program.projects[project]
                .Where(task => task.Status == Status.StatusTask.Active);

            foreach (var task in onlyActiveTasks)
            {
                sumOfMinutes += task.ExpectedTimeToFinih;
            }
            Console.WriteLine($"Ukupno ocekivano vrijeme za zavrsiti sve zadatke iznosi: {sumOfMinutes}");
        }
    }
}