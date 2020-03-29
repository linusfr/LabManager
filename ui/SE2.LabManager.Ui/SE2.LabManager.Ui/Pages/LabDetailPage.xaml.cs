using SE2.LabManager.Logic;

namespace SE2.LabManager.Ui {
    /// <summary>
    /// Interaction logic for LabDetailPage.xaml
    /// </summary>
    public partial class LabDetailPage : BasePage<LabDetailViewModel> {
        public LabDetailPage() {
            InitializeComponent();
        }

        // Constructor with specific view model
        public LabDetailPage(LabDetailViewModel specificViewModel) : base(specificViewModel) {
            InitializeComponent();
        }
    }

}

