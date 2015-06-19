using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.Logging;
using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.SqlSyntax;

namespace Umbraco_Identity_Playground.Auth
{
    public class AddDbTable : ApplicationEventHandler
    {
        //This happens everytime the Umbraco Application starts
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            //Get the Umbraco Database context
            var db = applicationContext.DatabaseContext.Database;

            //Should I use DBSchemaHelper?
            //As DB.TableExist extension methods seems to call DBSchemaHelper?
            //Also with new way why is it moaning about SqlSyntaxContext
            var creator = new DatabaseSchemaHelper(db, LoggerResolver.Current.Logger, SqlSyntaxContext.SqlSyntaxProvider);

            //New way
            //Check if DB table does NOT exist - if not create it
            if (creator.TableExist("umbracoUserTwoFactor"))
            {
                creator.CreateTable<TwoFactorAuthPoco>();
            }


            //Check if the DB table does NOT exist
            //if (!db.TableExist("umbracoUserTwoFactor"))
            //{
            //    //Create DB table - and set overwrite to false
            //    db.CreateTable<TwoFactorAuthPoco>(false);
            //}
        }

          
    }
}