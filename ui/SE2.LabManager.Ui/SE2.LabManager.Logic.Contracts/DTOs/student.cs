using System.Collections.Generic;

namespace SE2.LabManager.Logic.Contracts.DTOs {
    public class Student {
        public Student() {
            this.Presents = new HashSet<Present>();
            this.TaskDones = new HashSet<TaskDone>();
            this.Labs = new HashSet<Lab>();
        }

        public int StudentID { get; set; }
        public int MatricelNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StudyCourse { get; set; }
        public int Semester { get; set; }
        public string Email { get; set; }
        public string Salutation { get; set; }

        public virtual ICollection<Present> Presents { get; set; }
        public virtual ICollection<TaskDone> TaskDones { get; set; }
        public virtual ICollection<Lab> Labs { get; set; }
    }
}
