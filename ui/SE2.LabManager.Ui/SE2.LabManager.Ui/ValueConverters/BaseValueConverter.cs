using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace SE2.LabManager.Ui
{

    /// <summary>
    /// A base value Converter that allows direct XAML usage
    /// </summary>

    public abstract class BaseValueConverter<T> : MarkupExtension, IValueConverter
        where T: class, new()
    {
        #region Private Members
        // a single static instance of this value converter
        private static T mConverter = null;
        #endregion

        #region Markup Extension Methods
        // Provides a static instance of the value converter
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return mConverter ?? (mConverter = new T());
        }
        #endregion

        #region Value Converter Methods

        // method to convert one type to another
        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

        // method to convert a value back to its source type
        public abstract object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);
        #endregion


    }
}
