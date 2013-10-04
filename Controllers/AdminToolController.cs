using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
//using Apartment.Filters;
using Apartment.Models;


namespace Apartment.Controllers
{
    public class AdminToolController : Controller
    {
        //
        // GET: /AdminTool/

        public ActionResult Index()
        {
            return View();
        }

        
        [Authorize(Roles = "Admin")]
        public ActionResult createUser()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles="Admin")]
        public ActionResult createUser(RegisterModel model)
        {


            if (ModelState.IsValid)
            {
                //FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                try
                {

                    Membership.CreateUser(model.UserName,model.Password);
                    Roles.AddUserToRole(model.UserName, model.Role);
                    //Profile.Department = model.Department;
                    return View("createUser");

                    
                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                }
            }
            

            // If we got this far, something failed, redisplay form
            return View("createUser");
            
        }//end create user

        [Authorize(Roles = "Admin")]
        public ActionResult deleteUser()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult deleteUser(RegisterModel model)
        {


            
                //FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                try
                {
                    var userRoles = Roles.GetRolesForUser(model.UserName);
                    Roles.RemoveUserFromRoles(model.UserName,userRoles);
                    Membership.DeleteUser(model.UserName);
                    
                    return View("deleteUser");


                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                }
            

            // If we got this far, something failed, redisplay form
            return View("deleteUser");

        }//end delete user
        [Authorize(Roles = "Admin")]
        public ActionResult modifyUser()
        {
            return View();
        }

        //
        // POST: /Account/Manage

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult modifyUser(RegisterModel model)
        {
            //all this does is allow user to change password other info   
            try
            {
                string user = model.UserName;
                var userRoles = Roles.GetRolesForUser(user);
                MembershipUser userM = Membership.GetUser(user);
                string tempPassword = userM.ResetPassword();


                userM.ChangePassword(tempPassword, model.ConfirmPassword);
                Roles.RemoveUserFromRoles(user, userRoles);
                Roles.AddUserToRole(user, model.Role);

                //Implement Email Intimation later
                //changePasswordSucceeded = WebSecurity.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword);
            }
            catch (Exception)
            {
                //changePasswordSucceeded = false;
            }


            return View("modifyUser");





            // If we got this far, something failed, redisplay form
            //return View(model);
        }//manage user

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }//end error code

    }
}
