using Microsoft.Xrm.Sdk;

namespace TranslationMigrate.Core
{
    public class TranslationStack
    {
        public EntityCollection TranslationEnglish { get; set; }
        public EntityCollection TranslationSpanish { get; set; }
    }
}
