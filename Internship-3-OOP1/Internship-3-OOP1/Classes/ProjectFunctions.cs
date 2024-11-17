using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship_3_OOP1.Classes
{
    public class ProjectFunctions
    {
        private static List<ProjectTasks> dummy = new List<ProjectTasks>();
        public static void CreateNewProject()
        {
            Console.WriteLine("Prvo unosite podatke o projektu te potom zadatke i podatke o njima");
            string nameOfProject = GetProjectName(true);

            Console.WriteLine("Unesite opis projekta");
            string description = GetProjectDescription();

            Console.WriteLine("unesite datum pocetka projekta u formatu dd/MM/yyyy: ");
            DateOnly dateOfStart = GetDateOfStart();

            Console.WriteLine("unesite datum kraja projekta u formatu dd/MM/yyyy: ");
            DateOnly dateOfEnd = GetDateOfEnd(dateOfStart);
            
            var newProject = new Project(nameOfProject, description, dateOfStart, dateOfEnd);
            Console.WriteLine("Projekt uspjesno kreiran");

            TaskFunctions.CreateTask(newProject);
        }
        private static string GetProjectName(bool isNewName)
        {
            string nameOfProject;
            while (true)
            {
                Console.WriteLine("Unesite ime projekta: ");
                nameOfProject = Console.ReadLine();
                if (string.IsNullOrEmpty(nameOfProject))
                {
                    Console.WriteLine("Ime projekta ne smije biti prazno unesite opet");
                    continue;
                }
                if (FunctionalityFunctions.CheckIfNameExists(nameOfProject, "projekt", dummy) && isNewName)
                {
                    Console.WriteLine("Već postoji projekt s istim imenom molimo odaberite novo ime");
                    continue;
                }
                break;
            }
            return nameOfProject;
        }
        
        private static string GetProjectDescription()
        {
            string description = "";
            while (true)
            {
                description = Console.ReadLine();
                if (!string.IsNullOrEmpty(description))
                    break;

                Console.WriteLine("Opis projekta ne moze biti prazno. Unesite opet");
            }
            return description;
        }
        private static DateOnly GetDateOfStart()
        {
            DateOnly dateOfStart;
            while (true)
            {
                var date = Console.ReadLine();
                if (DateOnly.TryParse(date, out dateOfStart))
                    break;
                Console.WriteLine("unesen neispravan format datuma, unseite opet");
            }
            return dateOfStart;
        }
        private static DateOnly GetDateOfEnd(DateOnly dateOfStart)
        {
            DateOnly dateOfEnd;
            while (true)
            {
                var date = Console.ReadLine();
                if (!DateOnly.TryParse(date, out dateOfEnd))
                {
                    Console.WriteLine("unesen neispravan format datuma, unseite opet");
                    continue;
                }

                if (dateOfEnd >= dateOfStart)
                    break;

                Console.WriteLine("Datum kraja ne moze biti prije datuma pocetka, unesite opet");
            }
            return dateOfEnd;
        }

        public static void DeleteProject()
        {
            Console.WriteLine("Unesite ime projekta koji zelite izbrisati");
            string projectToDelete = GetProjectName(false);
            var project = FunctionalityFunctions.FindProject(projectToDelete);
            if(project == null)
            {
                Console.WriteLine("Ne postoji projekt s unesenim imenom");
                return;
            }
            if(FunctionalityFunctions.getCharConfirmation() == 'y')
            {
                Program.projects.Remove(project);
                Console.WriteLine("Projekt uspjesno izbrisan");
            }
            else
                Console.WriteLine("Odustali ste od brisanja projekta");
        }

        public static void ShowProjectsByStatus()
        {
            bool isFound = false;
            string status = ProjectStatus();

            var activeProjects = Program.projects
                .Where(project => project.Key.Status.ToString() == status /*Status.ProjectStatus.Active*/);
            foreach (var project in activeProjects)
            {
                if(project.Key != null)
                    isFound = true;
                Console.WriteLine($"Projekt: {project.Key.ProjectName}, status: {project.Key.Status}, datum zavrsetka: {project.Key.DateOfEnd}, opis projekta: {project.Key.DescriptionOfProject}");
            }
            if(!isFound)
            {
                Console.WriteLine($"Projekt sa statusom: {status} nije pronaden");
            }
        }
        private static string ProjectStatus()
        {
            //string status = "";
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
                        return "Pending";
                    default:
                        Console.WriteLine("ne ispravan unos, unesite opet");
                        break;
                }
            }
        }
        public static void ProjectManagement()
        {
            while(true)
            {
                Console.WriteLine("\n\ta) Ispis svih zadataka unutar odabranog projekta\n\tb) Prikaz detalja odabranog projekta\n\tc) Uređivanje statusa projekta\n\t" +
                "d) Dodavanje zadatka unutar projekta\n\te) Brisanje zadatka iz projekta\n\tf) Prikaz ukupno očekivanog vremena potrebnog za sve aktivne zadatke u projektu");
                char option = Console.ReadKey().KeyChar;
                switch (option)
                {
                    case 'a':
                        TaskFunctions.GetPrintAllTasks();
                        return;
                    case 'b':
                        ShowProjectDetails();
                        return;
                    case 'c':
                        EditStatusOfProject();
                        return;
                    case 'd':
                        ChooseProject();
                        return;
                    case 'e':
                        TaskFunctions.DeleteTasksFromProjects();
                        return;
                    case 'f':
                        TaskFunctions.SumExpectedTime();
                        return;
                    default:
                        Console.WriteLine("krivi unos, unesite opet");
                        break;
                }
            }
        }
        public static void ChooseProject()
        {
            Console.WriteLine("unesite ime projekta u koji zelite dodati zadatke");
            string nameOfProject = GetProjectName(false);
            var project = FunctionalityFunctions.FindProject(nameOfProject);
            if(project == null)
            {
                Console.WriteLine("ne postoji projekt s unesenim imenom");
                return;
            }
            string statusOfCurrentProject = CheckStatus(project);
            if(statusOfCurrentProject == "Finished")
            {
                Console.WriteLine("zavrseni projekt se ne moze uredivati, niti dodavati zadatke");
                return;
            }
            TaskFunctions.CreateTask(project);
        }
        private static string CheckStatus(Project project)
        {
            return project.Status.ToString();
        }
        public static void EditStatusOfProject()
        {
            string nameOfProject = getNameOfProject(false);
            var project = FunctionalityFunctions.FindProject(nameOfProject);
            if(project == null)
            {
                Console.WriteLine("uneseni projekt ne postoji");
                return; 
            }
            Console.WriteLine($"Trenutni status odabranog projekta({nameOfProject}): {project.Status}" +
                $"\n\nOdaberite u koji status zelite promijeniti");
            string newStatus = ProjectStatus();
            if (newStatus == "Pending")
                project.Status = Status.ProjectStatus.Pending;
            else if (newStatus == "Finished")
                project.Status = Status.ProjectStatus.Finished;
            else
                project.Status = Status.ProjectStatus.Active;
        }
        public static void ShowProjectDetails()
        {
            string nameOfProject = GetProjectName(false);
            var project = FunctionalityFunctions.FindProject(nameOfProject);
            Console.WriteLine($"Projekt: {project.ProjectName}, opis projekta: {project.DescriptionOfProject}, datum pocetka: {project.DateOfStart}, datum zavrsetka: {project.DateOfEnd}, status: {project.Status}");
        }
        public static void PrintAllProjects()
        {
            foreach (var project in Program.projects)
            {
                Console.WriteLine($"\nIme projekta: {project.Key.ProjectName}, opis projekta: {project.Key.DescriptionOfProject}, datum pocetka projekta: {project.Key.DateOfStart}, " +
                    $"datum zavrsetka: {project.Key.DateOfEnd}, status: {project.Key.Status}");
                foreach (var task in project.Value)
                {
                    Console.WriteLine($"\tZadatak: {task.NameOfTask}, opis zadatka: {task.DescriptionOfTask}, ocekivano vrijeme zavrsetka zadatka: {task.ExpectedTimeToFinih}, status zadatka: {task.Status}");
                }
                //Console.WriteLine("\n");
            }
        }
        public static string getNameOfProject(bool isNewName)
        {
            return GetProjectName(isNewName);
        }
    }
}
