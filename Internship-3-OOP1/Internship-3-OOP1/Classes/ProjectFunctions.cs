using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship_3_OOP1.Classes
{
    public class ProjectFunctions
    {
        public static void CreateNewProject()
        {
            Console.WriteLine("Prvo unosite podatke o projektu te potom zadatke i podatke o njima");
            string nameOfProject = GetProjectName();

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
        private static string GetProjectName()
        {
            List<ProjectTasks> dummy = new List<ProjectTasks>();
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
                if (FunctionalityFunctions.CheckIfNameExists(nameOfProject, "projekt", dummy))
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
            List<ProjectTasks> dummy = new List<ProjectTasks>();
            Console.WriteLine("Unesite ime projekta koji zelite izbrisati");
            string projectToDelete = GetProjectName();
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

        }
    }
}
