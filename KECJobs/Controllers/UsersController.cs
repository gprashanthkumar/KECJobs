using KECJobs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static KECJobs.Utils;

namespace KECJobs.Controllers
{
    public class UsersController : Controller
    {
        // GET: Users
        public ActionResult Index()
        {
            return View();
        }



        #region "Remove"
        // GET: Users/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Users/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Users/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        #endregion


        // GET: /Users/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        // GET: /Users/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(KECLoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await ValidateUser(model);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.Inactive:
                    return View("Lockout");                
                case SignInStatus.Invalid:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            KECAuthenticate.AuthenticationSignOff();

            return RedirectToAction("Index", "Home");
        }

        #region "Helpers"

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
        #endregion

        private async Task<SignInStatus> ValidateUser(KECLoginViewModel userData)
        {
            var _result = SignInStatus.Invalid;
            using (var KECentity = new KECJobsEntities())
            {
                var _user = (from u in KECentity.tbl_Users
                         where u.UserName == userData.UserName
                            && u.Password == userData.Password
                         select u).FirstOrDefault();

                if (_user != null) {

                    if ((_user != null) && (_user.isActive = false))
                    {
                        _result = SignInStatus.Inactive;
                    }
                    else if ((_user != null) && (_user.isActive = true))
                    {
                        //Now get role details and set them
                        //based on userid set authentication cookie
                        var Token = KECAuthenticate.CreateAuthToken(_user.UserID);
                        Response.Cookies.Add(Token);
                        KECAuthenticate.UserID = _user.UserID.ToString();
                        KECAuthenticate.FullName = _user.FullName;
                        if (_user.RoleID != null) {
                            KECAuthenticate.RoleId = (int) _user.RoleID;
                            KECAuthenticate.RoleName = _user.tbl_Lookup_Roles.RoleName;
                            KECAuthenticate.IsAdmin = _user.tbl_Lookup_Roles.isAdmin;
                            KECAuthenticate.isJobsEditor = _user.tbl_Lookup_Roles.isJobsEditor;
                            KECAuthenticate.isReferenceEditor = _user.tbl_Lookup_Roles.isReferenceEditor;
                            KECAuthenticate.isRegistrationEditor = _user.tbl_Lookup_Roles.isRegistrationEditor;
                            KECAuthenticate.isSkillDevelopmentEditor = _user.tbl_Lookup_Roles.isSkillDevelopmentEditor;
                            KECAuthenticate.isGuest = _user.tbl_Lookup_Roles.IsGuest;
                        }
                       
                    



                        _result = SignInStatus.Success;
                    }
                }
                _user = null;


            }
            return _result;

        }
    }
}
