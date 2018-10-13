using System.Configuration;

namespace TranslationMigrate.Core
{
    internal class OrganizationServiceCredentials
    {
        public string UserName { get; private set; }
        public string Password { get; private set; }
        public string Url { get; private set; }

        internal OrganizationServiceCredentials(CredentialType credentialType)
        {
            LoadCredentialItems(credentialType);
        }

        private void LoadCredentialItems(CredentialType credentialType)
        {
            switch (credentialType)
            {
                case CredentialType.Dev:
                    UserName = ConfigurationSettings.AppSettings[DevUserNameConfiguration].ToString();
                    Password = ConfigurationSettings.AppSettings[DevPasswordConfiguration].ToString();
                    Url = $"{ConfigurationSettings.AppSettings[DevURLConfiguration].ToString()}/XRMServices/2011/Organization.svc";
                    break;
                case CredentialType.MasterDev:
                    UserName = ConfigurationSettings.AppSettings[MasterDevUserNameConfiguration].ToString();
                    Password = ConfigurationSettings.AppSettings[MasterDevPasswordConfiguration].ToString();
                    Url = $"{ConfigurationSettings.AppSettings[MasterDevURLConfiguration].ToString()}/XRMServices/2011/Organization.svc";
                    break;
            }
        }

        private const string DevUserNameConfiguration = "DevUserName";
        private const string DevPasswordConfiguration = "DevPassword";
        private const string DevURLConfiguration = "DevURL";

        private const string MasterDevUserNameConfiguration = "MasterDevUserName";
        private const string MasterDevPasswordConfiguration = "MasterDevPassword";
        private const string MasterDevURLConfiguration = "MasterDevURL";
    }
}
