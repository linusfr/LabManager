using SE2.LabManager.Logic;
using System.Windows;

namespace SE2.LabManager.Ui {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {


        // Custom startup to load IoC immediately before anything else
        protected override void OnStartup(StartupEventArgs e)
        {
            // Let base application do what it needs
            base.OnStartup(e);

            // setup IoC
            IoC.Setup();

            // Show the main window
            Current.MainWindow = new MainWindow();
            Current.MainWindow.Show();
        }
    }
}
