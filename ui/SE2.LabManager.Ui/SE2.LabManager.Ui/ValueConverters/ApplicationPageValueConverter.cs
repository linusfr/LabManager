using SE2.LabManager.Logic;
using System;
using System.Diagnostics;
using System.Globalization;

namespace SE2.LabManager.Ui
{
    /// <summary>
    /// Converts the ApplicationPage to an actual view/page
    /// </summary>

    public class ApplicationPageValueConverter : BaseValueConverter<ApplicationPageValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Get current page view model
            ApplicationViewModel avm = IoC.Get<ApplicationViewModel>();
            BaseViewModel currentPageViewModel = avm.CurrentPageViewModel;

            // Find the appropriate page
            switch ((ApplicationPage)value)
            {
                case ApplicationPage.CourseOverview:
                    return new CourseOverviewPage(currentPageViewModel as CourseOverviewViewModel);
                case ApplicationPage.CourseDetail:
                    return new CourseDetailPage(currentPageViewModel as CourseDetailViewModel);
                case ApplicationPage.LabDetail:
                    return new LabDetailPage(currentPageViewModel as LabDetailViewModel);

                default:
                    Debugger.Break();
                    return null;

            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
