using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship_3_OOP1.Classes
{
    public static class Menu
    {
        public static void MainMenu()
        {
            do
            {
                Console.WriteLine("1 - Ispis svih projekata s pripadajućim zadacima\n2 - Dodavanje novog projekta\n3 - Brisanje projekta\n4 - Prikaz svih zadataka s rokom u sljedećih 7 dana\n" +
                    "5 - Prikaz  projekata filtriranih po status\n\ta) aktivni\n\tb) završeni\n\tc) na čekanju\n6 - Upravljanje pojedinim projektom\n\ta) Ispis svih zadataka unutar odabranog projekta\n\tb) " +
                    "Prikaz detalja odabranog projekta\n\tc) Uređivanje statusa projekta\n\td) Dodavanje zadatka unutar projekta\n\te) Brisanje zadatka iz projekta\n\t" +
                    "f) Prikaz ukupno očekivanog vremena potrebnog za sve aktivne zadatke u projektu\n7 - Upravljanje pojedinim zadatkom\n\ta) Prikaz detalja odabranog zadatka\n\tb) Uređivanje statusa zadatka" +
                    "\n0 - Izlaz iz aplikacije\n");
                char option = Console.ReadKey().KeyChar;
                switch (option)
                {
                    case '1':
                        PrintAllProjects();
                        break;
                    case '2':
                        CreateNewProject();
                        break;
                    case '0':
                        return;
                    default:
                        Console.WriteLine("neispravan unos, unesite opet");
                        break;
                }
            }while (true);
        }

        public static void PrintAllProjects()
        {
            foreach(var project in Program.projects)
            {
                Console.WriteLine($"\nIme projekta: {project.Key.ProjectName}, opis projekta: {project.Key.DescriptionOfProject}, datum pocetka projekta: {project.Key.DateOfStart}, " +
                    $"datum zavrsetka: {project.Key.DateOfEnd}, status: {project.Key.Status}");
                foreach(var task in project.Value)
                {
                    Console.WriteLine($"\tZadatak: {task.NameOfTask}, opis zadatka: {task.DescriptionOfTask}, ocekivano vrijeme zavrsetka zadatka: {task.ExpectedTimeToFinih}, status zadatka: {task.Status}");
                }
                Console.WriteLine("\n");
            }
        }
        public static void CreateNewProject()
        {
            List<ProjectTasks> dummy = new List<ProjectTasks>();
            Console.WriteLine("Prvo unosite podatke o projektu te potom zadatke i podatke o njima");
            string nameOfProject;
            while(true)
            {
                Console.WriteLine("Unesite ime projekta: ");
                nameOfProject = Console.ReadLine();
                if (string.IsNullOrEmpty(nameOfProject))
                {
                    Console.WriteLine("Ime projekta ne smije biti prazno unesite opet");
                    continue;
                }
                if (CheckIfNameExists(nameOfProject, "projekt", dummy))
                {
                    Console.WriteLine("Već postoji projekt s istim imenom molimo odaberite novo ime");
                    continue;
                }
                break;
            }
            
            Console.WriteLine("Unesite opis projekta");
            string description = "";
            while (true)
            {
                description = Console.ReadLine();
                if (!string.IsNullOrEmpty(description))
                    break;
                
                Console.WriteLine("Opis projekta ne moze biti prazno. Unesite opet");
            }
            Console.WriteLine("unesite datum pocetka projekta u formatu dd/MM/yyyy: ");
            DateOnly dateOfStart;
            while (true)
            {
                var date = Console.ReadLine();
                if (DateOnly.TryParse(date, out dateOfStart))
                    break;
                Console.WriteLine("unesen neispravan format datuma, unseite opet");
            }
            Console.WriteLine("unesite datum kraja projekta u formatu dd/MM/yyyy: ");
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
            var newProject = new Project(nameOfProject, description, dateOfStart, dateOfEnd);
            CreateTask(newProject);
        }
        public static void CreateTask(Project newProject)
        {
            List<ProjectTasks> tasks = new List<ProjectTasks>();
            Console.WriteLine("Unesite broj zadataka koji zelite unijeti za odabrani projekt");
            var taskNumber = Console.ReadLine();
            int numOfTasks;
            if (!int.TryParse(taskNumber, out numOfTasks))
            {
                Console.WriteLine("Unesite isparavan broj zadataka");
                CreateTask(newProject);
                return;
            }
            Console.WriteLine("Ako zelite promijeniti broj zadataka pritisnite 0, za nastavak pritisnite bilo koje slovo");
            char continueChar = Console.ReadKey().KeyChar;
            if(continueChar == '0')
            {
                CreateTask(newProject);
                return;
            }
            for (int i = 1; i <= numOfTasks; i++)
            {
                Console.WriteLine($"Unesite ime {i}.zadatka, ime ne smije biti prazno");
                string nameOfTask = "";
                while (true)
                {
                    nameOfTask = Console.ReadLine();
                    if (!string.IsNullOrEmpty(nameOfTask))
                    {
                        if (!CheckIfNameExists(nameOfTask, "zadatak", tasks))
                            break;
                        Console.WriteLine("Zadatak s istim imenom u ovom projektu vec postoji. Uneiste opet ime");
                        continue;
                    }
                    Console.WriteLine("ne ispravan unos, ime ne smije biti prazno");
                }
                Console.WriteLine("Unesite opis zadatka, opis ne smije biti prazan");
                string descriptionOfTask = "";
                while (true)
                {
                    descriptionOfTask = Console.ReadLine();
                    if (!string.IsNullOrEmpty(descriptionOfTask))
                        break;
                    Console.WriteLine("opis ne moze biti prazan, unesite opet");
                }
                Console.WriteLine("Unesite datum do kojeg zadatak treba biti zavrsen(format: dd/MM/yyyy)");
                DateOnly deadlineDate;
                while(true)
                {
                    var date = Console.ReadLine();
                    if (DateOnly.TryParse(date, out deadlineDate) && deadlineDate <= newProject.DateOfEnd)
                        break;
                    Console.WriteLine("unesen neispravan format datuma ili ste unijeli datum za zavrsetak zadatka nakon planiranog datuma zavrsetka projekta, unseite opet");
                }
                Console.WriteLine("Unesite koliko minuta je potrebno za zavrsiti zadatak");
                int timeToFinish;
                while(true)
                {
                    var time = Console.ReadLine();
                    if(int.TryParse(time, out timeToFinish))
                        break;
                    Console.WriteLine("krivi unos, unesite broj minuta");
                }
                var newTask = new ProjectTasks(nameOfTask, descriptionOfTask, deadlineDate, timeToFinish, newProject.ProjectName, newProject.getId());
                tasks.Add(newTask);
            }
            Program.projects[newProject] = tasks;
        }
        public static bool CheckIfNameExists(string nameOfProjectOrTask, string type, List<ProjectTasks> listOfTasks)
        {
            if (type == "projekt")
            {
                foreach (var project in Program.projects)
                {
                    if (nameOfProjectOrTask == project.Key.ProjectName)
                        return true;
                }
                return false;
            }
            else if(type == "zadatak")
            {
                foreach (var task in listOfTasks)
                {
                    if(nameOfProjectOrTask == task.NameOfTask)
                        return true;
                }
                return false;
            }
            else
            {
                Console.WriteLine("pogreska pokrenite ponovo program");
                return false;
            }
        }
    }
}
