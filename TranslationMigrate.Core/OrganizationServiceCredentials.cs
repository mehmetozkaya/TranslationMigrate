namespace TranslationMigrate.Core
{
    public class OrganizationServiceCredentials
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Url { get; set; }



        //var userName = ConfigurationSettings.AppSettings["CRMUserName"].ToString();
        //var password = ConfigurationSettings.AppSettings["CRMUserPassword"].ToString();
        //var serviceUrl = $"{ConfigurationSettings.AppSettings["CRMOrgURL"].ToString()}/XRMServices/2011/Organization.svc";


        private const string DevUserNameConfiguration = "DevUserName";
        private const string DevPasswordConfiguration = "DevPassword";
        private const string DevURLConfiguration = "DevURL";
    }
}
