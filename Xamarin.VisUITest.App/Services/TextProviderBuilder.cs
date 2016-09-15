using System;
using System.Collections.Generic;
using MvvmCross.Plugins.JsonLocalization;

namespace Xamarin.VisUITest.App.Services
{
    public class TextProviderBuilder : MvxTextProviderBuilder
    {
        public TextProviderBuilder()
            : base(Constants.GeneralNamespace, Constants.RootTextFolder, new MvxEmbeddedJsonDictionaryTextProvider(false))
        {
        }

        protected override IDictionary<string, string> ResourceFiles
        {
            get
            {
                var dictionary = new Dictionary<string, string>();

                dictionary.Add(Constants.TextTypeKey, Constants.TextTypeKey);

                return dictionary;
            }
        }
    }
}