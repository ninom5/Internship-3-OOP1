using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Internship_3_OOP1.Status;

namespace Internship_3_OOP1.Classes
{
    public class ProjectTasks
    {
        public string NameOfTask { get; set; }
        public string DescriptionOfTask { get; set; }
        public DateOnly DeadLine { get; set; }
        public int ExpectedTimeToFinih {  get; set; }
        public string ProjectName{ get; set; }
        public StatusTask Status;
        public Guid ProjectId { get; set; }
        public ProjectTasks(string nameOfTask, string description, DateOnly deadLine, int expectedTimeToFinish, string projectName, Guid id) 
        {
            NameOfTask = nameOfTask;
            DescriptionOfTask = description;
            DeadLine = deadLine;
            ExpectedTimeToFinih = expectedTimeToFinish;
            ProjectName = projectName;
            ProjectId = id;
            Status = StatusTask.Active;
        }
    }
}
