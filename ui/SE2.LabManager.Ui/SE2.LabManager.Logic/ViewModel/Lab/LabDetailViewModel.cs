using SE2.LabManager.Data;
using SE2.LabManager.Data.Contracts;
using SE2.LabManager.Logic.Contracts.DTOs;
using SE2.LabManager.Logic.Model.LabDetail;
using SE2.LabManager.PdfExport;
using SE2.LabManager.PdfExport.Contracts.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace SE2.LabManager.Logic {

    /// <summary>
    /// A view model for the lab detail page
    /// </summary>

    public class LabDetailViewModel : BaseViewModel {

        #region Variables

        #region Public Members

        #region Lab Infos
        public int LabID { get; set; }
        public string LabNumber { get; set; }
        public string LabTime { get; set; }
        public string LabWeekday { get; set; }
        public int LabHour { get; set; }
        public int LabMinute { get; set; }
        public int LabDateCount { get; set; }
        public int LabTaskCount { get; set; }
        public int StudentCount { get; set; }
        public ObservableCollection<Lab> Labs { get; set; }
        public ObservableCollection<Student> Students { get; set; }
        public ObservableCollection<LabDetailPresence> Attendances { get; set; }
        public ObservableCollection<LabDetailTask> Tasks { get; set; }
        #endregion

        #region Course Infos
        public string Lecture { get; set; }
        public int CourseID { get; set; }
        #endregion

        #region PopUp Messages
        public string PdfFeedback { get; set; }
        public string InputFeedback { get; set; }
        #endregion

        #region DeleteStudent Infos
        public int DeleteStudentID { get; set; }
        #endregion

        #region Visibilities Of PopUps
        public bool EditLabViewVisible { get; set; }
        public bool AddStudentViewVisible { get; set; }
        public bool EditStudentViewVisibility { get; set; }
        public bool PdfFeedbackViewVisibility { get; set; }
        public bool DeleteStudentViewVisibility { get; set; }
        public bool InputFeedbackViewVisibility { get; set; }
        #endregion

        #region Create Course Collections
        public ObservableCollection<string> Weekdays { get; set; }
        public ObservableCollection<int> TaskNumbers { get; set; }
        public ObservableCollection<int> Days { get; set; }
        public ObservableCollection<string> Months { get; set; }
        public ObservableCollection<int> Years { get; set; }
        public ObservableCollection<int> Hours { get; set; }
        public ObservableCollection<string> Minutes { get; set; }
        public ObservableCollection<string> Salutations { get; set; }
        public ObservableCollection<string> StudyCourses { get; set; }
        #endregion

        #region Edit Lab Popup Infos
        public string SelectedWeekday { get; set; }
        public int SelectedTaskNumber { get; set; }
        public int SelectedStartDay { get; set; }
        public string SelectedStartMonth { get; set; }
        public int SelectedStartYear { get; set; }
        public int SelectedStartHour { get; set; }
        public int SelectedStartMinutes { get; set; }
        public int SelectedLabNumber { get; set; }
        public int SelectedLabDuration { get; set; }
        #endregion

        #region Transfer Student Popup Infos
        public ObservableCollection<int> ExistingLabNumbers { get; set; }
        public ObservableCollection<int> NotUsedLabNumbers { get; set; }
        public int SelectedNewLabNumber { get; set; }
        #endregion

        #region Add Student Popup Infos
        public string SelectedSalutation { get; set; }
        public string SelectedFirstName { get; set; }
        public string SelectedLastName { get; set; }
        public string SelectedMatrNr { get; set; }
        public string SelectedStudyCourse { get; set; }
        public string SelectedEmail { get; set; }
        public int SelectedStudentID { get; set; }
        public int SelectedSemester { get; set; }
        #endregion

        #region Edit Note Popup Infos
        public int SelectedPresentId { get; set; }
        public bool EditNoteViewVisible { get; set; }
        public string SelectedNote { get; set; }
        #endregion

        #endregion

        #region Private Properties
        private readonly LabExporter exporter;
        private readonly DataAccess dataAccess;
        private readonly DataCRUD<lab> context;
        #endregion

        #region Command Declaration
        #region Back Button
        public ICommand GoToCourseDetailCommand { get; set; }
        #endregion

        #region Student
        #region AddStudent
        public ICommand AddStudentCommand { get; set; }
        public ICommand CloseStudentPopupCommand { get; set; }
        public ICommand AddStudentToLabCommand { get; set; }
        #endregion
        #region Update Student
        public ICommand UpdateStudentCommand { get; set; }
        #endregion
        #region Transfer Student
        public ICommand TransferStudentCommand { get; set; }
        #endregion
        #region Edit Student
        public ICommand EditStudentCommand { get; set; }
        public ICommand CloseEditStudentPopupCommand { get; set; }
        #endregion
        #region Delete Student
        public ICommand DeleteStudentCommand { get; set; }
        public ICommand DeleteStudentPopupCommand { get; set; }
        public ICommand CloseDeleteStudentPopupCommand { get; set; }
        #endregion
        #endregion

        #region Lab
        #region EditLab
        public ICommand EditLabCommand { get; set; }
        public ICommand CloseLabPopupCommand { get; set; }
        #endregion
        #region UpdateLab
        public ICommand UpdateLabCommand { get; set; }
        #endregion
        #endregion

        #region  InputFeedback
        public ICommand CloseInputFeedbackCommand { get; set; }
        #endregion
        #region Attendance
        public ICommand ClickedAttendanceCommand { get; set; }
        #endregion
        #region Task
        public ICommand ClickedTaskCommand { get; set; }
        #endregion
        #region Note
        public ICommand EditNoteCommand { get; set; }
        public ICommand UpdateNoteCommand { get; set; }
        public ICommand CloseNotePopupCommand { get; set; }
        #endregion
        #region PDF
        public ICommand PrintStudentInfoCommand { get; set; }
        public ICommand PrintAttendanceCommand { get; set; }
        public ICommand PrintTasksCommand { get; set; }
        public ICommand ClosePdfFeedbackCommand { get; set; }
        #endregion
        #endregion

        #endregion

        #region Constructors

        #region Without Parameter
        public LabDetailViewModel() {
        }
        #endregion

        #region With Lab ID
        public LabDetailViewModel(int labID) {

            #region initialize private properties
            exporter = new LabExporter();
            dataAccess = new DataAccess();
            context = dataAccess.Context;
            #endregion

            #region setProperties
            #region Set Lab Properties
            LabID = labID;

            var lab = dataAccess.GetLabById(labID);

            LabNumber = "Labornummer: " + lab.LabNumber;

            List<Labdate> labDates = new List<Labdate>();
            context.GetLabdatesOfLab(lab.LabID)
                .ForEach(l => labDates.Add(dataAccess.ConvertLabdate(l)));

            if (labDates.Count > 0) {
                Labdate labDate = labDates.First();
                LabWeekday = (labDate.Date.DayOfWeek).ToString();
                LabHour = labDate.Date.Hour;
                LabMinute = (labDate.Date.Minute);
                var minute = (LabMinute.ToString().Equals("0")) ? "00" : LabMinute.ToString();
                LabTime = $"{labDate.Date.DayOfWeek}, {labDate.Date.Hour}:{minute} Uhr";
            } else {
                LabTime = "Es gibt keine Termine";
            }

            LabDateCount = labDates.Count;
            #endregion

            #region Course Info
            CourseID = context.GetCourseOfLab(lab.LabID).courseID;
            Lecture = dataAccess.Context.GetCourseOfLab(lab.LabID).name;
            #endregion

            #region Transfer Student Popup
            ExistingLabNumbers = new ObservableCollection<int>();
            context.GetLabsOfCourse(CourseID).ForEach(l => {
                ExistingLabNumbers.Add(l.labNumber);
            });
            #endregion
            #endregion

            FillAll();

            FillLists();

            #region createCommands
            #region Student
            AddStudentCommand = new RelayCommand(AddStudent);
            PrintStudentInfoCommand = new RelayCommandParameterized((parameter) => PrintStudentInfo());
            AddStudentToLabCommand = new RelayCommand(AddStudentToLab);
            TransferStudentCommand = new RelayCommand(TransferStudent);
            CloseStudentPopupCommand = new RelayCommand(CloseStudentPopup);
            CloseEditStudentPopupCommand = new RelayCommand(CloseEditStudentPopup);
            UpdateStudentCommand = new RelayCommandParameterized((parameter) => UpdateStudent(parameter));
            EditStudentCommand = new RelayCommandParameterized((parameter) => UpdateStudentPopUp(parameter));
            DeleteStudentCommand = new RelayCommandParameterized((parameter) => DeleteStudent(parameter));
            DeleteStudentPopupCommand = new RelayCommandParameterized((parameter) => DeleteStudentPopup(parameter));
            CloseDeleteStudentPopupCommand = new RelayCommand(CloseDeleteStudentPopup);
            #endregion

            #region Attendance
            PrintAttendanceCommand = new RelayCommandParameterized((parameter) => PrintAttendance());
            ClickedAttendanceCommand = new RelayCommandParameterized((parameter) => ClickedAttendance(parameter));
            #endregion

            #region Task
            ClickedTaskCommand = new RelayCommandParameterized((parameter) => ClickedTask(parameter));
            #endregion

            #region Lab
            EditLabCommand = new RelayCommand(OpenLabPopup);
            UpdateLabCommand = new RelayCommand(UpdateLabDetails);
            CloseLabPopupCommand = new RelayCommand(CloseLabPopup);
            #endregion

            #region Course
            GoToCourseDetailCommand = new RelayCommand(GoBack);
            #endregion

            #region Note
            EditNoteCommand = new RelayCommandParameterized((parameter) => OpenEditNotePopup(parameter));
            UpdateNoteCommand = new RelayCommand(UpdateNote);
            CloseNotePopupCommand = new RelayCommand(CloseNotePopup);
            #endregion

            #region PDF
            PrintTasksCommand = new RelayCommandParameterized((parameter) => PrintTasks());
            ClosePdfFeedbackCommand = new RelayCommand(ClosePdfPopup);
            #endregion

            #region InputFeedback
            CloseInputFeedbackCommand = new RelayCommand(InputFeedbackPopup);
            #endregion
            #endregion

            RefreshCourseDetailView();
        }
        #endregion

        #endregion

        #region Methods

        #region Student
        private void TransferStudent() {

            // check if inputs are filled
            if (SelectedNewLabNumber == 0) {

                OpenError("Es muss ein Labor angegeben werden.");

                return;
            }

            // get student and lab
            var s = dataAccess.StudentContext.GetById(SelectedStudentID);
            var l = dataAccess.LabContext.GetById(LabID);

            // remove student from lab
            RemoveStudFromLab(s, l);

            // get new lab
            var newLab = dataAccess.LabContext.GetAll().Where(filterL => filterL.labNumber == SelectedNewLabNumber).First();
            // add stud to new lab
            AddStudToLab(s, newLab);

            // refresh lists
            FillAll();

            // close popup
            CloseEditStudentPopup();
        }
        private void AddStudToLab(student s, lab l) {

            // add stud to new lab
            context.AddStudToLab(s, l);

            // add present for each labdate for student
            context.GetLabdatesOfLab(l.labID)
                    .ForEach(lD => {
                        dataAccess.PresentContext.AddOne(
                            new present {
                                labdate_labdateID = lD.labdateID,
                                note = "",
                                student_studentID = s.studentID,
                                wasPresent = 0
                            });
                    });

            // add TaskDone for each Task for student
            context.GetTasksOfLab(l.labID)
                    .ForEach(t => {

                        dataAccess.TaskDoneContext.AddOne(
                            new taskdone {
                                isDone = 0,
                                student_studentID = s.studentID,
                                task_taskID = t.taskID
                            });

                    });
        }
        private void RemoveStudFromLab(student s, lab l) {
            // delete old presents of student
            context.GetLabdatesOfLab(LabID).ForEach(ld => {
                context.GetPresentsOfLabdate(ld.labdateID)
                    .Where(p => p.student_studentID == s.studentID)
                    .ToList()
                    .ForEach(p => {
                        dataAccess.PresentContext.Remove(p.presentID);
                    });
            });

            // delete old taskDones of student
            context.GetTasksOfLab(LabID).ForEach(t => {
                context.GetTaskDonesOfTask(t.taskID)
                    .Where(tD => tD.student_studentID == s.studentID)
                    .ToList()
                    .ForEach(tD => {
                        dataAccess.TaskDoneContext.Remove(tD.taskDoneID);
                    });
            });

            context.RemoveStudFromLab(s, l);
        }
        #endregion

        #region note
        private void UpdateNote() {

            var p = dataAccess.PresentContext.GetById(SelectedPresentId);
            p.note = SelectedNote;

            dataAccess.PresentContext.Update(p.presentID, p);

            FillAttendances();

            CloseNotePopup();
        }
        private void CloseNotePopup() {
            // Change visibility of lab
            EditNoteViewVisible ^= true;
        }
        #endregion

        #region FillMethods
        private void FillAll() {
            FillStudents();
            FillAttendances();
            FillTasks();
        }
        private void FillStudents() {
            Students = new ObservableCollection<Student>();
            if (context.GetStudentsOfLab(LabID) != null) {
                var students = context.GetStudentsOfLab(LabID);
                students.ForEach((s) => {
                    var stud = dataAccess.ConvertStudent(s);
                    Students.Add(stud);
                });
            }

            // student count
            StudentCount = Students.Count;
        }
        private void FillTasks() {
            Tasks = new ObservableCollection<LabDetailTask>();
            context.GetTasksOfLab(LabID).ForEach(t => {
                context.GetTaskDonesOfTask(t.taskID).ForEach(tD => {

                    var s = context.GetStudentOfTaskDone(tD.taskDoneID);

                    Tasks.Add(new LabDetailTask {

                        // student
                        StudentID = s.studentID,
                        FirstName = s.firstName,
                        LastName = s.lastName,
                        MatricelNumber = s.matricelNumber,

                        // task
                        TaskID = t.taskID,
                        TaskNumber = t.taskNumber,
                        DueDate = t.dueDate,

                        // taskDone
                        TaskDoneID = tD.taskDoneID,
                        IsDone = Convert.ToBoolean(tD.isDone)

                    }); ;
                });
            });
            LabTaskCount = Tasks.Count;
        }
        private void FillAttendances() {

            List<Labdate> labDates = new List<Labdate>();
            context.GetLabdatesOfLab(LabID)
                .ForEach(l => labDates.Add(dataAccess.ConvertLabdate(l)));


            Attendances = new ObservableCollection<LabDetailPresence>();
            labDates.ForEach(lD => {
                context.GetPresentsOfLabdate(lD.LabdateID).ForEach(p => {

                    var s = context.GetStudentOfPresent(p.presentID);

                    Attendances.Add(new LabDetailPresence {

                        // student
                        StudentID = s.studentID,
                        FirstName = s.firstName,
                        LastName = s.lastName,
                        MatricelNumber = s.matricelNumber,

                        // presence
                        PresentID = p.presentID,
                        WasPresent = Convert.ToBoolean(p.wasPresent),
                        Note = p.note,

                        // labDate
                        LabdateID = lD.LabdateID,
                        Date = lD.Date

                    });
                });
            });
            RefreshCourseDetailView();
        }
        public void FillLists() {

            ExistingLabNumbers = new ObservableCollection<int>();
            context.GetLabsOfCourse(CourseID).ForEach(l => {
                ExistingLabNumbers.Add(l.labNumber);
            });

            NotUsedLabNumbers = new ObservableCollection<int>();
            for (var i = 1; i <= 20; i++) {
                NotUsedLabNumbers.Add(i);
            }

            ExistingLabNumbers.ToList().ForEach(labNr => {
                if (NotUsedLabNumbers.Contains(labNr)) {
                    NotUsedLabNumbers.Remove(labNr);
                }
            });

            Weekdays = new ObservableCollection<string> {
                "Montag",
                "Dienstag",
                "Mittwoch",
                "Donnerstag",
                "Freitag"
            };

            // Fill Task Numbers
            TaskNumbers = new ObservableCollection<int>();
            for (int i = 0; i < 15; i++) {
                TaskNumbers.Add(i);
            }

            // Fill Days
            Days = new ObservableCollection<int>();
            for (int i = 1; i < 32; i++) {
                Days.Add(i);
            }

            // Fill Months
            Months = new ObservableCollection<string> {
                "Januar",
                "Februar",
                "März",
                "April",
                "Mai",
                "Juni",
                "Juli",
                "August",
                "September",
                "Oktober",
                "November",
                "Dezember"
            };

            // Fill Years
            Years = new ObservableCollection<int> {
                2020,
                2021,
                2022,
                2023
            };

            // Fill Hours
            Hours = new ObservableCollection<int>();
            for (int i = 8; i < 23; i++) {
                Hours.Add(i);
            }

            // Fill Minutes
            Minutes = new ObservableCollection<string> {
                "00",
                "15",
                "30",
                "45"
            };

            // Fill Salutations
            Salutations = new ObservableCollection<string> {
                "Herr",
                "Frau"
            };

            // Fill StudyCourses
            StudyCourses = new ObservableCollection<string> {
                "AI",
                "MI"
            };
        }
        #endregion

        #region PDF Command Functions

        // Prints all student info to pdf
        // parameter passed in is list of students
        private void PrintStudentInfo() {

            var pdfStudents = new List<PdfStudent>();

            Students.ToList().ForEach(s => {
                pdfStudents.Add(new PdfStudent {
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    MatricelNumber = s.MatricelNumber,
                    Email = s.Email,
                    Salutation = s.Salutation,
                    Semester = s.Semester,
                    StudyCourse = s.StudyCourse
                });
            });

            var course = dataAccess.GetCourseById(CourseID);
            var lab = dataAccess.GetLabById(LabID);

            var lecturers = context.GetLecturerOfCourse(CourseID);
            string lecturerFullName = "Kein Lehrender";
            if (lecturers.Count > 0) {
                var lecturer = lecturers.First();
                lecturerFullName = $"{lecturer.firstName} {lecturer.lastName}";
            }

            // format list for pdf by wanted property
            pdfStudents = pdfStudents.OrderBy(s => s.LastName).ToList();

            var pdfPath = exporter.CreateInformationListForStuds(course.Name, lecturerFullName, lab.LabNumber.ToString(), pdfStudents);

            CheckIfPdfCreated(pdfPath);
        }

        // Prints all student attendances to pdf
        // parameter passed in is list of students and their attendance
        private void PrintAttendance() {

            var pdfPresents = new List<PdfPresence>();

            Attendances.ToList().ForEach(a => {

                pdfPresents.Add(new PdfPresence {
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    MatricelNumber = a.MatricelNumber,
                    Date = a.Date,
                    Note = a.Note,
                    WasPresent = a.WasPresent
                });
            });

            var course = dataAccess.GetCourseById(CourseID);
            var lab = dataAccess.GetLabById(LabID);
            var lecturers = context.GetLecturerOfCourse(CourseID);
            string lecturerFullName = "Kein Lehrender";

            if (lecturers.Count > 0) {
                var lecturer = lecturers.First();
                lecturerFullName = $"{lecturer.firstName} {lecturer.lastName}";
            }

            // format list for pdf by wanted property
            pdfPresents = pdfPresents.OrderBy(p => p.Date).ToList();

            var pdfPath = exporter.CreatePresenceListForStuds(course.Name, lecturerFullName, lab.LabNumber.ToString(), pdfPresents);

            CheckIfPdfCreated(pdfPath);
        }

        // Prints all student tasks to pdf
        // parameter passed in is list of students and their tasks
        private void PrintTasks() {

            var pdfTasks = new List<PdfTask>();

            Tasks.ToList().ForEach(t => {
                pdfTasks.Add(new PdfTask {
                    FirstName = t.FirstName,
                    LastName = t.LastName,
                    MatricelNumber = t.MatricelNumber,
                    DueDate = t.DueDate,
                    IsDone = t.IsDone,
                    TaskNumber = t.TaskNumber
                });
            });

            var course = dataAccess.GetCourseById(CourseID);
            var lab = dataAccess.GetLabById(LabID);

            var lecturers = context.GetLecturerOfCourse(CourseID);
            string lecturerFullName = "Kein Lehrender";
            if (lecturers.Count > 0) {
                var lecturer = lecturers.First();
                lecturerFullName = $"{lecturer.firstName} {lecturer.lastName}";
            }


            // format list for pdf by wanted property
            pdfTasks = pdfTasks.OrderBy(t => t.TaskNumber).ToList();

            var pdfPath = exporter.CreateTaskListForStuds(course.Name, lecturerFullName, lab.LabNumber.ToString(), pdfTasks);

            CheckIfPdfCreated(pdfPath);
        }

        #endregion

        #region PDF Feedback Popup Command Functions

        // opens a popup with a feedback message
        private void CheckIfPdfCreated(string pdfPath) {
            // get the users home directory
            var userDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

            if (pdfPath.Contains(userDirectory)) {
                PdfFeedback = $"Die PDF-Datei wurde erfolgreich erstellt unter: {pdfPath}";
            } else {
                PdfFeedback = "Die PDF-Datei konnte leider nicht erstellt werden.";
            }

            PdfFeedbackViewVisibility ^= true;
        }

        // closes the pdf feedback popup
        private void ClosePdfPopup() {
            PdfFeedbackViewVisibility ^= true;
        }
        #endregion

        #region Edit Lab PopUp Command Functions

        // Open the edit lab details popup 
        private void OpenLabPopup() {
            // Change visibility of popup
            EditLabViewVisible ^= true;

            SelectedLabNumber = dataAccess.GetLabById(LabID).LabNumber;
            SelectedWeekday = TranslateWeekday(LabWeekday);
            SelectedTaskNumber = LabTaskCount;

            //Get Startdate of Lab
            DateTime startDate = dataAccess.Context.GetLabdatesOfLab(LabID).First().date;
            SelectedStartDay = startDate.Day;
            SelectedStartMonth = ConvertMonthToString(startDate.Month);
            SelectedStartYear = startDate.Year;

            SelectedStartHour = startDate.Hour;
            SelectedStartMinutes = startDate.Minute;
            Console.WriteLine(SelectedStartMinutes);
        }

        // Close the edit lab details popup
        private void CloseLabPopup() {
            // Change visibility of lab
            EditLabViewVisible ^= true;
        }

        private void OpenEditNotePopup(object pId) {

            var p = dataAccess.GetPresentById((int)pId);
            SelectedPresentId = p.PresentID;
            SelectedNote = p.Note;

            // Change visibility of lab
            EditNoteViewVisible ^= true;
        }

        // Update lab object in db with user input
        private void UpdateLabDetails() {

            if (SelectedLabNumber != 0
                    && SelectedStartYear != 0
                    && SelectedStartMonth != ""
                    && SelectedStartDay != 0
                    && SelectedLabDuration != 0
                    && SelectedTaskNumber != 0) {

                // update lab

                //how much task in lab
                int taskCount = 0;

                if (SelectedLabDuration < SelectedTaskNumber) {
                    OpenError("Zu Viele Aufgaben für die Anzahl der Wochen.");
                    return;
                }

                taskCount = SelectedTaskNumber;

                lab updatedLab = new lab {
                    labID = LabID,
                    course_courseID = CourseID,
                    labNumber = SelectedLabNumber
                };

                dataAccess.LabContext.Update(LabID, updatedLab);

                //new startDate
                DateTime newStartDate = new DateTime(
                    SelectedStartYear,
                    CourseDetailViewModel.ConvertMonthToInt(SelectedStartMonth),
                    SelectedStartDay,
                    SelectedStartHour,
                    SelectedStartMinutes,
                    0);
                DateTime tmpDate = newStartDate;

                //Delete Old Labdates
                List<labdate> oldLabdates = dataAccess.Context.GetLabdatesOfLab(LabID);
                foreach (var labdate in oldLabdates) {
                    dataAccess.LabDateContext.Remove(labdate.labdateID);
                }

                //Create New Labdates
                for (int i = 0; i < SelectedLabDuration; i++) {

                    //make new labdate
                    labdate labdate = new labdate {
                        date = tmpDate,
                        lab_labID = LabID
                    };
                    //Each Week
                    tmpDate = tmpDate.AddDays(7);

                    //Add labdate
                    dataAccess.LabDateContext.AddOne(labdate);
                }
                var newLabdates = dataAccess.Context.GetLabdatesOfLab(LabID);

                //delete old tasks
                List<task> oldTasks = dataAccess.Context.GetTasksOfLab(LabID);
                foreach (var task in oldTasks) {
                    dataAccess.TaskContext.Remove(task.taskID);
                }

                //Create new Tasks

                for (int i = 0; i < taskCount; i++) {

                    //make new Task
                    task newTask = new task {
                        taskNumber = i + 1,
                        dueDate = newLabdates[i].date,
                        lab_labID = LabID,
                    };

                    //Add task
                    dataAccess.TaskContext.AddOne(newTask);
                }
                var studentInLab = context.GetStudentsOfLab(LabID);
                var allLabdates = context.GetLabdatesOfLab(LabID);
                var alltask = context.GetTasksOfLab(LabID);

                studentInLab.ForEach(student => {
                    alltask.ForEach(task => {
                        taskdone newTask = new taskdone {
                            student_studentID = student.studentID,
                            isDone = 0,
                            task_taskID = task.taskID
                        };
                        dataAccess.TaskDoneContext.AddOne(newTask);
                    });

                    allLabdates.ForEach(dates => {
                        present newPresent = new present {
                            note = "",
                            wasPresent = 0,
                            student_studentID = student.studentID,
                            labdate_labdateID = dates.labdateID
                        };
                        dataAccess.PresentContext.AddOne(newPresent);
                    });
                });

                //Update courseDetailPage
                LabDateCount = newLabdates.Count;
                LabHour = newLabdates.First().date.Hour;
                LabMinute = newLabdates.First().date.Minute;
                LabWeekday = newLabdates.First().date.DayOfWeek.ToString();
                LabNumber = "Labornummer: " + SelectedLabNumber.ToString(); ;
                LabTime = $"{LabWeekday}, {LabHour}:{LabMinute}";

                RefreshCourseDetailView();

                FillAll();

                // Change visibility of lab
                EditLabViewVisible ^= true;
                Console.WriteLine();
            };
        }


        #endregion

        #region Add Student PopUp Command Functions

        // Opens Add Student Popup
        private void AddStudent() {
            // Change visibility of popup
            AddStudentViewVisible ^= true;
        }

        // Add a student to the current Lab
        private void AddStudentToLab() {

            // check if all inputs are filled
            if (SelectedEmail == null
                || SelectedSalutation == null
                || SelectedFirstName == null
                || SelectedLastName == null
                || SelectedMatrNr == null
                || SelectedStudyCourse == null
                || SelectedEmail == null
                ) {
                OpenError("Es müssen alle Felder ausgefüllt sein.");
                return;
            }

            // check if student already exists in database
            var studs = dataAccess.StudentContext.GetAll().Where(stud => stud.matricelNumber == Convert.ToInt32(SelectedMatrNr));

            var s = new student();

            // if student exists already, add the lab to it
            if (studs.ToList().Count > 0) {

                // get already existing student
                s = studs.First();

            } else {

                s = new student {
                    salutation = SelectedSalutation,
                    firstName = SelectedFirstName,
                    lastName = SelectedLastName,
                    matricelNumber = Convert.ToInt32(SelectedMatrNr),
                    studyCourse = SelectedStudyCourse,
                    email = SelectedEmail,
                    semester = SelectedSemester,

                };

                dataAccess.StudentContext.AddOne(s);

            }

            // get current lab
            var l = dataAccess.LabContext.GetById(LabID);

            // add student to the lab
            AddStudToLab(s, l);

            FillAll();

            // Close Popup
            CloseStudentPopup();
        }

        // Command to close the Add Student Popup
        private void CloseStudentPopup() {
            // change visibility of popup
            AddStudentViewVisible ^= true;

            // Empty Input Fields
            SelectedSalutation = "";
            SelectedFirstName = "";
            SelectedLastName = "";
            SelectedMatrNr = "";
            SelectedStudyCourse = "";
            SelectedEmail = "";
            SelectedSemester = 0;
        }

        #endregion

        #region Edit Student Popup Command Functions

        // Opens Edit Student Popup
        private void UpdateStudentPopUp(object parameter) {
            // Change visibility of popup
            EditStudentViewVisibility ^= true;

            SelectedStudentID = (int)parameter;

            //get to edit Student (Converted)
            Student editedStudent = dataAccess.GetStudentById(SelectedStudentID);

            SelectedSalutation = editedStudent.Salutation;
            SelectedFirstName = editedStudent.FirstName;
            SelectedLastName = editedStudent.LastName; ;
            SelectedMatrNr = editedStudent.MatricelNumber.ToString();
            SelectedEmail = editedStudent.Email;
            SelectedStudyCourse = editedStudent.StudyCourse;
            SelectedSemester = editedStudent.Semester;
        }

        // Command to close the Add Student Popup
        private void CloseEditStudentPopup() {
            // change visibility of popup
            EditStudentViewVisibility ^= true;

        }

        // Command to update student in db with user input
        private void UpdateStudent(object parameter) {

            try {
                if (SelectedSalutation == ""
                 || SelectedFirstName == ""
                 || SelectedLastName == ""
                 || SelectedMatrNr == ""
                 || SelectedStudyCourse == ""
                 || SelectedSemester == 0
                 || SelectedEmail == "") {
                    OpenError("Es müssen alle Felder ausgefüllt sein und die Matrikelnummer eine Zahl sein.");
                    return;
                }

                student student = new student {
                    studentID = SelectedStudentID,
                    firstName = SelectedFirstName,
                    lastName = SelectedLastName,
                    salutation = SelectedSalutation,
                    semester = SelectedSemester,
                    matricelNumber = Convert.ToInt32(SelectedMatrNr),
                    email = SelectedEmail,
                    studyCourse = SelectedStudyCourse
                };

                dataAccess.StudentContext.Update(SelectedStudentID, student);

                //Refresh Site
                FillAll();

                CloseEditStudentPopup();

            } catch (FormatException e) {
                OpenError("Es müssen alle Felder ausgefüllt sein und die Matrikelnummer eine Zahl sein.");
            }
        }
        #endregion

        #region Delete Student Popup Command Functions

        // Open the Delete Student Popup
        private void DeleteStudentPopup(object parameter) {
            //Show Student ID in delete student message
            DeleteStudentID = (int)parameter;


            // change visibility of popup
            DeleteStudentViewVisibility ^= true;
        }

        // Close delete student popup
        public void CloseDeleteStudentPopup() {
            // change visibility of popup
            DeleteStudentViewVisibility ^= true;
        }

        // Delete a student from db
        private void DeleteStudent(object parameter) {

            // get student and lab
            var s = dataAccess.StudentContext.GetById(Convert.ToInt32(parameter));
            var l = dataAccess.LabContext.GetById(LabID);

            RemoveStudFromLab(s, l);

            FillAll();

            // Close Popup
            DeleteStudentViewVisibility ^= true;
        }


        #endregion

        #region Datagrid Command Functions

        // When user clicks on a task cell in datagrid, change Cell value
        private void ClickedTask(Object taskObject) {

            // parse object
            var t = (LabDetailTask)taskObject;

            // get present from db
            var tD = dataAccess.TaskDoneContext.GetById(t.TaskDoneID);

            // change present
            tD.isDone = Convert.ToSByte(!t.IsDone);

            // write changes to database
            dataAccess.TaskDoneContext.Update(tD.taskDoneID, tD);

            // refresh Attendance List
            FillTasks();
        }

        // When user clicks on a attendance cell in datagrid, change Cell value
        private void ClickedAttendance(Object attendanceObject) {

            // parse object
            var a = (LabDetailPresence)attendanceObject;

            // get present from db
            var present = dataAccess.PresentContext.GetById(a.PresentID);

            // change present
            present.wasPresent = Convert.ToSByte(!a.WasPresent);

            // write changes to database
            dataAccess.PresentContext.Update(present.presentID, present);

            // refresh Attendance List
            FillAttendances();

        }

        #endregion

        #region Input Feedback Popup Command Functions

        // Opens/closes feedback popup
        public void InputFeedbackPopup() {
            InputFeedbackViewVisibility ^= true;
        }


        #endregion

        #region Helper Functions
        private void FillExistingLabNumbers() {
            ExistingLabNumbers = new ObservableCollection<int>();
            context.GetLabsOfCourse(CourseID).ForEach(l => {
                ExistingLabNumbers.Add(l.labNumber);
            });
        }
        public void RefreshCourseDetailView() {
            // Update List of Labs in view
            var labsFromDB = dataAccess.Context.GetLabsOfCourse(CourseID);

            FillExistingLabNumbers();

            //Fill Labs
            Labs = new ObservableCollection<Lab>();
            labsFromDB.ForEach(l => {
                Labs.Add(dataAccess.ConvertLab(l));
            });
        }

        // translates weekdays from English to German
        public string TranslateWeekday(string dayEN) {
            string dayDE = "";

            switch (dayEN) {
                case "Monday":
                    dayDE = "Montag";
                    break;
                case "Tuesday":
                    dayDE = "Dienstag";
                    break;
                case "Wednesday":
                    dayDE = "Mittwoch";
                    break;
                case "Thursday":
                    dayDE = "Donnerstag";
                    break;
                case "Friday":
                    dayDE = "Freitag";
                    break;
                default:
                    break;
            }

            return dayDE;
        }
        public string ConvertMonthToString(int month) {
            switch (month) {
                case 1:
                    return "Januar";
                case 2:
                    return "Februar";
                case 3:
                    return "März";
                case 4:
                    return "April";
                case 5:
                    return "Mai";
                case 6:
                    return "Juni";
                case 7:
                    return "Juli";
                case 8:
                    return "August";
                case 9:
                    return "September";
                case 10:
                    return "Oktober";
                case 11:
                    return "November";
                case 12:
                    return "Dezember";
                default:
                    return "default";

            }
        }

        #endregion

        #region Open Error PopUp with Message
        private void OpenError(string message) {
            if (InputFeedbackViewVisibility) {
                return;
            }

            InputFeedback = message;

            InputFeedbackViewVisibility = true;
        }
        #endregion

        #region Back Function
        private void GoBack() {
            // Navigate to course detail view, pass courseID as parameter to view model
            IoC.Get<ApplicationViewModel>().GoToPage(ApplicationPage.CourseDetail, new CourseDetailViewModel(CourseID));
        }
        #endregion

        #endregion
    }
}
