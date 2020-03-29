using SE2.LabManager.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;


namespace SE2.LabManager.Logic {
    /// <summary>
    /// A view model for the Course Overview Page
    /// </summary>
    public class CourseOverviewViewModel : BaseViewModel {

        #region Public Properties
        // List of all courses
        public ObservableCollection<Contracts.DTOs.Course> Courses { get; set; }
        readonly DataAccess access = new DataAccess();

        // True to show the corresponding popup, false to hide it
        public bool CreateCourseViewVisible { get; set; }
        public bool InputFeedbackViewVisibility { get; set; }

        // TextBlock Inputs in create course popup
        public string InputCourseName { get; set; }
        public string InputCourseSemester { get; set; }
        public string InputLecturerFirstName { get; set; }
        public string InputLecturerLastName { get; set; }
        public string InputLecturerSalutation { get; set; }
        public string InputLecturerEmail { get; set; }

        // Input Feedback Message
        public string InputFeedback { get; set; }

        #endregion

        #region Commands
        // command to open the detailed view of a single course
        public ICommand GoToCourseCommand { get; set; }

        // command to open the "Create new Course" view
        public ICommand GoToCreateCourseCommand { get; set; }

        // command to delete a single course
        public ICommand DeleteCourseCommand { get; set; }

        // Command to create a new course in popup
        public ICommand CreateCourseCommand { get; set; }

        // Command to close the popup
        public ICommand ClosePopupCommand { get; set; }
        public ICommand CloseInputFeedbackCommand { get; set; }
        #endregion

        #region Constructor
        // Default constructor
        public CourseOverviewViewModel() {
            // Create Commands
            GoToCourseCommand = new RelayCommandParameterized((parameter) => GoToCourse(parameter));
            GoToCreateCourseCommand = new RelayCommand(GoToCreateCourse);
            DeleteCourseCommand = new RelayCommandParameterized((parameter) => DeleteCourse(parameter));
            CreateCourseCommand = new RelayCommand(CreateCourse);
            ClosePopupCommand = new RelayCommand(ClosePopup);
            CloseInputFeedbackCommand = new RelayCommand(InputFeedbackPopup);

            // fill courseOverview with courses
            Courses = access.GetAllCourses();
        }
        #endregion

        #region Command Functions
        // Navigates to detailed view of selected course
        private void GoToCourse(object parameter) {
            // Navigate to course detail view, pass courseID as parameter to view model
            IoC.Get<ApplicationViewModel>().GoToPage(ApplicationPage.CourseDetail, new CourseDetailViewModel((int)parameter));
        }

        // Opens a view to create a new course
        private void GoToCreateCourse() {
            // Toggle View visibility
            CreateCourseViewVisible ^= true;
        }

        // Deletes a specific course and all its contents
        private void DeleteCourse(object parameter) {
            Console.WriteLine("Delete course: " + parameter);
            try {
                access.deleteCourse((int)parameter);

                // update course overview
                Courses = access.GetAllCourses();
            } catch (FormatException e) {
                InputFeedback = $"Course can't be deleted.";
                InputFeedbackPopup();
                Console.WriteLine(e);
            }

        }

        // Creates a new course from user input
        public void CreateCourse() {
            try {
                // at first check if all inputs filled
                if (InputLecturerFirstName == null || InputLecturerLastName == null || InputLecturerEmail == null || InputLecturerSalutation == null ||
                    InputCourseName == null || InputCourseSemester == null) {
                    throw new FormatException();
                }
                // create a lecturer for the new course
                lecturer newLecturer = new lecturer();

                // add a new course to the db
                course newCourse = new course {
                    semester = InputCourseSemester,
                    name = InputCourseName,
                };
                access.addCourse(newCourse);

                // check if lecturer already exist
                List<lecturer> listOfLecturers = new List<lecturer>();
                listOfLecturers = access.LecturerContext.GetAll().Where(s => s.firstName == InputLecturerFirstName && s.lastName == InputLecturerLastName).ToList();

                if (listOfLecturers != null) {
                    // if no lecturer exist create a new one
                    newLecturer = new lecturer {
                        email = InputLecturerEmail,
                        firstName = InputLecturerFirstName,
                        lastName = InputLecturerLastName,
                        salutation = InputLecturerSalutation
                    };
                    access.LecturerContext.AddOne(newLecturer);
                    access.LecturerContext.AddLecToCourse(newLecturer, newCourse);
                } else {
                    // if lecturer exist fill the fields without add a new lecturer to the db
                    foreach (lecturer existingLecturer in listOfLecturers) {
                        newLecturer.firstName = existingLecturer.firstName;
                        newLecturer.lastName = existingLecturer.lastName;
                        newLecturer.email = existingLecturer.email;
                        newLecturer.salutation = existingLecturer.salutation;
                    }
                    access.LecturerContext.AddOne(newLecturer);
                    access.LecturerContext.AddLecToCourse(newLecturer, newCourse);
                }

                // update courseOverview
                Courses = access.GetAllCourses();

                // close popup
                CreateCourseViewVisible = false;
            } catch (FormatException e) {
                InputFeedback = "Der Kurs konnte nicht erstellt werden. Bitte überprüfen Sie die Eingaben.";
                InputFeedbackPopup();
                Console.WriteLine(e);
            }
        }

        // Closes the popup and reloads the course overview page
        public void ClosePopup() {

            // Empty all input fields
            InputCourseName = "";
            InputCourseSemester = "";
            InputLecturerFirstName = "";
            InputLecturerLastName = "";
            InputLecturerSalutation = "";
            InputLecturerEmail = "";

            // change visibility of the popup
            CreateCourseViewVisible ^= true;
        }
        #endregion

        #region Input Feedback Popup Command Functions
        // Opens/closes feedback popup
        public void InputFeedbackPopup() {
            InputFeedbackViewVisibility ^= true;
        }
        #endregion

    }
}
