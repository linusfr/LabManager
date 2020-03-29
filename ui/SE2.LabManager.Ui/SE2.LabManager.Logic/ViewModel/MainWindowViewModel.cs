
namespace SE2.LabManager.Logic {
    // A view model for the main Window
    public class MainWindowViewModel : BaseViewModel {
        public MainWindowViewModel() {

            // fill Database
            new DataBaseTestData();

        }
    }
}
