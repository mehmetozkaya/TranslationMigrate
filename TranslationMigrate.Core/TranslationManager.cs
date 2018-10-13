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
            var devTranslations = GetDevTranslationList();

            throw new NotImplementedException();
        }

        private string GetDevTranslationList()
        {
            using (var service = new DynamicsService(CredentialType.Dev))
            {
                service.CreateQuery(LanguageCode.English);

            };

            return null;
        }

        


    }
}
