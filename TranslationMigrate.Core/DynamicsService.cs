using System;
using Microsoft.Xrm.Sdk.Client;
using System.ServiceModel.Description;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;

namespace TranslationMigrate.Core
{
    public class DynamicsService : IDynamicsService, IDisposable
    {
        private OrganizationServiceProxy _organizationServiceProxy;
        private OrganizationServiceCredentials _credentials;

        private DynamicsService()
        {
            _credentials = new OrganizationServiceCredentials(CredentialType.Dev);            
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

        public QueryExpression CreateQuery(LanguageCode languageCode)
        {
            QueryExpression query = new QueryExpression("etel_translation");
            query.Criteria.AddCondition("etel_lcid", ConditionOperator.Equal, (int)languageCode);
            query.Criteria.AddCondition("statecode", ConditionOperator.Equal, 0);
            query.ColumnSet = new ColumnSet(new string[] { "etel_lcid", "etel_formid", "etel_code", "etel_message", "modifiedon", "createdon" });
            query.Orders.Add(new OrderExpression("modifiedon", OrderType.Descending));
            query.TopCount = 5000;

            return query;
        }

        internal void UpdateTranslation(Entity sourceItem)
        {
            var translationEntity = Retrieve(sourceItem.LogicalName, sourceItem.Id, new ColumnSet(new string[] { "etel_lcid", "etel_formid", "etel_code", "etel_message", "modifiedon", "createdon" }));
            translationEntity["etel_code"] = sourceItem.Attributes["etel_code"].ToString();
            translationEntity["etel_formid"] = sourceItem.Attributes["etel_formid"].ToString();
            translationEntity["etel_message"] = sourceItem.Attributes["etel_message"].ToString();
            Update(translationEntity);
        }

        public QueryExpression CreateQueryWithLastDays(LanguageCode languageCode, int lastDayCount)
        {
            var query = CreateQuery(languageCode);
            query.Criteria.AddCondition("modifiedon", ConditionOperator.LastXDays, lastDayCount);
            return query;
        }

        public EntityCollection Execute(QueryExpression query) => 
            _organizationServiceProxy.RetrieveMultiple(query);

        public Entity Retrieve(string entityName, Guid entityGuid, ColumnSet columnSet) => _organizationServiceProxy.Retrieve(entityName, entityGuid, columnSet);

        public void Update(Entity entity) => _organizationServiceProxy.Update(entity);

        public void Create(Entity entity) => _organizationServiceProxy.Create(entity);

        public void Dispose()
        {
            _organizationServiceProxy = null;
            _credentials = null;
        }
    }
}
