using System;

namespace TranslationMigrate.Core
{
    public class TranslationManager : ITranslationManager
    {
        public TranslationManager()
        {
            using (var service = new DynamicsService(CredentialType.Dev))
            {

                

            };  
            
        }

        public void Sync()
        {
            throw new NotImplementedException();
        }
    }
}
