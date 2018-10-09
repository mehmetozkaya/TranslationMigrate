using Microsoft.Xrm.Sdk.Client;

namespace TranslationMigrate.Core
{
    internal class DynamicsService : IDynamicsService
    {
        private readonly OrganizationServiceProxy _organizationServiceProxy;

        public DynamicsService(OrganizationServiceProxy organizationServiceProxy)
        {
            _organizationServiceProxy = organizationServiceProxy;
        }


    }
}
