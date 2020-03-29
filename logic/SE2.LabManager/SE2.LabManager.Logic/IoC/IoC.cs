using Ninject;
using System;

namespace SE2.LabManager.Logic
{
    /// <summary>
    ///  The IoC container for our application
    /// </summary>

    public static class IoC
    {

        #region Public Properties

        // The kernel for our IoC container
        public static IKernel Kernel { get; private set; } = new StandardKernel();
        
        #endregion

        #region Construction

        // Sets up the IoC container, binds all information required and is ready for use
        // Must be called as soon as application starts up to ensure all services can be found
        public static void Setup()
        {
            // Bind all required view models
            BindViewModels();
        }

        // Binds all singleton view models
        private static void BindViewModels()
        {
            // Bind to a single instance of Application view model
            Kernel.Bind<ApplicationViewModel>().ToConstant(new ApplicationViewModel());
        }

        #endregion

        // Gets a service from the IoC of the specified type
        public static T Get<T>()
        {
            return Kernel.Get<T>();
        }

    }
}
