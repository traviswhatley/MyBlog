using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlog.Controllers
{
    //security
    //must be logged in to do anything
    [Authorize()]
    public class PostController : Controller
    {
        //connecting to database
        Models.MyBlogEntities db = new Models.MyBlogEntities();
        //
        // GET: /Post/
      
        public ActionResult Index()
        {
            //will return all of logged in users posts
            return View(db.Posts.Where(x=>x.Username.ToLower() == User.Identity.Name.ToLower()));
        }

        // GET: /Post/Create
        [HttpGet]
        public ActionResult Create()
        {
            //pass blank post object to view
            return View(new Models.Post());
        }

        // POST: /Post/Create

        [HttpPost]
        public ActionResult Create(Models.Post post)
        {
            //setting authors name to post
            post.Username = User.Identity.Name;
            //setting time it was made
            post.DateCreated = System.DateTime.Now;
            //setting likes
            post.Likes = 0;
            //make sure that imageURL has a value
            if (post.ImageURL == null)
            {
                post.ImageURL = "";
            }
            //add/save to database
            db.Posts.Add(post);
            db.SaveChanges();

            return RedirectToAction("Index", "Post");
        }

        // GET: /Posts/Delete/{id}
        [HttpGet]
        public ActionResult Delete(int id)
        {
            //get post from database
            Models.Post postToDelete = db.Posts.Find(id);
            return View(postToDelete);
        }

        // POST: /Post/Delete/{id}
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Models.Post postToDelete = db.Posts.Find(id);
            db.Posts.Remove(postToDelete);
            db.SaveChanges();

            return RedirectToAction("Index", "Post");
        }

        // GET: /Post/Edit/{id}
        [HttpGet]
        public ActionResult Edit(int id)
        {
            //get post to edit from database
            Models.Post postToEdit = db.Posts.Find(id);
            return View(postToEdit);
        }

        // POST: /Post/Edit/{id}
        [HttpPost]
        public ActionResult Edit(Models.Post postToEdit)
        {
            //set the post to the updated
            db.Entry(postToEdit).State = System.Data.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", "Post");
        }
    }
}
