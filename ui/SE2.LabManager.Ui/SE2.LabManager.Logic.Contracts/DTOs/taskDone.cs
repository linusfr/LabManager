namespace SE2.LabManager.Logic.Contracts.DTOs {
    public class TaskDone {
        public int TaskDoneID { get; set; }
        public sbyte IsDone { get; set; }
        public int Student_studentID { get; set; }
        public int Task_taskID { get; set; }

        public virtual Student Student { get; set; }
        public virtual Tasks Task { get; set; }
    }
}
