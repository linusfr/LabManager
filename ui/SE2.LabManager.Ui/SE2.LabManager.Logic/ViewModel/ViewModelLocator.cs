namespace SE2.LabManager.Logic {

    /// <summary>
    ///  Locates view models from the IoC for use in binding in Xaml files
    /// </summary>
    public class ViewModelLocator {

        #region Public Properties

        // Singleton instance of the locator
        public static ViewModelLocator Instance { get; private set; } = new ViewModelLocator();

        //The application view model 
        public static ApplicationViewModel ApplicationViewModel => IoC.Get<ApplicationViewModel>();

        #endregion
    }
}
