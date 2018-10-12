using System;
using Microsoft.Xrm.Sdk.Client;
using System.ServiceModel.Description;

namespace TranslationMigrate.Core
{
    public class DynamicsService : IDynamicsService, IDisposable
    {
        private OrganizationServiceProxy _organizationServiceProxy;
        private OrganizationServiceCredentials _credentials;

        private DynamicsService()
        {
            
        }

        public DynamicsService(CredentialType credentialType)
        {
            _credentials = new OrganizationServiceCredentials(credentialType);
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

        public void Dispose()
        {
            _organizationServiceProxy = null;
            _credentials = null;
        }
    }
}
