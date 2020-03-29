
namespace SE2.LabManager.Logic {
    /// <summary>
    /// The application state as a view model
    /// </summary>
    public class ApplicationViewModel : BaseViewModel {

        // The current page of the application
        // Initially set to CourseOverview as starting page
        public ApplicationPage CurrentPage { get; private set; } = ApplicationPage.CourseOverview;

        // The view model to use for the current page when the current page changes
        public BaseViewModel CurrentPageViewModel { get; set; }


        // Navigates to the specified page
        public void GoToPage(ApplicationPage page, BaseViewModel viewModel = null) {
            // Set the view Model
            CurrentPageViewModel = viewModel;

            // Set the current page
            CurrentPage = page;
        }

    }
}
