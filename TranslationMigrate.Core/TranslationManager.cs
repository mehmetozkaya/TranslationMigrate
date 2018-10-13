using System;

namespace TranslationMigrate.Core
{
    public class TranslationManager : ITranslationManager
    {        

        public TranslationManager()
        {
           
            
        }

        public void Sync()
        {
            var targetTranslations = GetTargetTranslationList();

            var sourceTranslations = GetSourceTranslationList();

            throw new NotImplementedException();
        }     

        private TranslationStack GetTargetTranslationList()
        {
            using (var service = new DynamicsService(CredentialType.MasterDev))
            {
                var query = service.CreateQuery(LanguageCode.English);
                var entityCollection = service.Execute(query);

                var querySpanish = service.CreateQuery(LanguageCode.Spanish);
                var entityCollectionSpanish = service.Execute(querySpanish);

                return new TranslationStack
                {
                    TranslationEnglish = entityCollection,
                    TranslationSpanish = entityCollectionSpanish
                };
            };            
        }

        private object GetSourceTranslationList()
        {
            throw new NotImplementedException();
        }
    }
}
