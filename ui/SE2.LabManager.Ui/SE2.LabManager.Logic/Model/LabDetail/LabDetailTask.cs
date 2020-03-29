using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE2.LabManager.Logic.Model.LabDetail {
    public class LabDetailTask {

        // student
        public int StudentID { get; set; }
        public int MatricelNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get => $"{FirstName} {LastName}"; }

        // task
        public int TaskID { get; set; }
        public int TaskNumber { get; set; }
        public DateTime DueDate { get; set; }
        public string DueDateString { get => $"{DueDate.Hour} Uhr am {DueDate.Day}.{DueDate.Month}"; }


        // taskDone
        public int TaskDoneID { get; set; }
        public bool IsDone { get; set; }
        public string IsDoneString { get => IsDone ? "Ja" : "Nein"; }

    }
}
