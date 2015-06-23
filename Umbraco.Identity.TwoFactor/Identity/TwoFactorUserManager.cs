using Microsoft.AspNet.Identity;
using Umbraco.Core.Models.Identity;
using Umbraco.Core.Security;

namespace Umbraco.Identity.TwoFactor.Identity
{
    public static class TwoFactorUserManager
    {
        
        public static BackOfficeUserManager ExtendBackOfficeUserManager(this BackOfficeUserManager userManager)
        {
            //TODO: How do I set SupportsUserTwoFactor = true on userManager
            //Why can I not set this?
            //userManager.SupportsUserTwoFactor = true;

            userManager.RegisterTwoFactorProvider("SMS", new PhoneNumberTokenProvider<BackOfficeIdentityUser, int>());
            userManager.RegisterTwoFactorProvider("Email", new EmailTokenProvider<BackOfficeIdentityUser, int>());

        }

    }
}
