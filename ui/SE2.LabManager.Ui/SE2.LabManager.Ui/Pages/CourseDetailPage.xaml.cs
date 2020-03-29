using SE2.LabManager.Logic;

namespace SE2.LabManager.Ui
{
    /// <summary>
    /// Interaction logic for CourseDetailPage.xaml
    /// </summary>
    public partial class CourseDetailPage : BasePage<CourseDetailViewModel>
    {
        // Default constructor
        public CourseDetailPage() : base()
        {
            InitializeComponent();
        }

        // Constructor with specific view model
        public CourseDetailPage(CourseDetailViewModel specificViewModel) : base(specificViewModel)
        {
            InitializeComponent();
        }
    }
}
