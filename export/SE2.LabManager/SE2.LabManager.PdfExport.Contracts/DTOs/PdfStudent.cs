using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE2.LabManager.PdfExport.Contracts.DTOs {
    public class PdfStudent {

        public int MatricelNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StudyCourse { get; set; }
        public int Semester { get; set; }
        public string Email { get; set; }
        public string Salutation { get; set; }

    }
}
