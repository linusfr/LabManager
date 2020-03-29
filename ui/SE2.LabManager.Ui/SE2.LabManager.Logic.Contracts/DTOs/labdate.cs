using System.Collections.Generic;

namespace SE2.LabManager.Logic.Contracts.DTOs {
    public class Labdate {
        public Labdate() {
            this.Presents = new HashSet<Present>();
        }

        public int LabdateID { get; set; }
        public System.DateTime Date { get; set; }
        public int Lab_labID { get; set; }

        public virtual Lab Lab { get; set; }
        public virtual ICollection<Present> Presents { get; set; }

    }
}
