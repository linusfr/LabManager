using SE2.LabManager.Data;
using SE2.LabManager.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SE2.LabManager.Logic {
    public class DataBaseTestData {
        #region Constructor
        public DataBaseTestData() {
            FillDataBase();
        }
        #endregion

        #region DBset
        DataCRUD<lecturer> lecDbSet = new DataCRUD<lecturer>();
        DataCRUD<course> courseDbSet = new DataCRUD<course>();
        DataCRUD<lab> labsDbset = new DataCRUD<lab>();
        DataCRUD<labdate> labdatesDbSet = new DataCRUD<labdate>();
        DataCRUD<student> studDbSet = new DataCRUD<student>();
        DataCRUD<present> presentDbSet = new DataCRUD<present>();
        DataCRUD<task> taskDbSet = new DataCRUD<task>();
        DataCRUD<taskdone> taskDoneDbSet = new DataCRUD<taskdone>();
        #endregion

        #region List of DB Entries
        //Course
        List<string> courseNames = new List<string> { "Software Engineering", "Theoretische Informatik", "Deklarative Software Technologien" };
        List<string> semester = new List<string> { "WS 19/20", "SS 20", "WS20/21" };
        //Lab
        List<int> labNumber = new List<int> { 1, 2, 3, 4 };
        List<int> labDateCount = new List<int> { 1, 2, 3 };
        //Lecturer
        List<string> salutationLecturer = new List<string> { "M.Sc.", "Dr.", "Prof. Dr." };
        List<string> firstNameLecturer = new List<string> { "Ben", "Parisa", "Jan" };
        List<string> lastNameLecturer = new List<string> { "Schulz", "Sadeghi", "Christiansen" };
        List<string> emailLecturer = new List<string> { "benjamin.schulz@hs-flensburg.de", "sadeghi@hs-flensburg.de", "jan.christiansen@hs-flensburg.de" };
        //Student
        List<string> salutationStudent = new List<string> { "Herr", "Frau", "Herr", "Frau" };
        List<string> firstNameStudent = new List<string> { "Thomas", "Catharine", "Linus", "Kathrin" };
        List<string> lastNameStudent = new List<string> { "Burger", "Craemer", "Frotscher", "Dürkop" };
        List<int> matricelnumberStudent = new List<int> { 630123, 630123, 630123, 630123 };
        List<string> emailStudent = new List<string> { "thomas.burger@stud.hs-flensburg.de", "catharine.kraemer@stud.hs-flensburg.de", "linus.frotscher@stud.hs-flensburg.de", "kathrin.dürkop@stud.hs-flensburg.de" };
        List<string> studyCourseStudent = new List<string> { "AI", "AI", "AI", "AI" };
        List<int> semesterStudent = new List<int> { 19, 19, 19, 19 };
        #endregion

        #region Fill and Clear Methods
        private void FillDataBase() {
            ClearDB();
            FillCourse();
            FillLecturer();
            FillLabs();
            fillLabdates();
            FillStudent();
            FillPresent();
            FillTask();
            FillTaskDone();
        }
        private void FillCourse() {
            courseNames.ForEach(courseName => {
                course newCourse = new course {
                    name = courseName,
                    semester = semester[courseNames.IndexOf(courseName)]
                };
                courseDbSet.AddOne(newCourse);
            });
        }

        private void FillLecturer() {
            firstNameLecturer.ForEach(firstname => {
                lecturer tmplec = new lecturer {
                    salutation = salutationLecturer[firstNameLecturer.IndexOf(firstname)],
                    firstName = firstname,
                    lastName = lastNameLecturer[firstNameLecturer.IndexOf(firstname)],
                    email = emailLecturer[firstNameLecturer.IndexOf(firstname)]
                };
                lecDbSet.AddLecToCourse(lecDbSet.AddOne(tmplec), courseDbSet.GetById(firstNameLecturer.IndexOf(firstname) + 1));
            });
        }
        private void FillLabs() {
            courseNames.ForEach(course => {
                labNumber.ForEach(lab => {
                    lab tmpLab = new lab {
                        labNumber = labNumber[labNumber.IndexOf(lab)]
                    };
                    labsDbset.AddLab(tmpLab, courseNames.IndexOf(course) + 1);
                });
            });
        }
        private void fillLabdates() {
            List<lab> labsList = labsDbset.GetAll();
            labsList.ForEach(labs => {
                DateTime tmpDate = new DateTime(2020, 05, 15, 15, 00, 00);
                labDateCount.ForEach(labDate => {
                    labdate tmpLabdate = new labdate {
                        date = tmpDate
                    };
                    labsDbset.AddLabdate(tmpLabdate, labsList.IndexOf(labs) + 1);
                    tmpDate = tmpDate.AddDays(7);
                });
            });
        }
        private void FillStudent() {
            firstNameStudent.ForEach(firstName => {
                var index = firstNameStudent.IndexOf(firstName);
                student tmpStud = new student {
                    salutation = salutationStudent[index],
                    firstName = firstName,
                    lastName = lastNameStudent[index],
                    email = emailStudent[index],
                    matricelNumber = matricelnumberStudent[index],
                    studyCourse = studyCourseStudent[index],
                    semester = semesterStudent[index]
                };
                studDbSet.AddOne(tmpStud);
                if (index % 2 == 0) {
                    studDbSet.AddStudToLab(tmpStud, labsDbset.GetById(1));
                } else {
                    studDbSet.AddStudToLab(tmpStud, labsDbset.GetById(2));
                }
            });
        }
        private void FillPresent() {
            List<student> studentList = studDbSet.GetAll();
            List<labdate> studsLabdatesOfLab = new List<labdate>();
            List<labdate> dates = labdatesDbSet.GetAll();
            studentList.ForEach(student => {
                var index = studentList.IndexOf(student);
                labsDbset.GetLabsOfStudent(student.studentID).ForEach(lab => {
                    studsLabdatesOfLab = dates.Where(l => l.lab_labID == lab.labID).ToList();
                });
                foreach (var i in studsLabdatesOfLab) {
                    present pre = new present {
                        labdate_labdateID = i.labdateID,
                        student_studentID = studentList[index].studentID,
                        note = "war hier",
                        wasPresent = 0
                    };
                    presentDbSet.AddOne(pre);
                }
            });
        }
        private void FillTask() {
            List<lab> labs = labsDbset.GetAll();
            List<labdate> labDates = labdatesDbSet.GetAll();

            labs.ForEach(lab => {
                var indexLab = labs.IndexOf(lab);
                var i = 1;
                labDates.Where(s => s.lab_labID == indexLab).ToList().ForEach(date => {
                    var indexLabDate = labDates.IndexOf(date);
                    task tmpTask = new task {
                        lab_labID = indexLab,
                        taskNumber = i,
                        dueDate = date.date
                    };
                    i++;
                    taskDbSet.AddOne(tmpTask);
                });
            });
        }
        private void FillTaskDone() {
            List<task> alltasksList = taskDbSet.GetAll();
            List<student> studentsList = studDbSet.GetAll();
            studentsList.ForEach(student => {
                studDbSet.GetLabsOfStudent(student.studentID).ForEach(lab => {
                    alltasksList.Where(s => s.lab_labID == lab.labID).ToList().ForEach(task => {
                        taskdone taskDone = new taskdone {
                            isDone = 1,
                            student_studentID = student.studentID,
                            task_taskID = task.taskID
                        };
                        taskDoneDbSet.AddOne(taskDone);
                    });
                });
            });
        }

        private void ClearDB() {
            courseDbSet.RemoveAll();
            courseDbSet.setIncrementBack("course", 1);

            labsDbset.RemoveAll();
            labsDbset.setIncrementBack("lab", 1);

            lecDbSet.RemoveAll();
            lecDbSet.setIncrementBack("lecturer", 1);

            taskDoneDbSet.RemoveAll();
            taskDoneDbSet.setIncrementBack("taskDone", 1);

            taskDbSet.RemoveAll();
            taskDbSet.setIncrementBack("task", 1);

            presentDbSet.RemoveAll();
            presentDbSet.setIncrementBack("present", 1);

            studDbSet.RemoveAll();
            studDbSet.setIncrementBack("student", 1);

            labdatesDbSet.RemoveAll();
            labdatesDbSet.setIncrementBack("labdate", 1);
        }
        #endregion

    }
}
