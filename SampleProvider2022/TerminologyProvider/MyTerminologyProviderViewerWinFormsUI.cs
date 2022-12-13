using Sdl.Terminology.TerminologyProvider.Core;
using System;
using System.Globalization;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace SampleProvider
{
    [TerminologyProviderViewerWinFormsUI]
    class MyTerminologyProviderViewerWinFormsUI : ITerminologyProviderViewerWinFormsUI
    {
        public Control Control => new ElementHost();
        public bool Initialized => true;
        public IEntry SelectedTerm { get; set; }

        public event EventHandler<EntryEventArgs> SelectedTermChanged;
        public event EventHandler TermChanged;

        public void Initialize(ITerminologyProvider terminologyProvider, CultureInfo source, CultureInfo target)
        {

        }

        public bool SupportsTerminologyProviderUri(Uri terminologyProviderUri)
        {
            return terminologyProviderUri.AbsoluteUri.StartsWith("very.sample");
        }

        public void AddAndEditTerm(IEntry term, string source, string target)
        {
            
        }

        public void AddTerm(string source, string target)
        {
            
        }

        public void EditTerm(IEntry term)
        {
            
        }



        public void JumpToTerm(IEntry entry)
        {
            
        }

        public void Release()
        {
            
        }


    }
}
