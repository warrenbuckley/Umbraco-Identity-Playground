using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Umbraco.Core.Security;
using Umbraco.Web.WebApi;

namespace Umbraco_Identity_Playground.Controllers
{
    public class TwoFactorController : UmbracoAuthorizedApiController
    {
        //Getting userManager taken from core
        //https://github.com/umbraco/Umbraco-CMS/blob/a3d4ccc062b94859e446b1206bf21f334577a35c/src/Umbraco.Web/Editors/AuthenticationController.cs#L54

        //Maybe move this into our own abstract class?!
        private BackOfficeUserManager _userManager;

        protected BackOfficeUserManager UserManager
        {
            get
            {
                if (_userManager == null)
                {
                    var mgr = TryGetOwinContext().Result.GetUserManager<BackOfficeUserManager>();
                    if (mgr == null)
                    {
                        throw new NullReferenceException("Could not resolve an instance of " + typeof(BackOfficeUserManager) + " from the " + typeof(IOwinContext) + " GetUserManager method");
                    }
                    _userManager = mgr;
                }
                return _userManager;
            }
        }


        /// <summary>
        /// From AngularJS do a HTTP GET
        /// To check of state of Two Factor being enabled to use with a button to toggle state
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<HttpResponseMessage> IsTwoFactorEnabled()
        {
            //TODO: UserStore needs updating to check our custom DB table to see if it is enabled

            var isEnabled = await UserManager.GetTwoFactorEnabledAsync(User.Identity.GetUserId<int>());
            return Request.CreateResponse(HttpStatusCode.OK, isEnabled);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<HttpResponseMessage> ToggleTwoFactorAuth()
        {
            //TODO: UserStore needs updating to check our custom DB table to see if it is enabled & set new value

            //Get the users current state of two factor being enabled
            var isCurrentlyEnabled = await UserManager.GetTwoFactorEnabledAsync(User.Identity.GetUserId<int>());

            //Set the inverse state, so false/disable it if isCurrentlyEnabled is true/enabled
            await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId<int>(), !isCurrentlyEnabled);

            //Return 200 state
            return Request.CreateResponse(HttpStatusCode.OK);
        }


    }
}