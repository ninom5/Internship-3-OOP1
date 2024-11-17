using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship_3_OOP1.Classes
{
    public class FunctionalityFunctions
    {
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
            else if (type == "zadatak")
            {
                foreach (var task in listOfTasks)
                {
                    if (nameOfProjectOrTask == task.NameOfTask)
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
        private static char Confirmation()
        {
            Console.WriteLine("Zelite li to stvarno izbrisati. y/n");
            return Console.ReadKey().KeyChar;
        }
        public static char getChar()
            { return Confirmation(); }
    }
}
