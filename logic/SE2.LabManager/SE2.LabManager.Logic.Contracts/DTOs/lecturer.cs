using System.Collections.Generic;

namespace SE2.LabManager.Logic.Contracts.DTOs {
    public class Lecturer {
        public Lecturer() {
            this.Courses = new HashSet<Course>();
        }

        public int LecturerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Salutation { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}
