using SE2.LabManager.Logic;
using System.Windows;

namespace SE2.LabManager.Ui {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        public MainWindow() {
            InitializeComponent();

            // bind to viewModel
            DataContext = new MainWindowViewModel();
        }
        


    }
}
