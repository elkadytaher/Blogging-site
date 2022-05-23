using Blogging.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blogging.Controllers
{
    public class EntryUsersController : Controller
    {
        // GET: EntryUsers
        UsersController usersController = new UsersController();
        BlogsEntities db = new BlogsEntities();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(user user)
        {
            if (ModelState.IsValid)
            {

                var ValidateUser = db.users.SingleOrDefault(u => u.email.ToString().Equals(user.email) && u.password.ToString().Equals(user.password));
                if (ValidateUser != null)
                {


                    SessionsCreator(ValidateUser);
                    if (ValidateUser.user_type.Equals("Blogger"))
                    {
                        SessionsCreatorForBloger(db.blogers.SingleOrDefault(bu => bu.userid == ValidateUser.userid));
                        return Redirect("/Blogger/Index");

                    }
                    else
                    {

                        SessionsCreatorForModerator(db.moderators.SingleOrDefault(bu => bu.userid == ValidateUser.userid));
                        return Redirect("/Moderator/Index");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid login credentials.");
                }

            }

            return View(user);

        }
        public ActionResult Register()
        {
            return View();

        }
        [HttpPost]
        public ActionResult Register(user user)
        {

            if (ModelState.IsValid)
            {

                usersController.AddExistingUserToBlogers(usersController.AddNewUserToUsers(user));
                return Redirect("/EntryUsers/Login");

            }

            return View(user);
        }

        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction( "Login" ,"EntryUsers");
        }

        private void SessionsCreator(user user)
        {
            Session["userid"] = user.userid.ToString();
            Session["FirstName"] = user.first_name.ToString();
            Session["LastName"] = user.last_name.ToString();
            Session["email"] = user.email.ToString();
            Session["type"] = user.user_type.ToString();

        }
        private void SessionsCreatorForBloger(bloger bloger)
        {
            Session["bloggerid"] = bloger.blogerid.ToString();
        }
        private void SessionsCreatorForModerator(moderator moderator)
        {
            Session["moderatorid"] = moderator.moderatorid.ToString();

        }
    }
}