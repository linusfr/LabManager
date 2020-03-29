using System;
using System.Windows.Input;

namespace SE2.LabManager.Logic {
    /// <summary>
    /// A basic command that runs an action
    /// </summary>

    internal class RelayCommandParameterized : ICommand {

        #region Private Members

        // The action to run
        private Action<object> mAction;

        #endregion

        #region Public Events

        // The event that is fired when CanExecute(object) value has changed
        public event EventHandler CanExecuteChanged = (sender, e) => { };

        #endregion

        #region Constructor

        // Default constructor with parameter
        public RelayCommandParameterized(Action<object> action) {
            mAction = action;
        }

        #endregion


        #region Command Methods

        // A relay command can always execute
        public bool CanExecute(object parameter) {
            return true;
        }

        // Executes the commands Action
        public void Execute(object parameter) {
            mAction(parameter);
        }

        #endregion

    }
}
