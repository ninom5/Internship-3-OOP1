using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Internship_3_OOP1.Classes;

namespace Internship_3_OOP1.Classes
{
    public static class Menu
    {
        public static void MainMenu()
        {
            do
            {
                Console.WriteLine("\n1 - Ispis svih projekata s pripadajućim zadacima\n2 - Dodavanje novog projekta\n3 - Brisanje projekta\n4 - Prikaz svih zadataka s rokom u sljedećih 7 dana\n" +
                    "5 - Prikaz  projekata filtriranih po status\n\ta) aktivni\n\tb) završeni\n\tc) na čekanju\n6 - Upravljanje pojedinim projektom\n\ta) Ispis svih zadataka unutar odabranog projekta\n\tb) " +
                    "Prikaz detalja odabranog projekta\n\tc) Uređivanje statusa projekta\n\td) Dodavanje zadatka unutar projekta\n\te) Brisanje zadatka iz projekta\n\t" +
                    "f) Prikaz ukupno očekivanog vremena potrebnog za sve aktivne zadatke u projektu\n7 - Upravljanje pojedinim zadatkom\n\ta) Prikaz detalja odabranog zadatka\n\tb) Uređivanje statusa zadatka" +
                    "\n0 - Izlaz iz aplikacije\n");
                char option = Console.ReadKey().KeyChar;
                switch (option)
                {
                    case '1':
                        FunctionalityFunctions.PrintAllProjects();
                        break;
                    case '2':
                        ProjectFunctions.CreateNewProject();
                        break;
                    case '3':
                        DeleteProject();
                        break;
                    case '4':
                        ShowAllTasksDeadLine7();
                        break;
                    case '5':
                        ShowProjectsByStatus();
                        break;
                    case '0':
                        return;
                    default:
                        Console.WriteLine("neispravan unos, unesite opet");
                        break;
                }
            }while (true);
        }

        public static void ShowProjectsByStatus()
        {

        }
        public static void DeleteProject()
        {
            List<ProjectTasks> dummy = new List<ProjectTasks>();
            Console.WriteLine("Unesite ime projekta koji zelite izbrisati");
            var projectToDelete = Console.ReadLine();
            while (true)
            {
                if (string.IsNullOrEmpty(projectToDelete))
                {
                    Console.WriteLine("krivi unos, unesite opet");
                    continue;
                }
                if (!FunctionalityFunctions.CheckIfNameExists(projectToDelete, "projekt", dummy))
                {
                    Console.WriteLine("ne postoji projekt s unesenim imenom, unesite opet");
                    continue;
                }
                //var toDelete = Program.projects
                //    .Where(project => project.Key.ProjectName == projectToDelete);
                //    .FirstOrDefault...
                //Program.projects.Remove(toDelete);
                var allProject = Program.projects;
                foreach (var project in allProject.Keys)
                {
                    if (projectToDelete == project.ProjectName)
                    {
                        while (true)
                        {
                            char confirm = FunctionalityFunctions.getChar();
                            if (confirm == 'y')
                            {
                                var projectToErase = project;
                                allProject.Remove(projectToErase);
                                Console.WriteLine($"Projekt: {projectToErase.ProjectName} uspjesno izbrisan");
                                break;
                            }
                            else if(confirm == 'n')
                            {
                                Console.WriteLine("odustali ste od brisanja projekta");
                                return;
                            }
                            else
                            {
                                Console.WriteLine("ne ispravan unos, unesite opet");
                            }
                        }
                    }
                }
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
        
        
    }
}
