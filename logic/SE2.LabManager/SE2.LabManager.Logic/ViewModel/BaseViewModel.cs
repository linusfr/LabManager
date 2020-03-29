using System.ComponentModel;

namespace SE2.LabManager.Logic {
    // A base view model that fires Property Changed events as needed

    public class BaseViewModel : INotifyPropertyChanged {
        // The event that is fired when any child property changes its value
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
    }
}
