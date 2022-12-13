using Sdl.Terminology.TerminologyProvider.Core;
using System;

namespace SampleProvider
{
    [TerminologyProviderFactory(Id = "VeryCustom_Terminology_Provider_Id",
                                Name = "VeryCustom_Terminology_Provider_Name",
                                Description = "VeryCustom_Terminology_Provider_Description")]
    public class MyTerminologyProviderFactory : ITerminologyProviderFactory
    {
        public ITerminologyProvider CreateTerminologyProvider(Uri terminologyProviderUri, ITerminologyProviderCredentialStore credentials)
        {
            return new MyTerminologyProvider(terminologyProviderUri);
        }

        public bool SupportsTerminologyProviderUri(Uri terminologyProviderUri)
        {
            return terminologyProviderUri.AbsoluteUri.StartsWith("very.sample");
        }
    }
}
