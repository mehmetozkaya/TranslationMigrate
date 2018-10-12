using System;
using Microsoft.Xrm.Sdk.Client;
using System.Configuration;
using System.ServiceModel.Description;

namespace TranslationMigrate.Core
{
    public class DynamicsService : IDynamicsService
    {
        private readonly OrganizationServiceProxy _organizationServiceProxy;
        private readonly OrganizationServiceCredentials _credentials;

        private DynamicsService()
        {
            
        }

        public DynamicsService(OrganizationServiceCredentials organizationServiceCredentials)
        {
            _credentials = organizationServiceCredentials ?? throw new ArgumentNullException(nameof(organizationServiceCredentials));
            _organizationServiceProxy = LoadOrganizationService();
        }

        private OrganizationServiceProxy LoadOrganizationService()
        {
            try
            {
                ClientCredentials credentials = new ClientCredentials();
                credentials.UserName.UserName = _credentials.UserName;
                credentials.UserName.Password = _credentials.Password;
                Uri serviceUri = new Uri(_credentials.Url);
                var service = new OrganizationServiceProxy(serviceUri, null, credentials, null);
                service.EnableProxyTypes();
                return service;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public void MigrateTranslations()
        {
            throw new NotImplementedException();
        }
    }
}
