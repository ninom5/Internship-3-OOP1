using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship_3_OOP1.Classes
{
    public class Task
    {
        public string NameOfTask { get; set; }
        public string DescriptionOfTask { get; set; }
        public DateOnly DeadLine { get; set; }
        public TaskStatus Status { get; set; }
        public TimeOnly ExpectedTimeToFinih {  get; set; }
        public Project ProjectName{ get; set; }

        public Task(string nameOfTask, string description, DateOnly deadLine, TimeOnly expectedTimeToFinish, Project projectName) 
        {
            NameOfTask = nameOfTask;
            DescriptionOfTask = description;
            DeadLine = deadLine;
            ExpectedTimeToFinih = expectedTimeToFinish;
            ProjectName = projectName;
        }
    }
}
