using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace KECJobs
{
    public class Utils
    {
        public enum SignInStatus
        {
            [Description("User login credentials are successful.")]
            Success = 1,
            [Description("User account is inactive. Contact Adminstrator")]
            Inactive = 2,
            [Description("User login credentials are invalid.")]
            Invalid = 3
        }
    }
}