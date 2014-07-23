using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security; //adding using for membership

namespace MyBlog.Controllers
{
    public class AccountController : Controller
    {
        //add connection to myBlog database
        Models.MyBlogEntities db = new Models.MyBlogEntities();
        //
        // GET: /Account/

        public ActionResult Index()
        {
            //to create a user
            Membership.CreateUser("admin", "techIsFun1");

            //to log someone in
            if (Membership.ValidateUser("admin", "techIsFun1"))
            {
                //user/pass is match log them in
                FormsAuthentication.SetAuthCookie("admin", false);
            }
            return View();
        }

        // GET: /Account/Register
        public ActionResult Register()
        {
            //creating a blank registration object
            return View(new Models.Register());
        }

        // POST: /Account/Register
        [HttpPost]
        public ActionResult Register(Models.Register reg)
        {
            //post action receives a register object
            //1) check to see if username is already in use
            if (Membership.GetUser(reg.Username) == null)
            {
                //username is valid, so add user to db
                Membership.CreateUser(reg.Username, reg.Password);
                //add user to myBlog authors table
                Models.Author author = new Models.Author();
                //set properties of our author
                author.Username = reg.Username;
                author.Name = reg.Name;
                author.ImageURL = reg.ImageURL;
                //add author to database
                db.Authors.Add(author);
                db.SaveChanges();
                //log user in
                FormsAuthentication.SetAuthCookie(reg.Username, false);
                //kick user back to landing (home) page
                return RedirectToAction("Index", "Home");
            }
            else
            {
                //username is already in use
                ViewBag.Error = "Username is already in use. Please choose another.";
                //return view with reg object
                return View(reg);
            }
        }

        // GET: /Account/Logout
        [HttpGet]
        public ActionResult Logout()
        {
            //log someone out
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        // GET: /Account/Login
        [HttpGet]
        public ActionResult Login()
        {
            //log in user
            return View(new Models.Login());
        }

        // POST: Account/Login
        [HttpPost]
        public ActionResult Login(Models.Login log)
        {
            if (Membership.ValidateUser(log.Username, log.Password))
            {
                FormsAuthentication.SetAuthCookie(log.Username, false);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Error = "Login failed. Try again";
                return View(log);
            }
        }

    }
}
