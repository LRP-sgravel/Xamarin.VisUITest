using System;
using System.Globalization;
using MvvmCross.Localization;
using MvvmCross.Platform.Converters;
using Xamarin.Forms;

namespace Xamarin.VisUITest.App.Views
{
    class LanguageValueConverter : MvxValueConverter<IMvxTextProvider, string>, IValueConverter
    {
        protected override string Convert(IMvxTextProvider value, Type targetType, object parameter, CultureInfo culture)
        {
            string result = string.Empty;

            if (value != null && parameter is string)
            {
                result = value.GetText(Constants.GeneralNamespace, Constants.TextTypeKey, parameter as string);
            }

            return result;
        }

        protected override IMvxTextProvider ConvertBack(string value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
