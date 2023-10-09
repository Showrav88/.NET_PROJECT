using labtask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace labtask.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Login l)
        {
            return View(l);
        }
        [HttpGet]
            public ActionResult SignUp()
            {
                return View();
            }
            [HttpPost]
            public ActionResult SignUp(SignUp s)
            {
                
                
                if (ModelState.IsValid)
                {
                    TempData["SignUpdata"] = s;
                    return RedirectToAction("Login");
                
                }
                return View(s);
            }
        }
    }
