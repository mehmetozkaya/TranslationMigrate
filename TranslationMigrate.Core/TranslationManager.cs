using System;

namespace TranslationMigrate.Core
{
    public class TranslationManager
    {
        private readonly IDynamicsService _service;

        public TranslationManager(IDynamicsService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }


    }
}
