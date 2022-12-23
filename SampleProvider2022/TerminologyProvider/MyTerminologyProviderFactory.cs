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
	        // TODO Check that your credentials or other criteria are still valid from the criteria persisted in the Uri
			// If not valid then throw an exception here (e.g. failed login etc...)
			

			return new MyTerminologyProvider(terminologyProviderUri);
        }

        public bool SupportsTerminologyProviderUri(Uri terminologyProviderUri)
        {
            return terminologyProviderUri.AbsoluteUri.StartsWith("very.sample");
        }
    }
}
