namespace Cookbook_v2.Application.Settings
{
    public class AuthenticationSettings
    {
        /// <summary>
        /// Secret key used for encryption
        /// </summary>
        public string Secret { get; set; } = "";

        /// <summary>
        /// Expiration time in days
        /// </summary>
        public int ExpirationInDays { get; set; } = 0;
    }
}
