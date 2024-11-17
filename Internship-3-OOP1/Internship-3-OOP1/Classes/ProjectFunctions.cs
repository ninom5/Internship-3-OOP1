using System;
using System.Collections.Generic;
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
            if(FunctionalityFunctions.getChar() == 'y')
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
            Console.WriteLine("\n\ta) Ispis svih zadataka unutar odabranog projekta\n\tb) Prikaz detalja odabranog projekta\n\tc) Uređivanje statusa projekta\n\t" +
                "d) Dodavanje zadatka unutar projekta\n\te) Brisanje zadatka iz projekta\n\tf) Prikaz ukupno očekivanog vremena potrebnog za sve aktivne zadatke u projektu");
            char option = Console.ReadKey().KeyChar;
            while(true)
            {
                switch(option)
                {
                    case 'a':
                        TaskFunctions.GetPrintAllTasks();
                        return;
                    case 'b':
                        ShowProjectDetails();
                        return;
                }
            }
        }
        public static void ShowProjectDetails()
        {
            string nameOfProject = GetProjectName(false);
            var project = FunctionalityFunctions.FindProject(nameOfProject);
            Console.WriteLine($"Projekt: {project.ProjectName}, opis projekta: {project.DescriptionOfProject}, datum pocetka: {project.DateOfStart}, datum zavrsetka: {project.DateOfEnd}, status: {project.Status}");
        }
        public static string getNameOfProject(bool isNewName)
        {
            return GetProjectName(isNewName);
        }
    }
}
