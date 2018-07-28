using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HomespunClassics.DATA;
using System.Data.Entity.Infrastructure;
using HomespunClassics.UI.Models;

namespace HomespunClassics.UI.Controllers
{
    public class PostsController1 : Controller
    {
        private HomespunClassicsDbEntities db = new HomespunClassicsDbEntities();
        // GET: Posts
        public ActionResult Index()
        {
            return RedirectToAction("Archive");
        }

        public ActionResult Archive()
        {
            var posts = db.Posts.Include(p => p.AspNetUser).Include(p => p.Category)/*.Where(p => p.Published == true)*/;
            return View(posts.ToList());
        }

        // GET: Posts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: Posts/Create
        public ActionResult Create()
        {
            ViewBag.PostAuthorID = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName");
            ViewBag.TagID = new SelectList(db.Tags, "TagID", "TagName");
            var post = new Post();
            post.Tags = new List<Tag>();
            PopulateAssignedTagData(post);
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PostId,PostTitle,PostDescription,PostBody,PostAuthorID,CategoryID,Published,DateCreated,TagId")] Post post, string[] selectedTags)
        {
            if (selectedTags != null)
            {
                post.Tags = new List<Tag>();
                foreach (var tag in selectedTags)
                {
                    var tagToAdd = db.Tags.Find(int.Parse(tag));
                    post.Tags.Add(tagToAdd);
                }
            }
            if (ModelState.IsValid)
            {
                db.Posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PostAuthorID = new SelectList(db.AspNetUsers, "Id", "Email", post.PostAuthorID);
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", post.CategoryID);
            ViewBag.TagID = new SelectList(db.Tags, "TagID", "TagName");
            PopulateAssignedTagData(post);
            return View(post);
        }
        private Tag GetTag(string tagName)
        {
            return db.Tags.Where(x => x.TagName == tagName).FirstOrDefault() ?? new Tag() { TagName = tagName };
        }
        // GET: Posts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Post post = db.Posts.Find(id);
            Post post = db.Posts.Include(p => p.Tags).Where(i => i.PostId == id).Single();
            if (post == null)
            {
                return HttpNotFound();
            }
            ViewBag.PostAuthorID = new SelectList(db.AspNetUsers, "Id", "Email", post.PostAuthorID);
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", post.CategoryID);
            ViewBag.TagID = new SelectList(db.Tags, "TagID", "TagName");
            ViewBag.AvailableTags = db.Tags.ToList();
            PopulateAssignedTagData(post);
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Post post)
        {
            var postToUpdate = post;

            if (ModelState.IsValid)
            {

                //UpdatePostTags(selectedTags, postToUpdate);

                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");

            }
            //db.Entry(post).State = EntityState.Modified;
            //db.SaveChanges();
            //return RedirectToAction("Index");
            ViewBag.PostAuthorID = new SelectList(db.AspNetUsers, "Id", "Email", post.PostAuthorID);
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", post.CategoryID);
            ViewBag.TagID = new SelectList(db.Tags, "TagID", "TagName");
            ViewBag.AvailableTags = db.Tags.ToList();

            return View(post);
        }

        //public ActionResult Edit(int? id, string[] selectedTags)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    var postToUpdate = db.Posts
        //        .Include(p => p.Tags)
        //        .Where(i => i.PostId == id)
        //        .Single();
        //    if (TryUpdateModel(postToUpdate, "",
        //           new string[] { "PostId,PostTitle,PostDescription,PostBody,PostAuthorID,CategoryID,Published,DateCreated" }))
        //    {
        //        try
        //        {
        //            UpdatePostTags(selectedTags, postToUpdate);

        //            db.Entry(postToUpdate).State = EntityState.Modified;
        //            db.SaveChanges();

        //            return RedirectToAction("Index");
        //        }
        //        catch (RetryLimitExceededException /* dex */)
        //        {
        //            //Log the error (uncomment dex variable name and add a line here to write a log.
        //            ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
        //        }
        //    }
        //    ViewBag.PostAuthorID = new SelectList(db.AspNetUsers, "Id", "Email", postToUpdate.PostAuthorID);
        //    ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", postToUpdate.CategoryID);
        //    //ViewBag.TagID = new SelectList(db.Tags, "TagID", "TagName");
        //    PopulateAssignedTagData(postToUpdate);
        //    return View(postToUpdate);
        //}
        
        // GET: Posts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
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
        private void PopulateAssignedTagData(Post post)
        {
            var allTags = db.Tags;
            var postTags = new HashSet<int>(post.Tags.Select(t => t.TagID));
            var viewModelAvailable = new List<PostTagViewModel>();
            var viewModelSelected = new List<PostTagViewModel>();
            foreach (var tag in allTags)
            {
                if (postTags.Contains(tag.TagID))
                {
                    viewModelSelected.Add(new PostTagViewModel
                    {
                        TagID = tag.TagID,
                        TagName = tag.TagName,
                        //Assigned = postTags.Contains(tag.TagID)
                    });
                }
                else
                {
                    viewModelAvailable.Add(new PostTagViewModel
                    {
                        TagID = tag.TagID,
                        TagName = tag.TagName

                    });
                }
            }
            ViewBag.selOpts = new MultiSelectList(viewModelSelected, "TagID", "TagName");
            ViewBag.availOpts = new MultiSelectList(viewModelAvailable, "TagID", "TagName");
        }
    }
}
