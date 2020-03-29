namespace SE2.LabManager.Logic.Contracts.DTOs {
    public class Present {
        public int PresentID { get; set; }
        public sbyte WasPresent { get; set; }
        public string Note { get; set; }
        public int Labdate_labdateID { get; set; }
        public int Student_studentID { get; set; }

        public virtual Labdate Labdate { get; set; }
        public virtual Student Student { get; set; }
    }
}
