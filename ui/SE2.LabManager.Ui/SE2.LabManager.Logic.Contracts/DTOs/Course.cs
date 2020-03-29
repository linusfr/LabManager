using System.Collections.Generic;

namespace SE2.LabManager.Logic.Contracts.DTOs {
    public class Course {
        public Course() {
            this.Labs = new HashSet<Lab>();
            this.Lecturers = new HashSet<Lecturer>();
        }

        public int CourseID { get; set; }
        public string Name { get; set; }
        public string Semester { get; set; }

        public virtual ICollection<Lab> Labs { get; set; }
        public virtual ICollection<Lecturer> Lecturers { get; set; }
    }
}
