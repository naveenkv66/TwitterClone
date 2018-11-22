using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TwitterClone.Controllers
{
    public class TwitterController : Controller
    {
        // GET: Twitter
        public ActionResult Tweet()
        {
            if (Session["UserName"] != null)
            {

                return RedirectToAction("TwitterStream");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult TwitterStream()
        {
            ViewBag.UserName = Session["UserName"];
            return View();
        }
    }
}