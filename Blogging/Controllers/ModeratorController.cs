using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blogging.Controllers
{
    public class ModeratorController : Controller
    {
        // GET: Moderator
        public ActionResult Index()
        {
           if( Session["moderatorid"] != null)
            return View();
            return HttpNotFound();

        }
    }
}