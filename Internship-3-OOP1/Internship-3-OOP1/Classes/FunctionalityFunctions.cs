﻿using System;
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
        
        private static char Confirmation()
        {
            Console.WriteLine("Zelite li to stvarno izbrisati. y/n");
            return Console.ReadKey().KeyChar;
        }

        public static char getCharConfirmation()
            { return Confirmation(); }

        public static Project FindProject(string projectToFind)
        {
            foreach (var project in Program.projects.Keys)
            {
                if (project.ProjectName == projectToFind)
                {
                    return project;
                }
            }
            return null;
        }

        public static void GetPrinted(List<ProjectTasks> sortedList)
        {
            PrintSortedTasks(sortedList);
        }
        private static void PrintSortedTasks(List<ProjectTasks> sortedList)
        {
            if(!sortedList.Any())
            {
                Console.WriteLine("nema zadataka");
                return;
            }
            Console.WriteLine("Zadaci sortirani zeljenom kriteriju");
            foreach (var task in sortedList)
            {
                Console.WriteLine($"Zadatak: {task.NameOfTask}, ocekivano trajanje: {task.ExpectedTimeToFinih}, prioritet: {task.Priority}, rok za zavrsetak: {task.DeadLine}, opis zadatka: {task.DescriptionOfTask}, " +
                    $"status zadatka: {task.Status}");
            }
        }
    }
}
