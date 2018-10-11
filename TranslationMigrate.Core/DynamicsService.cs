using System;
using Microsoft.Xrm.Sdk.Client;
using System.Configuration;
using System.ServiceModel.Description;

namespace TranslationMigrate.Core
{
    internal class DynamicsService : IDynamicsService
    {
        private readonly OrganizationServiceProxy _organizationServiceProxy;

        public DynamicsService(OrganizationServiceProxy organizationServiceProxy)
        {
            _organizationServiceProxy = LoadOrganizationService();
        }

        private OrganizationServiceProxy LoadOrganizationService()
        {
            var userName = ConfigurationSettings.AppSettings["CRMUserName"].ToString();
            var password = ConfigurationSettings.AppSettings["CRMUserPassword"].ToString();
            var serviceUrl = $"{ConfigurationSettings.AppSettings["CRMOrgURL"].ToString()}/XRMServices/2011/Organization.svc";

            OrganizationServiceProxy service = null;
            try
            {
                ClientCredentials credentials = new ClientCredentials();
                credentials.UserName.UserName = userName;
                credentials.UserName.Password = password;
                Uri serviceUri = new Uri(serviceUrl);
                service = new OrganizationServiceProxy(serviceUri, null, credentials, null);
                service.EnableProxyTypes();
            }
            catch (Exception exception)
            {
                return null;
                //Log the Error message
            }            
            return service;
        }

        public string Asd() => "";
    }

    internal class DynamicsServiceFactory
    {
        private readonly string _factoryMethod;

        public DynamicsServiceFactory(string factoryMethod)
        {
            _factoryMethod = factoryMethod;
        }
    }
}
