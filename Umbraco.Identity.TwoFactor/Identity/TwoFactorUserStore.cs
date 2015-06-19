using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Umbraco.Core;
using Umbraco.Core.Models.Identity;
using Umbraco.Identity.TwoFactor.Poco;

namespace Umbraco.Identity.TwoFactor.Identity
{
    public class TwoFactorUserStore : IUserTwoFactorStore<BackOfficeIdentityUser, int>
    {
        public Task<bool> GetTwoFactorEnabledAsync(BackOfficeIdentityUser user)
        {
            //User ID to lookup in our table
            var userId = user.Id;

            //PetaPoco or nPoco that UmbracoDB uses go look it up
            var db = ApplicationContext.Current.DatabaseContext.Database;
            var record = db.SingleOrDefault<TwoFactorAuth>("WHERE userId=@0", userId);

            //No record found - create one for the user
            if (record == null)
            {
                var newRecord = new TwoFactorAuth {TwoFactorEnabled = false, UserId = userId};
                db.Insert(newRecord);

                return Task.FromResult(newRecord.TwoFactorEnabled);
            }

            //What about creating the record initialling, is that what the othermethods are for?
            return Task.FromResult(record.TwoFactorEnabled);
        }

        public Task SetTwoFactorEnabledAsync(BackOfficeIdentityUser user, bool enabled)
        {
            //User ID to lookup in our table
            var userId = user.Id;

            //PetaPoco or nPoco that UmbracoDB uses go look it up
            var db = ApplicationContext.Current.DatabaseContext.Database;

            var record = db.SingleOrDefault<TwoFactorAuth>("WHERE userId=@0", userId);

            //No record found - create one for the user
            if (record == null)
            {
                var newRecord = new TwoFactorAuth { TwoFactorEnabled = false, UserId = userId };
                db.Insert(newRecord);

                return Task.FromResult(newRecord.TwoFactorEnabled);
            }

            //Update the value (inverse the value)
            record.TwoFactorEnabled = !record.TwoFactorEnabled;
            db.Update(record);

            return Task.FromResult(0);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(BackOfficeIdentityUser user)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(BackOfficeIdentityUser user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(BackOfficeIdentityUser user)
        {
            throw new NotImplementedException();
        }

        public Task<BackOfficeIdentityUser> FindByIdAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<BackOfficeIdentityUser> FindByNameAsync(string userName)
        {
            throw new NotImplementedException();
        }

        
    }
}
