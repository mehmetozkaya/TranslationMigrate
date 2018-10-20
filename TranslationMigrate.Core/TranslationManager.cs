using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;

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

            var differences = CompareAndSync(sourceTranslations.TranslationEnglish, targetTranslations.TranslationEnglish);
            var differencesSpanish = CompareAndSync(sourceTranslations.TranslationSpanish, targetTranslations.TranslationSpanish);
        }

        private List<TranslationItem> CompareAndSync(EntityCollection sourceTranslations, EntityCollection targetTranslations)
        {
            var translationDifferences = new List<TranslationItem>();
            foreach (var sourceItem in sourceTranslations.Entities)
            {
                var isExist = false;
                foreach (var targetItem in targetTranslations.Entities)
                {
                    if (IsSameTranslation(sourceItem, targetItem))
                    {
                        isExist = true;
                        if (sourceItem["etel_message"].ToString() != targetItem["etel_message"].ToString())
                        {
                            UpdateTranslation(sourceItem, targetItem);
                        }
                    }
                }

                if (!isExist)
                {
                    TranslationItem translationItem = CreateTranslationItem(sourceItem);
                    translationDifferences.Add(translationItem);
                    CreateOrUpdate(sourceItem);
                }
            }
            return translationDifferences;
        }

        private void UpdateTranslation(Entity sourceItem, Entity targetItem)
        {
            Entity targetUpdate = new Entity(targetItem.LogicalName, targetItem.Id);
            targetUpdate["etel_message"] = sourceItem.Attributes["etel_message"].ToString();

            using (var service = new DynamicsService(CredentialType.MasterDev))
            {
                service.Update(targetUpdate);
            }
        }

        private bool IsSameTranslation(Entity sourceItem, Entity targetItem)
        {
            return sourceItem["etel_formid"].ToString() == targetItem["etel_formid"].ToString()
                                    && sourceItem["etel_lcid"].ToString() == targetItem["etel_lcid"].ToString()
                                    && sourceItem["etel_code"].ToString() == targetItem["etel_code"].ToString();
        }

        private void CreateOrUpdate(Entity sourceItem)
        {
            try
            {
                using (var service = new DynamicsService(CredentialType.MasterDev))
                {
                    service.Create(sourceItem);
                }
            }
            catch (Exception)
            {
                using (var service = new DynamicsService(CredentialType.MasterDev))
                {
                    service.UpdateTranslation(sourceItem);
                }
            }
        }

        private TranslationItem CreateTranslationItem(Entity sourceItem)
        {
            return new TranslationItem
            {
                Code = sourceItem.Attributes["etel_code"].ToString().Trim(),
                FormId = sourceItem.Attributes["etel_formid"].ToString().Trim(),
                LanguageId = (int)sourceItem.Attributes["etel_lcid"],
                Message = sourceItem.Attributes["etel_message"].ToString().Trim(),
                RecordGuid = sourceItem.Id
            };
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
