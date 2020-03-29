using SE2.LabManager.Logic;

namespace SE2.LabManager.Ui
{
    /// <summary>
    /// Interaction logic for CourseOverviewPage.xaml
    /// </summary>
    public partial class CourseOverviewPage : BasePage<CourseOverviewViewModel>
    {
        public CourseOverviewPage(CourseOverviewViewModel specificViewModel) : base(specificViewModel)
        {
            InitializeComponent();
        }
    }
}
