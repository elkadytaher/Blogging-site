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
    public class BloggerController : Controller
    {
        // GET: Blogger
        private BlogsEntities db = new BlogsEntities();

        public ActionResult Index()

        {
           
            if (Session["type"].Equals("Blogger"))
            {
                var blogs = db.Blogs.Include(b => b.bloger);
                return View(blogs.ToList());
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

        }

        public ActionResult showMyBlogs() {
            if (Session["bloggerid"] != null)
            {
                int bloggerid = int.Parse(Session["bloggerid"].ToString());
                return View(db.Blogs.Where(b=>b.blogerid==bloggerid).ToList());
                
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

        }
        public ActionResult CreateBlog()
        {
            if (Session["type"].Equals("Blogger"))
            {
                return View();
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

        }
        [HttpPost]
        public ActionResult CreateBlog(Blog blog)
        {
            blog.blogerid = int.Parse(Session["bloggerid"].ToString());
            blog.blog_date = DateTime.Now;
            
            if (ModelState.IsValid)
            {
                db.Blogs.Add(blog);
                db.SaveChanges();

                return Redirect("/Blogger/AddImageToBlog");

            }


            return View(blog);
        }
        public ActionResult AddImageToBlog()
        {
            return View();
        }
        [HttpPost]

        public ActionResult AddImageToBlog(HttpPostedFileBase postedFile)
        {
            if (postedFile != null && Session["bloggerid"] != null)
            {
                string fileName = System.IO.Path.GetFileName(postedFile.FileName);
                string filePath = "~/Content/Images/" + fileName;
                postedFile.SaveAs(Server.MapPath(filePath));
                
                
                    int bloggerid = int.Parse(Session["bloggerid"].ToString());
                    Blog blog = db.Blogs.Where(b => b.blogerid == bloggerid).OrderByDescending(b => b.blog1).FirstOrDefault();
                    blog.blog_image = postedFile.FileName;
                    db.SaveChanges();
                    return Redirect("ShowMyBlogs");

                
            }
            else
            {
                return View();
            }


        }




    }
}