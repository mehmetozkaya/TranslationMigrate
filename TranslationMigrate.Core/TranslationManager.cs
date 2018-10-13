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

            CompareAndSync(sourceTranslations, targetTranslations);            
        }

        private void CompareAndSync(TranslationStack sourceTranslations, TranslationStack targetTranslations)
        {
            foreach (var sourceItem in sourceTranslations.TranslationEnglish.Entities)
            {
                var isExist = false;

                foreach (var targetItem in targetTranslations.TranslationEnglish.Entities)
                {
                    if(
                        sourceItem["etel_formid"].ToString() == targetItem["etel_formid"].ToString()
                        && sourceItem["etel_lcid"].ToString() == targetItem["etel_lcid"].ToString()
                        && sourceItem["etel_code"].ToString() == targetItem["etel_code"].ToString()
                        )
                    {
                        isExist = true;
                        if (sourceItem["etel_message"].ToString() != targetItem["etel_message"].ToString())
                        {
                            Microsoft.Xrm.Sdk.Entity targetUpdate = new Microsoft.Xrm.Sdk.Entity(targetItem.LogicalName, targetItem.Id);
                            targetUpdate["etel_message"] = sourceItem.Attributes["etel_message"].ToString();

                            using (var service = new DynamicsService(CredentialType.MasterDev))
                            {
                                service.Update(targetUpdate);
                            }
                        }
                    }
                }

                if(!isExist)
                {
                    using (var service = new DynamicsService(CredentialType.MasterDev))
                    {
                        service.Create(sourceItem);
                    }
                }
            }
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

        private TranslationStack GetSourceTranslationList()
        {
            using (var service = new DynamicsService(CredentialType.Dev))
            {
                var query = service.CreateQueryWithLastDays(LanguageCode.English, 10);
                var entityCollection = service.Execute(query);

                var querySpanish = service.CreateQueryWithLastDays(LanguageCode.Spanish, 10);
                var entityCollectionSpanish = service.Execute(querySpanish);

                return new TranslationStack
                {
                    TranslationEnglish = entityCollection,
                    TranslationSpanish = entityCollectionSpanish
                };
            };
        }
    }
}
