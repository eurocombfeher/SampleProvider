using Sdl.Terminology.TerminologyProvider.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms.Integration;
using SampleProvider.Views;

namespace SampleProvider
{
    public sealed class MyTerminologyProvider : AbstractTerminologyProvider
    {
        private IDefinition _definition;
        private string _name;
        //private static bool _initialized;

        public override string Name => _name;
        public override string Description => "Sample terminology provider";
        public override Uri Uri { get; }
        public override IDefinition Definition => _definition;

        public MyTerminologyProvider(Uri uri)
        {
            Uri = uri;
            _definition = new Definition(new List<IDescriptiveField>(), new List<IDefinitionLanguage>());
            _name = $"SAMPLE (SOME-ID/SERVER)";
            //TryLogin(); //moved to factory
            Status = new TerminologyProviderStatus(true);
        }



        public override IEntry GetEntry(int id)
        {
            return entries.FirstOrDefault(x => x.Id == id);
        }

        public override IEntry GetEntry(int id, IEnumerable<ILanguage> languages)
        {
            return entries.FirstOrDefault(x => x.Id == id);
        }

        public override IList<ILanguage> GetLanguages()
        {
            return new List<ILanguage>
            {
                new EntryLanguage{Locale = new CultureInfo("en-US"), Name = "English"},
                new EntryLanguage { Locale = new CultureInfo("de-DE"), Name = "German" },
                new EntryLanguage { Locale = new CultureInfo("fr-FR"), Name = "French" },
                new EntryLanguage { Locale = new CultureInfo("ja-JA"), Name = "Japanese" },
            };
        }

        private int _idIndex = 1;
        private List<IEntry> entries = new List<IEntry>();
        public override IList<ISearchResult> Search(string text, ILanguage source, ILanguage destination, int maxResultsCount, SearchMode mode, bool targetRequired)
        {
            var splitted = text.Split();
            var toReturn = new List<ISearchResult>();
            foreach (var s in splitted)
            {
                var ind = text.IndexOf(s, StringComparison.Ordinal);
                if (ind < 0) continue;
                var hit = new SearchMarkupResult
                {
                    Language = source,
                    Id = _idIndex++,
                    Score = 100,
                    Text = s,
                    Positions = new List<IMarkupPosition> { new MarkupPosition { Start = 0, Length = 5 } } //{ Start = ind, Length = s.Length }
                };
                var e = new Entry
                {
                    Id = hit.Id, 
                    Fields = new List<IEntryField>(), 
                    Languages = new List<IEntryLanguage>
                    {
                        new EntryLanguage{Locale = source.Locale, Name = source.Name, Fields = new List<IEntryField>(), Terms = new List<IEntryTerm>{ new EntryTerm{Value = s, Transactions = new List<IEntryTransaction>() } }},
                        new EntryLanguage{Locale = destination.Locale, Name = destination.Name, Fields = new List<IEntryField>(), Terms = new List<IEntryTerm>{ new EntryTerm{Value = s + " TARGET", Transactions = new List<IEntryTransaction>() } }},
                    },
                    Transactions = new List<IEntryTransaction>(),
                };
                foreach (var language in e.Languages.OfType<EntryLanguage>())
                {
                    language.ParentEntry = e;
                    foreach (var term in language.Terms.OfType<EntryTerm>())
                    {
                        term.ParentLanguage = language;
                    }
                }
                entries.Add(e);
                toReturn.Add(hit);
            }
            return toReturn;
        }

    }
}
