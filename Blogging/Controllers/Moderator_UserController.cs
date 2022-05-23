using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Blogging.Models;

namespace Blogging.Controllers
{
    public class Moderator_UserController : Controller
    {
        private BlogsEntities db = new BlogsEntities();
        private UsersController usersController = new UsersController();

        public ActionResult Index()
        {
            return View(db.users.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        public ActionResult Create()
        {
            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "userid,first_name,last_name,email,password,user_type")] user user)
        {
            if (ModelState.IsValid)
            {
            

                if (user.user_type.Equals("Blogger"))
                {
                    usersController.AddExistingUserToBlogers(usersController.AddNewUserToUsers(user));

                }
                else
                {
                    usersController.AddExistingUserToModerators(usersController.AddNewUserToUsers(user));

                }


                return RedirectToAction("Index");
            }

            return View(user);
        }
      
      

        
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            user user = db.users.Find(id);
            db.users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
