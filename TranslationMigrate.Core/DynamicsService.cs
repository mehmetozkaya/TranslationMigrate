﻿using System;
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

        private void GetTranslation()
        {
            QueryExpression querySpanish = new QueryExpression("etel_translation");
            querySpanish.Criteria.AddCondition("etel_lcid", ConditionOperator.Equal, 3082);
            querySpanish.Criteria.AddCondition("statecode", ConditionOperator.Equal, 0);            
            querySpanish.ColumnSet = new ColumnSet(new string[] { "etel_lcid", "etel_formid", "etel_code", "etel_message", "modifiedon", "createdon" });
            querySpanish.Orders.Add(new OrderExpression("modifiedon", OrderType.Descending));
            querySpanish.TopCount = 5000;

            QueryExpression queryEnglish = new QueryExpression("etel_translation");
            queryEnglish.Criteria.AddCondition("etel_lcid", ConditionOperator.Equal, 1033);
            queryEnglish.Criteria.AddCondition("statecode", ConditionOperator.Equal, 0);            
            queryEnglish.ColumnSet = new ColumnSet(new string[] { "etel_lcid", "etel_formid", "etel_code", "etel_message", "modifiedon", "createdon" });
            queryEnglish.TopCount = 5000;
            queryEnglish.Orders.Add(new OrderExpression("modifiedon", OrderType.Descending));

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
        

        public EntityCollection Execute(QueryExpression query) => 
            _organizationServiceProxy.RetrieveMultiple(query);

        public void Dispose()
        {
            _organizationServiceProxy = null;
            _credentials = null;
        }
    }

    public enum LanguageCode
    {
        Spanish = 3082,
        English = 1033
    }
}
