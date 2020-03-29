﻿using Ninject;
using SE2.LabManager.Logic;
using System;
using System.Diagnostics;
using System.Globalization;

namespace SE2.LabManager.Ui
{
    /// <summary>
    /// Converts a string name to a service pulled from the IoC container
    /// </summary>

    public class IoCConverter : BaseValueConverter<IoCConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Find the appropriate page
           switch ((string)parameter)
            {
                case nameof(ApplicationViewModel):
                    return IoC.Get<ApplicationViewModel>();
               
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
