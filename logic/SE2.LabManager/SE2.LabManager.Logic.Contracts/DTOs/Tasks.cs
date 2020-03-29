using System.Collections.Generic;

namespace SE2.LabManager.Logic.Contracts.DTOs {
    public class Tasks {
        public Tasks() {
            this.TaskDones = new HashSet<TaskDone>();
        }

        public int TaskID { get; set; }
        public int TaskNumber { get; set; }
        public System.DateTime DueDate { get; set; }
        public int Lab_labID { get; set; }

        public virtual Lab Lab { get; set; }
        public virtual ICollection<TaskDone> TaskDones { get; set; }
    }
}
