using SE2.LabManager.Data.Contracts;
using SE2.LabManager.Logic.Contracts.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace SE2.LabManager.Logic {
    /// <summary>
    ///  A view model for the Course Detail Page
    /// </summary>

    public class CourseDetailViewModel : BaseViewModel {

        #region Public Members
        // Course ID
        public int CourseID { get; set; }

        // Course Name
        public string CourseName { get; set; }

        // Entire lecturer name
        public string Lecturer { get; set; }

        // Lecturer salutation
        public string Salutation { get; set; }

        // Lecturer first name
        public string FirstName { get; set; }

        // lecturer last name
        public string LastName { get; set; }

        // lecturer email
        public string Email { get; set; }

        // Semester
        public string Semester { get; set; }

        // List of all labs belonging to this course
        public ObservableCollection<Lab> Labs { get; set; }

        // True to show the corresponding popup, false to hide it
        public bool EditCourseViewVisible { get; set; }
        public bool AddLabViewVisible { get; set; }
        public bool InputFeedbackViewVisibility { get; set; }

        // TextBlock Inputs in edit course popup
        public string InputCourseName { get; set; }
        public string InputCourseSemester { get; set; }
        public string InputLecturerFirstName { get; set; }
        public string InputLecturerLastName { get; set; }
        public string InputLecturerSalutation { get; set; }
        public string InputLecturerEmail { get; set; }

        // Collections for comboboxes in popup
        public ObservableCollection<string> Weekdays { get; set; }
        public ObservableCollection<int> TaskNumbers { get; set; }
        public ObservableCollection<int> Days { get; set; }
        public ObservableCollection<string> Months { get; set; }
        public ObservableCollection<int> Years { get; set; }
        public ObservableCollection<int> Hours { get; set; }
        public ObservableCollection<string> Minutes { get; set; }
        public ObservableCollection<int> LabNumbers { get; set; }
        public ObservableCollection<int> ExistingLabNumbers { get; set; }
        public ObservableCollection<int> NotUsedLabNumbers { get; set; }

        // Inputs in add lab popup
        public int SelectedTaskNumber { get; set; }
        public string SelectedWeekday { get; set; }
        public int SelectedLabNumber { get; set; }
        public int SelectedLabDuration { get; set; }

        private IDataCRUD<lab> context;

        #region startTime
        public int SelectedStartDay { get; set; }
        public string SelectedStartMonth { get; set; }
        public int SelectedStartMonthInt { get => ConvertMonthToInt(SelectedStartMonth); }
        public int SelectedStartYear { get; set; }
        public int SelectedStartHour { get; set; }
        public int SelectedStartMinutes { get; set; }
        #endregion

        #region endTime
        public int SelectedEndDay { get; set; }
        public string SelectedEndMonth { get; set; }
        public int SelectedEndMonthInt { get => ConvertMonthToInt(SelectedEndMonth); }
        public int SelectedEndYear { get; set; }
        public int SelectedEndHour { get; set; }
        public int SelectedEndMinutes { get; set; }
        #endregion

        // Input Feedback Message
        public string InputFeedback { get; set; }
        #endregion

        #region Commands
        // command to open the "Create new Lab" view
        public ICommand GoToCreateLabCommand { get; set; }

        // command to delete a single lab
        public ICommand DeleteLabCommand { get; set; }

        // command to open the detailed view of a single course
        public ICommand GoToLabCommand { get; set; }

        // command to go back to Course Overview Page
        public ICommand GoToCourseOverviewCommand { get; set; }

        // command to open the popup to edit course details
        public ICommand EditCourseCommand { get; set; }

        // Command to update the course details
        public ICommand UpdateCourseCommand { get; set; }

        // Command to create a new lab
        public ICommand CreateLabCommand { get; set; }

        // Commands to close the popups
        public ICommand ClosePopupCommand { get; set; }
        public ICommand CloseLabPopupCommand { get; set; }
        public ICommand CloseInputFeedbackCommand { get; set; }
        #endregion

        //Communicate with DB
        public DataAccess dataAccess = new DataAccess();

        public List<lecturer> lecturerList = new List<lecturer>();

        #region Constructor
        // Default constructor
        public CourseDetailViewModel() {
        }

        // Constructor with CourseId as parameter
        public CourseDetailViewModel(int courseID) {

            context = dataAccess.Context;

            // Create Commands
            GoToCreateLabCommand = new RelayCommandParameterized((parameter) => GoToCreateLab(parameter));
            DeleteLabCommand = new RelayCommandParameterized((parameter) => DeleteLab(parameter));
            GoToLabCommand = new RelayCommandParameterized((parameter) => GoToLab(parameter));
            GoToCourseOverviewCommand = new RelayCommand(GoBack);
            EditCourseCommand = new RelayCommand(EditCourse);
            UpdateCourseCommand = new RelayCommand(UpdateCourse);
            ClosePopupCommand = new RelayCommand(ClosePopup);
            CloseLabPopupCommand = new RelayCommand(CloseLabPopup);
            CreateLabCommand = new RelayCommand(CreateLab);
            CloseInputFeedbackCommand = new RelayCommand(InputFeedbackPopup);

            // Set CourseID
            CourseID = courseID;

            //get DataAccess to communicate with DB
            var course = dataAccess.GetCourseById(courseID);

            // Set Course Name
            CourseName = course.Name;

            // Set Semester
            Semester = course.Semester;

            // Set Lecturer        
            lecturerList = dataAccess.Context.GetLecturerOfCourse(CourseID);

            if (lecturerList.Count != 0) {
                Salutation = lecturerList.First().salutation;
                FirstName = lecturerList.First().firstName;
                LastName = lecturerList.First().lastName;
                Email = lecturerList.First().email;
            } else {
                Salutation = "keine lehrende Person";
                FirstName = " ";
                LastName = " ";
                Email = " ";
            }

            Lecturer = $"{Salutation} {FirstName} {LastName}";

            // Fill List of Labs        
            Labs = new ObservableCollection<Lab>();
            dataAccess.Context.GetLabsOfCourse(CourseID).ForEach(lab => Labs.Add(dataAccess.ConvertLab(lab)));

            FillLists();
        }
        #endregion

        #region Command Functions
        //Opens a view to create a new lab for this course
        private void GoToCreateLab(object parameter) {
            // Open Popup and uodate detail page
            RefreshCourseDetailView();
            AddLabViewVisible ^= true;
        }

        // Delete a specific lab and all its contents
        private void DeleteLab(object parameter) {
            // Delete Lab in DB with id
            dataAccess.LabContext.Remove((int)parameter);

            //refresh CourseDetailView
            RefreshCourseDetailView();
        }

        // Navigates to lab detailed view of selected lab by LabID
        private void GoToLab(object parameter) {
            IoC.Get<ApplicationViewModel>().GoToPage(ApplicationPage.LabDetail, new LabDetailViewModel((int)parameter));
        }

        // Attempts to return to Course Overview Page
        private void GoBack() {
            IoC.Get<ApplicationViewModel>().GoToPage(ApplicationPage.CourseOverview);
        }

        // Opens popup to edit course details
        private void EditCourse() {
            // Open Popup
            EditCourseViewVisible ^= true;

            // pre-fill input fields with current course data
            InputCourseName = CourseName;
            InputCourseSemester = Semester;

            // pre-fill input lecturer with current data
            InputLecturerFirstName = FirstName;
            InputLecturerLastName = LastName;
            InputLecturerSalutation = Salutation;
            InputLecturerEmail = Email;
        }

        // Update the current course details from user input
        public void UpdateCourse() {
            try {
                // at first check if all inputs filled
                if (InputLecturerFirstName == "" || InputLecturerLastName == "" || InputLecturerEmail == "" || InputLecturerSalutation == "" ||
                    InputCourseName == "" || InputCourseSemester == "") {
                    throw new FormatException();
                }

                //Updated course
                course updatedCourse = new course {
                    courseID = CourseID,
                    semester = InputCourseSemester,
                    name = InputCourseName
                };
                dataAccess.CourseContext.Update(CourseID, updatedCourse);

                //Updated Lecturer
                lecturer updateedLecturer = new lecturer {
                    lecturerID = lecturerList.First().lecturerID,
                    salutation = InputLecturerSalutation,
                    firstName = InputLecturerFirstName,
                    lastName = InputLecturerLastName,
                    email = InputLecturerEmail
                };
                dataAccess.LecturerContext.Update(lecturerList.First().lecturerID, updateedLecturer);

                // Update course in courseDetailPage
                CourseName = InputCourseName;
                Semester = InputCourseSemester;

                // Update lecturer in courseDetailPage
                Lecturer = $"{InputLecturerSalutation} {InputLecturerFirstName} {InputLecturerLastName}";
                FirstName = InputLecturerFirstName;
                LastName = InputLecturerLastName;
                Salutation = InputLecturerSalutation;
                Email = InputLecturerEmail;

                // Then close popup
                ClosePopup();
            } catch (FormatException e) {
                InputFeedback = $"Check your course details. Course could not be updated.";
                InputFeedbackPopup();
            }
        }

        // Closes the popup and reloads the course overview page
        public void ClosePopup() {
            EditCourseViewVisible ^= true;

        }

        // Create a new lab from user input
        public void CreateLab() {
            try {
                // at first check if all inputs filled
                if (SelectedLabNumber == 0 || SelectedLabDuration == 0 || SelectedStartDay == 0 || SelectedStartMonth == null ||
                    SelectedStartYear == 0 || SelectedStartHour == 0 || SelectedStartMinutes == 0) {
                    throw new FormatException();
                }

                //create new Lab
                lab lab = new lab {
                    labNumber = SelectedLabNumber,
                    course_courseID = CourseID
                };

                // lab to get the id for the kabdates
                if (lab.labNumber != 0
                    && SelectedStartYear != 0
                    && SelectedStartMonthInt != 0
                    && SelectedStartDay != 0
                    && SelectedLabDuration != 0
                    && SelectedTaskNumber != 0) {

                    lab entrieLab = dataAccess.LabContext.AddOne(lab);
                    // create labdates
                    DateTime startDate = new DateTime(SelectedStartYear, SelectedStartMonthInt, SelectedStartDay, SelectedStartHour, SelectedStartMinutes, 0);
                    DateTime tmpDate = startDate;

                    for (int i = 0; i < SelectedLabDuration; i++) {

                        // make new labdate
                        labdate labdate = new labdate {
                            date = tmpDate,
                            lab_labID = entrieLab.labID
                        };
                        // Each Week
                        tmpDate = tmpDate.AddDays(7);

                        // Add labdate
                        dataAccess.LabDateContext.AddOne(labdate);
                        Console.WriteLine($"date{i} : {labdate.date}");
                    }

                    // create task
                    for (int i = 1; i <= SelectedTaskNumber; i++) {
                        task newTask = new task {
                            taskNumber = i,
                            lab_labID = lab.labID
                        };
                        dataAccess.TaskContext.AddOne(newTask);
                    }
                } else {
                    Console.Write("missing entry");
                }

                //refreshCourseDetailView View
                RefreshCourseDetailView();
                FillLists();
                AddLabViewVisible ^= true;
            } catch (FormatException e) {
                InputFeedback = $"Check your lab details. Lab could not be created.";
                InputFeedbackPopup();
                Console.WriteLine("missing entry " + e);
            }
        }
        #endregion

        #region Input Feedback Popup Command Functions
        // Opens/closes feedback popup
        public void InputFeedbackPopup() {
            InputFeedbackViewVisibility ^= true;
        }
        #endregion

        #region Helper Functions
        // Closes the add lab popup and reloads the course detail page
        public void CloseLabPopup() {
            // change visibility of the popup
            AddLabViewVisible ^= true;
        }

        // Refresh Page
        public void RefreshCourseDetailView() {
            // Update List of Labs in view
            var labsFromDB = dataAccess.Context.GetLabsOfCourse(CourseID);

            // reset Lists
            Labs = new ObservableCollection<Lab>();
            LabNumbers = new ObservableCollection<int>();

            // Fill Labs
            labsFromDB.ForEach(l => {
                Labs.Add(dataAccess.ConvertLab(l));
            });

            FillLists();
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
        }

        // static - use in EditLabView update details
        public static int ConvertMonthToInt(string month) {
            switch (month.ToLower()) {
                case "januar":
                    return 1;
                case "februar":
                    return 2;
                case "märz":
                    return 3;
                case "april":
                    return 4;
                case "mai":
                    return 5;
                case "juni":
                    return 6;
                case "juli":
                    return 7;
                case "august":
                    return 8;
                case "september":
                    return 9;
                case "oktober":
                    return 10;
                case "november":
                    return 11;
                case "dezember":
                    return 12;
                default:
                    return 0;
            }
        }
        #endregion
    }
}
