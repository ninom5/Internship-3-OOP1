using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Internship_3_OOP1;
using Internship_3_OOP1.Status;

namespace Internship_3_OOP1.Classes
{
    public class Project
    {
        public string ProjectName { get; set; }
        public string DescriptionOfProject { get; set; }
        public DateOnly DateOfStart { get; set; }
        public DateOnly DateOfEnd { get; set; }
        private Guid id;
        public ProjectStatus Status; //{ get; set; }

        
        public Project(string name, string description, DateOnly startOfProject, DateOnly endOfProject)//konstruktor
        {
            ProjectName = name;
            DescriptionOfProject = description;
            DateOfStart = startOfProject;
            DateOfEnd = endOfProject;
            Status = ProjectStatus.Active;
            id = Guid.NewGuid();
        }

        public Guid getId()
        {
            return id;
        }
    }
}
