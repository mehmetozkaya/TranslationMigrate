using System;

namespace TranslationMigrate.Core
{
    public class TranslationItem
    {
        public string FormId { get; set; }
        public string Code { get; set; }
        public int LanguageId { get; set; }
        public Guid RecordGuid { get; set; }
        public string Message { get; set; }        
    }
}
