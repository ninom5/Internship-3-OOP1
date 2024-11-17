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
            if (continueChar == '0')
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
                        if (!FunctionalityFunctions.CheckIfNameExists(nameOfTask, "zadatak", tasks))
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
                while (true)
                {
                    var date = Console.ReadLine();
                    if (DateOnly.TryParse(date, out deadlineDate) && deadlineDate <= newProject.DateOfEnd && deadlineDate >= newProject.DateOfStart)
                        break;
                    Console.WriteLine("unesen neispravan format datuma ili ste unijeli datum za zavrsetak zadatka nakon planiranog datuma zavrsetka projekta ili prije pocetka projekta, unseite opet");
                }
                Console.WriteLine("Unesite koliko minuta je potrebno za zavrsiti zadatak");
                int timeToFinish;
                while (true)
                {
                    var time = Console.ReadLine();
                    if (int.TryParse(time, out timeToFinish))
                        break;
                    Console.WriteLine("krivi unos, unesite broj minuta");
                }
                var newTask = new ProjectTasks(nameOfTask, descriptionOfTask, deadlineDate, timeToFinish, newProject.ProjectName, newProject.getId());
                tasks.Add(newTask);
                Console.WriteLine("Zadatak uspjesno kreiran i dodan u listu zadataka");
            }
            Program.projects[newProject] = tasks;
            Console.WriteLine("Projekt sa svojim zadacimaa uspjesno kreiran");
        }
    }
}
