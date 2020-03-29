using SE2.LabManager.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace SE2.LabManager.Data {
    public class DataCRUD<T> : IDataCRUD<T> where T : class {
        #region Generic methods
        public T AddOne(T entity) {
            using (se02team06 context = new se02team06()) {
                DbSet<T> dbset = context.Set<T>();
                var some = dbset.Add(entity);
                context.SaveChanges();
                return some;
            }
        }
        public List<T> AddMany(List<T> entityList) {
            using (se02team06 context = new se02team06()) {
                DbSet<T> dbset2 = context.Set<T>();
                foreach (T item in entityList) {
                    dbset2.Add(item);
                }
                context.SaveChanges();
                return entityList;
            }
        }
        public List<T> GetAll() {
            using (se02team06 context = new se02team06()) {
                return context.Set<T>().ToList();
            }

        }
        public void Update(int id, T entry) {
            using (se02team06 context = new se02team06()) {
                try {
                    DbSet<T> dbset = context.Set<T>();
                    var old = dbset.Find(id);
                    context.Entry(old).CurrentValues.SetValues(entry);
                    context.SaveChanges();
                } catch (DbUpdateException e) {
                    Console.WriteLine("object with this id does not exist", e);
                } catch (Exception e) {
                    Console.WriteLine(e);
                }

            }

        }
        public void Remove(int id) {
            using (se02team06 context = new se02team06()) {
                try {
                    DbSet<T> dbset = context.Set<T>();
                    dbset.Remove(dbset.Find(id));
                    context.SaveChanges();
                } catch (ArgumentNullException e) {
                    Console.WriteLine("object with this id does not exist", e);
                } catch (Exception e) {
                    Console.WriteLine(e);
                }
            }
        }
        public void RemoveAll() {
            using (se02team06 context = new se02team06()) {
                try {
                    DbSet<T> dbset = context.Set<T>();
                    List<T> entityList = GetAll();
                    foreach (var item in entityList) {
                        dbset.Attach(item);
                        dbset.Remove(item);
                    }
                    context.SaveChanges();
                } catch (Exception e) {
                    Console.WriteLine("{0}Exception", e);
                }
            }
        }
        public T GetById(int id) {
            using (se02team06 context = new se02team06()) {
                try {
                    DbSet<T> dbset = context.Set<T>();
                    return dbset.Find(id);
                } catch (Exception e) {
                    Console.WriteLine(e);
                    return default(T);
                }
            }
        }
        #endregion

        #region Specific Methods for the Tables in DB

        #region Course
        public List<lab> GetLabsOfCourse(int id) {
            using (se02team06 context = new se02team06()) {
                DbSet<course> dbsetCourse = context.Set<course>();
                course courseTmp = dbsetCourse.Find(id);
                context.Entry(courseTmp).Collection(s => s.labs).Load();
                context.SaveChanges();
                return courseTmp.labs.ToList();
            }
        }
        public List<lecturer> GetLecturerOfCourse(int id) {
            using (se02team06 context = new se02team06()) {
                DbSet<course> dbsetCourse = context.Set<course>();
                course courseTmp = dbsetCourse.Find(id);
                context.Entry(courseTmp).Collection(s => s.lecturers).Load();
                context.SaveChanges();
                return courseTmp.lecturers.ToList();
            }
        }
        public void AddLecToCourse(lecturer lecturer, course course) {
            using (se02team06 context = new se02team06()) {
                DbSet<lecturer> dbsetLecturer = context.Set<lecturer>();
                DbSet<course> dbsetCourse = context.Set<course>();
                var tmpLec = dbsetLecturer.Find(lecturer.lecturerID);
                var tmpCourse = dbsetCourse.Find(course.courseID);
                tmpLec.courses.Add(tmpCourse);
                context.SaveChanges();
            }
        }

        public void AddLecturerToCourse(int lecturerID, int courseID) {
            using (se02team06 context = new se02team06()) {
                DbSet<lecturer> dbsetLecturer = context.Set<lecturer>();
                DbSet<course> dbsetCourse = context.Set<course>();
                var tmpLec = dbsetLecturer.Find(lecturerID);
                var tmpCourse = dbsetCourse.Find(courseID);
                tmpLec.courses.Add(tmpCourse);
                context.SaveChanges();
            }
        }
        #endregion

        #region Lecturer
        public List<course> GetCoursesOfLectruer(int id) {
            using (se02team06 context = new se02team06()) {
                DbSet<lecturer> dbsetLectruer = context.Set<lecturer>();
                lecturer lecturerTmp = dbsetLectruer.Find(id);
                context.Entry(lecturerTmp).Collection(s => s.courses).Load(); // loads Courses collection
                context.SaveChanges();
                return lecturerTmp.courses.ToList();
            }
        }

        #endregion

        #region Lab
        public course GetCourseOfLab(int id) {
            using (se02team06 context = new se02team06()) {
                DbSet<lab> dbsetLab = context.Set<lab>();
                lab labTmp = dbsetLab.Find(id);
                context.Entry(labTmp).Reference(s => s.course).Load();
                context.SaveChanges();
                return labTmp.course;
            }
        }
        public List<labdate> GetLabdatesOfLab(int id) {
            using (se02team06 context = new se02team06()) {
                DbSet<lab> dbsetLab = context.Set<lab>();
                lab labTmp = dbsetLab.Find(id);
                context.Entry(labTmp).Collection(s => s.labdates).Load();
                context.SaveChanges();
                return labTmp.labdates.ToList();
            }
        }
        public List<task> GetTasksOfLab(int id) {
            using (se02team06 context = new se02team06()) {
                DbSet<lab> dbsetLab = context.Set<lab>();
                lab labTmp = dbsetLab.Find(id);
                context.Entry(labTmp).Collection(s => s.tasks).Load();
                context.SaveChanges();
                return labTmp.tasks.ToList();
            }
        }
        public List<student> GetStudentsOfLab(int id) {
            using (se02team06 context = new se02team06()) {
                DbSet<lab> dbsetLab = context.Set<lab>();
                lab labTmp = dbsetLab.Find(id);
                context.Entry(labTmp).Collection(s => s.students).Load();
                context.SaveChanges();
                return labTmp.students.ToList();
            }
        }
        public lab AddLab(lab lab, int id) {
            using (se02team06 context = new se02team06()) {
                DbSet<lab> dbsetLab = context.Set<lab>();
                DbSet<course> dbsetCourse = context.Set<course>();
                lab.course_courseID = id;
                lab tmplab = dbsetLab.Add(lab);
                context.SaveChanges();
                return tmplab;
            }
        }
        #endregion

        #region Labdate
        public lab GetLabOfLabdate(int id) {
            using (se02team06 context = new se02team06()) {
                DbSet<labdate> dbsetLabdate = context.Set<labdate>();
                labdate labdateTmp = dbsetLabdate.Find(id);
                context.Entry(labdateTmp).Reference(s => s.lab).Load();
                context.SaveChanges();
                return labdateTmp.lab;
            }
        }
        public List<present> GetPresentsOfLabdate(int id) {
            using (se02team06 context = new se02team06()) {
                DbSet<labdate> dbsetLabdate = context.Set<labdate>();
                labdate labdateTmp = dbsetLabdate.Find(id);
                context.Entry(labdateTmp).Collection(s => s.presents).Load();
                context.SaveChanges();
                return labdateTmp.presents.ToList();
            }
        }

        public labdate AddLabdate(labdate labdate, int id) {
            using (se02team06 context = new se02team06()) {
                DbSet<labdate> dbsetLabdate = context.Set<labdate>();
                labdate.lab_labID = id;
                labdate tmpLabdate = dbsetLabdate.Add(labdate);
                context.SaveChanges();
                return tmpLabdate;

            }
        }
        #endregion

        #region Student
        public List<lab> GetLabsOfStudent(int id) {
            using (se02team06 context = new se02team06()) {
                DbSet<student> dbset = context.Set<student>();
                var model = dbset.Find(id);
                context.Entry(model).Collection(s => s.labs).Load();
                context.SaveChanges();
                return model.labs.ToList();
            }
        }
        public List<taskdone> GetTaskDonesOfStudent(int id) {
            using (se02team06 context = new se02team06()) {
                DbSet<student> dbset = context.Set<student>();
                var model = dbset.Find(id);
                context.Entry(model).Collection(s => s.taskdones).Load();
                context.SaveChanges();
                return model.taskdones.ToList();
            }
        }
        public List<present> GetPresentsOfStudent(int id) {
            using (se02team06 context = new se02team06()) {
                DbSet<student> dbset = context.Set<student>();
                var model = dbset.Find(id);
                context.Entry(model).Collection(s => s.presents).Load();
                context.SaveChanges();
                return model.presents.ToList();
            }
        }
        public void AddStudToLab(student student, lab lab) {
            using (se02team06 context = new se02team06()) {
                DbSet<student> dbsetStudent = context.Set<student>();
                DbSet<lab> dbsetLab = context.Set<lab>();
                var tmpStud = dbsetStudent.Find(student.studentID);
                var tmpLab = dbsetLab.Find(lab.labID);
                tmpStud.labs.Add(tmpLab);
                context.SaveChanges();
            }
        }
        public void RemoveStudFromLab(student student, lab lab) {
            using (se02team06 context = new se02team06()) {
                DbSet<student> dbsetStudent = context.Set<student>();
                DbSet<lab> dbsetLab = context.Set<lab>();
                var tmpStud = dbsetStudent.Find(student.studentID);
                var tmpLab = dbsetLab.Find(lab.labID);
                tmpStud.labs.Remove(tmpLab);
                tmpLab.students.Remove(tmpStud);
                context.SaveChanges();
            }
        }
        #endregion

        #region Present
        public labdate GetLabdateOfPresent(int id) {
            using (se02team06 context = new se02team06()) {
                DbSet<present> dbsetPresent = context.Set<present>();
                present presentTmp = dbsetPresent.Find(id);
                context.Entry(presentTmp).Reference(s => s.labdate).Load();
                context.SaveChanges();
                return presentTmp.labdate;
            }
        }
        public student GetStudentOfPresent(int id) {
            using (se02team06 context = new se02team06()) {
                DbSet<present> dbsetPresent = context.Set<present>();
                present presentTmp = dbsetPresent.Find(id);
                context.Entry(presentTmp).Reference(s => s.student).Load();
                context.SaveChanges();
                return presentTmp.student;
            }
        }
        #endregion

        #region Task
        public lab GetLabOfTask(int id) {
            using (se02team06 context = new se02team06()) {
                DbSet<task> dbsetTask = context.Set<task>();
                var task = dbsetTask.Find(id);
                context.Entry(task).Reference(s => s.lab).Load();
                context.SaveChanges();
                return task.lab;
            }
        }
        public List<taskdone> GetTaskDonesOfTask(int id) {
            using (se02team06 context = new se02team06()) {
                DbSet<task> dbsetTask = context.Set<task>();
                var task = dbsetTask.Find(id);
                context.Entry(task).Collection(s => s.taskdones).Load();
                context.SaveChanges();
                return task.taskdones.ToList();
            }
        }
        #endregion

        #region TaskDone
        public student GetStudentOfTaskDone(int id) {
            using (se02team06 context = new se02team06()) {
                DbSet<taskdone> dbsetTaskDone = context.Set<taskdone>();
                var tD = dbsetTaskDone.Find(id);
                context.Entry(tD).Reference(s => s.student).Load();
                context.SaveChanges();
                return tD.student;
            }
        }
        public task GetTaskOfTaskDone(int id) {
            using (se02team06 context = new se02team06()) {
                DbSet<taskdone> dbsetTaskDone = context.Set<taskdone>();
                var tD = dbsetTaskDone.Find(id);
                context.Entry(tD).Reference(s => s.task).Load();
                context.SaveChanges();
                return tD.task;
            }
        }
        #endregion

        #region Database
        public void setIncrementBack(string table, int incNum) {
            using (se02team06 context = new se02team06()) {
                context.Database.ExecuteSqlCommand($"ALTER TABLE {table} AUTO_INCREMENT = {incNum}");
            }
        }
        #endregion

        #endregion

    }
}
