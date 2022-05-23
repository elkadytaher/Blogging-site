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
    public class Moderator_BlogsController : Controller
    {
        private BlogsEntities db = new BlogsEntities();

        // GET: Moderator_Blogs
        public ActionResult Index()
        {
            if (Session["moderatorid"] != null) { 
            var blogs = db.Blogs.Include(b => b.bloger);
            return View(blogs.ToList());
        }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        public ActionResult Details(int? id)
        {
            if (id == null )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        /*
        public ActionResult Create()
        {
            ViewBag.blogerid = new SelectList(db.blogers, "blogerid", "blogerid");
            return View();
        }

        // POST: Moderator_Blogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "blog1,blogerid,blog_date,blog_content,blog_image,blog_title")] Blog blog)
        {
            if (ModelState.IsValid)
            {
                db.Blogs.Add(blog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.blogerid = new SelectList(db.blogers, "blogerid", "blogerid", blog.blogerid);
            return View(blog);
        }

        // GET: Moderator_Blogs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            ViewBag.blogerid = new SelectList(db.blogers, "blogerid", "blogerid", blog.blogerid);
            return View(blog);
        }

        // POST: Moderator_Blogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "blog1,blogerid,blog_date,blog_content,blog_image,blog_title")] Blog blog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(blog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.blogerid = new SelectList(db.blogers, "blogerid", "blogerid", blog.blogerid);
            return View(blog);
        }
        */
        public ActionResult Delete(int? id)
        { 
            
            if (id == null || !(Session["moderatorid"] != null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Blog blog = db.Blogs.Find(id);
            db.Blogs.Remove(blog);
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
