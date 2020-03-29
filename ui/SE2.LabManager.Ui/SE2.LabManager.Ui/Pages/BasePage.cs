using SE2.LabManager.Logic;
using System.Windows.Controls;

namespace SE2.LabManager.Ui
{
    /// <summary>
    /// A base page for all pages to gain base functionality
    /// </summary>

    public class BasePage<VM> : Page
        // tell the class that VM has to be a BaseViewModel and can be newed
        where VM:BaseViewModel, new()
    {

        #region Private Members

        // The View Model associated with this page
        private VM mViewModel;

        #endregion

        #region Public Properties

        public VM ViewModel {
        get
            {
                return mViewModel;
            }
            set
            {
                // If nothing has changed
                if (mViewModel == value)
                    return;

                // Update the value
                mViewModel = value;

                // Set the data context  for this page
                this.DataContext = mViewModel;
            }
        }


        #endregion


        #region Constructor

        // Default constructor 
        public BasePage()
        {
            // Create a default view model
            ViewModel = IoC.Get<VM>();

        }
      
        // Default constructor with specific view model to use, if any
        public BasePage(VM specificViewModel = null)
        {
            // set specific view model
            if (specificViewModel != null)
                ViewModel = specificViewModel;
            else
                // Create a default view model
                ViewModel = IoC.Get<VM>();
        }

        #endregion

    }
}
