using SE2.LabManager.Data;
using SE2.LabManager.Data.Contracts;
using SE2.LabManager.Logic.Contracts.DTOs;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SE2.LabManager.Logic {
    public class DataAccess {
        public DataCRUD<lab> Context = new DataCRUD<lab>();
        public DataCRUD<course> CourseContext = new DataCRUD<course>();
        public DataCRUD<lecturer> LecturerContext = new DataCRUD<lecturer>();
        public DataCRUD<lab> LabContext = new DataCRUD<lab>();
        public DataCRUD<labdate> LabDateContext = new DataCRUD<labdate>();
        public DataCRUD<student> StudentContext = new DataCRUD<student>();
        public DataCRUD<present> PresentContext = new DataCRUD<present>();
        public DataCRUD<task> TaskContext = new DataCRUD<task>();
        public DataCRUD<taskdone> TaskDoneContext = new DataCRUD<taskdone>();

        #region dataAccess
        public Lecturer GetLecturerById(int id) {
            return ConvertLecturer(LecturerContext.GetById(id));
        }
        public Course GetCourseById(int id) {
            return ConvertCourse(CourseContext.GetById(id));
        }
        public Lab GetLabById(int id) {
            return ConvertLab(LabContext.GetById(id));
        }
        public Labdate GetLabDateById(int id) {
            return ConvertLabdate(LabDateContext.GetById(id));
        }
        public Student GetStudentById(int id) {
            return ConvertStudent(StudentContext.GetById(id));
        }
        public Present GetPresentById(int id) {
            return ConvertPresent(PresentContext.GetById(id));
        }
        public Tasks GetTaskById(int id) {
            return ConvertTasks(TaskContext.GetById(id));
        }
        public TaskDone GetTaskDoneById(int id) {
            return ConvertTaskDone(TaskDoneContext.GetById(id));
        }
        public ObservableCollection<Course> GetAllCourses() {
            ObservableCollection<Course> AllCourses = new ObservableCollection<Course>();
            List<course> tempList = CourseContext.GetAll();
            foreach (course course in tempList) {
                AllCourses.Add(ConvertCourse(course));
            }
            return AllCourses;
        }

        public void deleteCourse(int courseID) {
            CourseContext.Remove(courseID);
        }
        public void addCourse(course course)
        {
            CourseContext.AddOne(course);
        }
        #endregion

        #region Converter
        public Course ConvertCourse(course c) {
            Course course = new Course {
                CourseID = c.courseID,
                Name = c.name,
                Semester = c.semester,
                Labs = new List<Lab>(),
                Lecturers = new List<Lecturer>()
            };

            return course;
        }

        public Lab ConvertLab(lab l) {

            Lab labor = new Lab {
                LabID = l.labID,
                LabNumber = l.labNumber,
                Course_courseID = l.course_courseID,
                Course = new Course(),
                Labdates = new List<Labdate>(),
                Tasks = new List<Tasks>(),
                Students = new List<Student>(),

                //new Property for UI
                LabDateCount = Context.GetLabdatesOfLab(l.labID).Count,
                StudentCount = Context.GetStudentsOfLab(l.labID).Count
            };

            return labor;
        }

        public Student ConvertStudent(student s) {
            Student student = new Student {
                StudentID = s.studentID,
                MatricelNumber = s.matricelNumber,
                FirstName = s.firstName,
                LastName = s.lastName,
                StudyCourse = s.studyCourse,
                Semester = s.semester,
                Email = s.email,
                Salutation = s.salutation,
                Presents = new List<Present>(),
                TaskDones = new List<TaskDone>(),
                Labs = new List<Lab>()
            };

            return student;
        }

        public Present ConvertPresent(present p) {
            Present present = new Present {
                PresentID = p.presentID,
                WasPresent = p.wasPresent,
                Note = p.note,
                Labdate_labdateID = p.labdate_labdateID,
                Student_studentID = p.student_studentID,
                Student = new Student(),
                Labdate = new Labdate()
            };

            return present;
        }

        public Labdate ConvertLabdate(labdate l) {
            Labdate labdate = new Labdate {
                LabdateID = l.labdateID,
                Date = l.date,
                Lab_labID = l.lab_labID,
                Lab = new Lab(),
                Presents = new List<Present>()
            };

            return labdate;
        }

        public Lecturer ConvertLecturer(lecturer l) {
            Lecturer lecturer = new Lecturer {
                LecturerID = l.lecturerID,
                FirstName = l.firstName,
                LastName = l.lastName,
                Salutation = l.salutation,
                Email = l.email,
                Courses = new List<Course>()
            };

            return lecturer;
        }

        public Tasks ConvertTasks(task t) {
            Tasks tasks = new Tasks {
                TaskID = t.taskID,
                TaskNumber = t.taskNumber,
                DueDate = t.dueDate,
                Lab_labID = t.lab_labID,
                Lab = new Lab(),
                TaskDones = new List<TaskDone>()
            };

            return tasks;
        }

        public TaskDone ConvertTaskDone(taskdone t) {
            TaskDone taskDone = new TaskDone {
                TaskDoneID = t.taskDoneID,
                IsDone = t.isDone,
                Student_studentID = t.student_studentID,
                Task_taskID = t.task_taskID,
                Student = new Student(),
                Task = new Tasks()
            };

            return taskDone;
        }

        #endregion
    }
}
