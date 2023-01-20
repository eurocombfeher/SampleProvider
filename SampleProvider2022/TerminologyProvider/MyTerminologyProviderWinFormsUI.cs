using Sdl.Terminology.TerminologyProvider.Core;
using System;
using System.Linq;
using System.Windows.Forms;

namespace SampleProvider
{
    [TerminologyProviderWinFormsUI]
    class MyTerminologyProviderWinFormsUI : ITerminologyProviderWinFormsUI
    {
        public bool SupportsEditing => true;

        public string TypeDescription => "Terminology Provider for Very Sample Web Services";

        public string TypeName => "Very sample Terminology Provider";

        public ITerminologyProvider[] Browse(IWin32Window owner, ITerminologyProviderCredentialStore credentialStore)
        {
            //Ensuring with GUID we have a unique value for each instance
            var uri = new Uri($"very.sample://sample.at/endpoint#####Some-Name#####" + Guid.NewGuid());
            var provider = new MyTerminologyProvider(uri);
            return new ITerminologyProvider[] { provider };
        }

        public bool Edit(IWin32Window owner, ITerminologyProvider terminologyProvider)
        {
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
    }
}
