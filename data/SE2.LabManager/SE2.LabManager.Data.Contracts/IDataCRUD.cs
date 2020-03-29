using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE2.LabManager.Data.Contracts {
    public interface IDataCRUD<T> where T : class {

        #region Generic methods
        T AddOne(T entity);
        List<T> AddMany(List<T> entityList);
        List<T> GetAll();
        void Update(int id, T entry);
        void Remove(int id);
        void RemoveAll();
        T GetById(int id);
        #endregion

        #region Specific Methods for the Tables in DB

        #region Course
        void AddLecToCourse(lecturer lecturer, course course);
        void AddLecturerToCourse(int lecturerID, int courseID);
        List<lab> GetLabsOfCourse(int id);
        List<lecturer> GetLecturerOfCourse(int id);
        #endregion

        #region Lecturer
        List<course> GetCoursesOfLectruer(int id);
        #endregion

        #region Lab
        lab AddLab(lab lab, int id);
        course GetCourseOfLab(int id);
        List<labdate> GetLabdatesOfLab(int id);
        List<task> GetTasksOfLab(int id);
        List<student> GetStudentsOfLab(int id);
        #endregion

        #region Labdate        
        lab GetLabOfLabdate(int id);
        List<present> GetPresentsOfLabdate(int id);
        labdate AddLabdate(labdate labdate, int id);
        #endregion

        #region Student
        List<lab> GetLabsOfStudent(int id);
        List<taskdone> GetTaskDonesOfStudent(int id);
        List<present> GetPresentsOfStudent(int id);
        void AddStudToLab(student student, lab lab);
        void RemoveStudFromLab(student student, lab lab);
        #endregion

        #region Present
        labdate GetLabdateOfPresent(int id);
        student GetStudentOfPresent(int id);
        #endregion

        #region Task
        lab GetLabOfTask(int id);
        List<taskdone> GetTaskDonesOfTask(int id);
        #endregion

        #region TaskDone
        student GetStudentOfTaskDone(int id);
        task GetTaskOfTaskDone(int id);
        #endregion

        #region Database
        void setIncrementBack(string table, int incNum);
        #endregion

        #endregion

    }

}
