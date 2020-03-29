using System.Collections.Generic;

namespace SE2.LabManager.Logic.Contracts.DTOs {
    public class Lab {
        public Lab() {
            this.Labdates = new HashSet<Labdate>();
            this.Tasks = new HashSet<Tasks>();
            this.Students = new HashSet<Student>();
        }

        public int LabID { get; set; }
        public int LabNumber { get; set; }
        public int Course_courseID { get; set; }

        public Course Course { get; set; }
        public ICollection<Labdate> Labdates { get; set; }
        public ICollection<Tasks> Tasks { get; set; }
        public ICollection<Student> Students { get; set; }

        //New Property
        public int LabDateCount { get; set; }
        public int StudentCount { get; set; }
    }
}
