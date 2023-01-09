using Sdl.Terminology.TerminologyProvider.Core;
using System;
using System.Windows.Forms.Integration;
using SampleProvider.Views;

namespace SampleProvider
{
    [TerminologyProviderFactory(Id = "VeryCustom_Terminology_Provider_Id",
                                Name = "VeryCustom_Terminology_Provider_Name",
                                Description = "VeryCustom_Terminology_Provider_Description")]
    public class MyTerminologyProviderFactory : ITerminologyProviderFactory
    {
        private static bool _initialized;
        public ITerminologyProvider CreateTerminologyProvider(Uri terminologyProviderUri, ITerminologyProviderCredentialStore credentials)
        {
            TryLogin();
            return new MyTerminologyProvider(terminologyProviderUri);
        }


        private void TryLogin()
        {
            if (_initialized) return;
            //if the data is not initialized we open a (blocking) window in which the user can login (like Remote MT termbase if not available), then on window close continue init
            var successfulLogin = ShowConfiguration();
            _initialized = successfulLogin;
        }

        private bool ShowConfiguration()
        {
            var window = new SampleConfig();
            ElementHost.EnableModelessKeyboardInterop(window);
            window.ShowDialog();
            return true;
        }


        public bool SupportsTerminologyProviderUri(Uri terminologyProviderUri)
        {
            return terminologyProviderUri.AbsoluteUri.StartsWith("very.sample");
        }
    }
}
