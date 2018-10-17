using Microsoft.Xrm.Sdk;
using System;

namespace TranslationMigrate.Core
{
    public class TranslationStack : IComparable
    {
        public EntityCollection TranslationEnglish { get; set; }
        public EntityCollection TranslationSpanish { get; set; }

        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
