using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE2.LabManager.Logic.Model.LabDetail {
    public class LabDetailPresence {

        // student
        public int StudentID { get; set; }
        public int MatricelNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get => $"{FirstName} {LastName}"; }

        // presence
        public int PresentID { get; set; }
        public bool WasPresent { get; set; }
        public string WasPresentString { get => WasPresent ? "Ja" : "Nein"; }
        public string Note { get; set; }

        // labDate
        public int LabdateID { get; set; }
        public DateTime Date { get; set; }
        public string DateString { get => $"{Date.Hour} Uhr am {Date.Day}.{Date.Month}"; }
    }
}
