﻿using System;
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
    public class Moderator_UsersController : Controller
    {
        private BlogsEntities db = new BlogsEntities();
        private UsersController usersController = new UsersController();


        // GET: Moderator_Users
        public ActionResult Index()
        {
            if (Session["moderatorid"] != null)
                return View(db.users.ToList());
            return HttpNotFound();
        }

        // GET: Moderator_Users/Details/5
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
            if (Session["moderatorid"] != null)
                return View();
            return HttpNotFound();
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
            if (id == null || !(Session["moderatorid"] != null))
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
