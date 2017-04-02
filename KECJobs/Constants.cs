using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KECJobs
{
    public static class Constants
    {

        public static string EncodeTo64(string _data)
        {
            string returnValue = string.Empty;
            try
            {
                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(_data);
                returnValue = System.Convert.ToBase64String(plainTextBytes);

            }
            catch (Exception)
            {
            }

            return returnValue;
        }

        public static string DecodeFrom64(string encodedData)
        {
            string returnValue = string.Empty;
            try
            {
                byte[] encodedDataAsBytes = Convert.FromBase64String(encodedData);
                returnValue = System.Text.Encoding.UTF8.GetString(encodedDataAsBytes);

            }
            catch (Exception)
            {
                //string msg = "Some Error Occured when processing the request.";
            }
            return returnValue;
        }

        public static class SessionVariables
        {
            public const string UserID = "UserID";

            //public const string FullName = "FullName";
            public const string Name = "Name";

            public const string EmailId = "EmailId";

            public const string RoleId = "RoleId";
            public const string RoleName = "RoleName";
            public const string isAdmin = "isAdmin";
            public const string isJobsEditor = "isJobsEditor";
            public const string isRegistrationEditor = "isRegistrationEditor";
            public const string isReferenceEditor = "isReferenceEditor";
            public const string isGuest = "isGuest";





            //public const string UserName = "UserName";

        }

    }
}