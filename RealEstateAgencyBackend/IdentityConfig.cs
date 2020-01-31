using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using RealEstateAgencyBackend.BLL;
using RealEstateAgencyBackend.DAL.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstateAgencyBackend
{
    public class IdentityConfig
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie
            });
        }
    }
}