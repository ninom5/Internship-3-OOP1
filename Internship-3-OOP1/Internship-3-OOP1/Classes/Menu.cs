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
                    "\n8 - Sortirani zadaci od najkraceg prema najduljem(po ocekivanom vremenu trajanja)\n9 - Sortirani zadaci po prioritetu\n0 - Izlaz iz aplikacije\n");
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
                        ProjectFunctions.DeleteProject();
                        break;
                    case '4':
                        TaskFunctions.ShowAllTasksDeadLine7();
                        break;
                    case '5':
                        ProjectFunctions.ShowProjectsByStatus();
                        break;
                    case '6':
                        ProjectFunctions.ProjectManagement();
                        break;
                    case '7':
                        TaskFunctions.TaskManagement();
                        break;
                    case '8':
                        BonusTasks.ShortToLong();
                        break;
                    case '0':
                        return;
                    default:
                        Console.WriteLine("neispravan unos, unesite opet");
                        break;
                }
            } while (true);
        }
    }
}
