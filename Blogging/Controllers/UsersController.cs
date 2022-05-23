using Blogging.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blogging.Controllers
{
    public class UsersController : Controller
    {
        // GET: Users
        BlogsEntities db = new BlogsEntities();
        public ActionResult Index()
        {
            return View();

        }
 
        public user AddNewUserToUsers(user user)
        {
            db.users.Add(user);
            db.SaveChanges();
            return db.users.SingleOrDefault(u => u.email.ToString().Equals(user.email) && u.password.ToString().Equals(user.password)); ;
        }

        public void AddExistingUserToModerators(user user)
        {
            user.user_type = "Moderator";
            moderator moderator = new moderator();
            moderator.userid = user.userid;
            db.moderators.Add(moderator);
            db.SaveChanges();
        }

        public void AddExistingUserToBlogers(user user) 
        {
            user.user_type = "Blogger";
            bloger bloger = new bloger();
            bloger.userid= user.userid;
            db.blogers.Add(bloger);
            db.SaveChanges();
        }
    }
}