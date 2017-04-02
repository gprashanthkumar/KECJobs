using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using static KECJobs.Constants;
using System.Globalization;
using System.Configuration;

namespace KECJobs
{
    public static class KECAuthenticate
    {
        public static bool isAuthenticated
        {
            get
            {
                return !string.IsNullOrEmpty(UserID);
            }
        }
        public static string UserID
        {
            get
            {
                var res = string.Empty;
               res = GetCookie(SessionVariables.UserID);

                return res;

            }
            set
            {
                SetCookie(SessionVariables.UserID, value);

            }
        }
        public static string EmailAddress
        {
            get
            {
                var res = string.Empty;
                res = GetCookie(SessionVariables.EmailId);


                return res;

            }
            set
            {
                SetCookie(SessionVariables.EmailId, value);
            }
        }
        public static string FullName
        {
            get
            {
                var res = string.Empty;
                res = GetCookie(SessionVariables.Name);


                return res;
            }
            set
            {
                SetCookie(SessionVariables.Name, value);
            }
        }

        public static int RoleId
        {
            get
            {
                var res = string.Empty;
                res = GetCookie(SessionVariables.RoleId);


                return Convert.ToInt32(res);
            }
            set
            {
                SetCookie(SessionVariables.RoleId, value);
            }
        }
        public static string RoleName
        {
            get
            {
                var res = string.Empty;
                res = GetCookie(SessionVariables.RoleName);


                return res;
            }
            set
            {
                SetCookie(SessionVariables.RoleName, value);
            }
        }
        public static bool IsAdmin
        {
            get
            {
                var res = string.Empty;
                res = GetCookie(SessionVariables.isAdmin);


                return Convert.ToBoolean(res);

            }
            set
            {
                SetCookie(SessionVariables.isAdmin, value);
            }
        }
        public static bool isJobsEditor
        {
            get
            {
                var res = string.Empty;
                res = GetCookie(SessionVariables.isJobsEditor);


                return Convert.ToBoolean(res);


            }
            set
            {
                SetCookie(SessionVariables.isJobsEditor, value);
            }
        }

        public static bool isReferenceEditor
        {
            get
            {
                var res = string.Empty;
                res = GetCookie(SessionVariables.isReferenceEditor);

                return Convert.ToBoolean(res);


            }
            set
            {
                SetCookie(SessionVariables.isReferenceEditor, value);
            }
        }
        public static bool isRegistrationEditor
        {
            get
            {
                var res = string.Empty;
                res = GetCookie(SessionVariables.isRegistrationEditor);

                return Convert.ToBoolean(res);

            }
            set
            {
                SetCookie(SessionVariables.isRegistrationEditor, value);
            }
        }
        public static bool isGuest
        {
            get
            {
                var res = string.Empty;
                res = GetCookie(SessionVariables.isGuest);

                return Convert.ToBoolean(res);

            }
            set
            {
                SetCookie(SessionVariables.isGuest, value);
            }
        }
        public static HttpCookie CreateAuthToken(int UserId, bool isPersistantCookie = false)
        {
            var expirytime = DateTime.Now.AddMinutes(60);

            if (isPersistantCookie)
                expirytime = DateTime.Now.AddYears(1);

            FormsAuthentication.Initialize();
            var ticket = new FormsAuthenticationTicket(1, UserId.ToString(CultureInfo.InvariantCulture), DateTime.Now, expirytime, isPersistantCookie, UserId.ToString(CultureInfo.InvariantCulture));


            var authenticationCookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket))
            {
                Expires = ticket.Expiration,
                Secure = SecureCookieEnabled()
            };



            return authenticationCookie;

        }


        public static void AuthenticationSignOff()
        {

            FormsAuthentication.SignOut();
            CleanupCookie(SessionVariables.UserID);
            CleanupCookie(SessionVariables.Name );
            CleanupCookie(SessionVariables.EmailId);
            CleanupCookie(SessionVariables.isGuest);
            CleanupCookie(SessionVariables.isAdmin);
            CleanupCookie(SessionVariables.isJobsEditor);
            CleanupCookie(SessionVariables.isReferenceEditor);
            CleanupCookie(SessionVariables.isRegistrationEditor);
            CleanupCookie(SessionVariables.RoleId);
            CleanupCookie(SessionVariables.RoleName);
            
        }
        public static bool SecureCookieEnabled()
        {
            string myval = ConfigurationManager.AppSettings["SecureCookie"];

            if (myval == null) return false;

            if (myval.ToLower() == "true")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void CleanupCookie(string cookieName) {

            var currentUserCookie = HttpContext.Current.Request.Cookies[cookieName];
            try
            {
                if (currentUserCookie != null)
                {

                    HttpContext.Current.Response.Cookies.Remove(cookieName);
                    currentUserCookie.Expires = DateTime.Now.AddDays(-10);
                    currentUserCookie.Value = null;
                    HttpContext.Current.Response.SetCookie(currentUserCookie);
                }
            }
            finally
            {
                currentUserCookie = null;
            }
            
           


        }

        internal static string GetCookie(string CookieName)
        {

            var res = string.Empty;
            try
            {
                //if (CookieName == SessionVariables.Name)
                //{
                //    Console.WriteLine("Getting  Fullname");
                //    Console.WriteLine(DecodeFrom64(HttpContext.Current.Request.Cookies[CookieName].Value));
                //}


                if (HttpContext.Current.Request.Cookies[CookieName] != null)
                    res = DecodeFrom64(HttpContext.Current.Request.Cookies[CookieName].Value);
            }
            catch (Exception ex) {
                //throw ex;
            }    

           

            return res;

        }

        internal static void SetCookie(string CookieName, object CookieValue) {

            try
            {
                if ((CookieValue != null) && !string.IsNullOrEmpty(Convert.ToString(CookieValue)))
                {

                    //if (CookieName == SessionVariables.Name)
                    //{
                    //    Console.WriteLine("setting  Fullname");
                    //    Console.WriteLine(CookieValue.ToString());
                    //}

                    HttpContext.Current.Response.Cookies[CookieName].Value = EncodeTo64(CookieValue.ToString());

                }
            }
            catch (Exception)
            {

                //throw;
            }
           

        }


    }
}