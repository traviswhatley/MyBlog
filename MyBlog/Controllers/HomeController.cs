using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlog.Controllers
{
    public class HomeController : Controller
    {
        //set connection to the database
        Models.MyBlogEntities db = new Models.MyBlogEntities();

        //
        // GET: /Home/

        public ActionResult Index()
        {
            //pass posts to the view, order by newest first
            return View(db.Posts.OrderByDescending(x=>x.DateCreated));
        }

        // GET: /Home/Like/id

        public ActionResult Like(int id)
        {
            //goes to the database and grabs the post that matches the post ID
            Models.Post post = db.Posts.Find(id);
            //add 1 to the post likes
            post.Likes++;
            //save changes
            db.SaveChanges();
            return Content(post.Likes + " likes");
        }

        //AJAX POST: /Home/addComment

        public ActionResult addComment(Models.Comment c)
        {
            //set the other properties of the comment
            c.DateCreated = DateTime.Now;
            //add to db
            db.Comments.Add(c);
            //save
            db.SaveChanges();
            //return the comment view
            return PartialView("comment", c);
        }

    }
}
