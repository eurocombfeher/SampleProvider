using Sdl.Terminology.TerminologyProvider.Core;
using System;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using SampleProvider.Views;

namespace SampleProvider
{
    [TerminologyProviderWinFormsUI]
	class MyTerminologyProviderWinFormsUI : ITerminologyProviderWinFormsUI
	{
		private static bool _initialized;

		public bool SupportsEditing => true;

		public string TypeDescription => "Terminology Provider for Very Sample Web Services";

		public string TypeName => "Very sample Terminology Provider";

		public ITerminologyProvider[] Browse(IWin32Window owner, ITerminologyProviderCredentialStore credentialStore)
		{
			// TODO initial settings/request for credentials when user is adding your custom provider
			//TryLogin();

			var uri = new Uri($"very.sample://SOME-URL#####SOME-NAME####SUB-NAME");
			var provider = new MyTerminologyProvider(uri);
			return new ITerminologyProvider[] { provider };
		}

		public bool Edit(IWin32Window owner, ITerminologyProvider terminologyProvider)
		{
			// TODO edit existing settings/credentials
			//TryLogin();

			return false;
		}

		public TerminologyProviderDisplayInfo GetDisplayInfo(Uri terminologyProviderUri)
		{
			var data = terminologyProviderUri.ToString().Split(new[] { "//" }, StringSplitOptions.RemoveEmptyEntries).LastOrDefault();
			var datas = data?.Split(new[] { "#####" }, StringSplitOptions.RemoveEmptyEntries);
			var suffix = datas == null || datas.Length < 3 ? "" : $" ({datas[1]}/{datas[2]})";
			return new TerminologyProviderDisplayInfo { Name = TypeName + suffix, TooltipText = TypeDescription + suffix };
		}

		public bool SupportsTerminologyProviderUri(Uri terminologyProviderUri)
		{
			return terminologyProviderUri.AbsoluteUri.StartsWith("very.sample");
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
	}
}
