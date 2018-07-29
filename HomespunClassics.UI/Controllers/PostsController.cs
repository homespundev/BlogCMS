using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HomespunClassics.DATA;
using HomespunClassics.UI.Models;
using System.Data.Entity.Infrastructure;

namespace HomespunClassics.UI.Controllers
{
    public class PostsController : Controller
    {
        private HomespunClassicsDbEntities db = new HomespunClassicsDbEntities();

        // GET: Posts
        public ActionResult Archive()
        {
            var posts = db.Posts.Include(p => p.AspNetUser).Include(p => p.Category);
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
        public ActionResult Create([Bind(Include = "PostId,PostTitle,PostDescription,PostBody,PostAuthorID,CategoryID,Published,DateCreated")] Post post, string[] selectedTags)
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
            PopulateAssignedTagData(post);
            return View(post);
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
            PopulateAssignedTagData(post);
            return View(post);
        }

        private void PopulateAssignedTagData(Post post)
        {
            var allTags = db.Tags;
            var postTags = new HashSet<int>(post.Tags.Select(t => t.TagID));
            var viewModel = new List<PostTagViewModel>();
            foreach (var tag in allTags)
            {
                viewModel.Add(new PostTagViewModel
                {
                    TagID = tag.TagID,
                    TagName = tag.TagName,
                    Assigned = postTags.Contains(tag.TagID)
                });
            }
            ViewBag.Tags = new MultiSelectList(allTags, "TagID", "TagName", null, postTags);
        }
        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, string[] selectedTags)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var postToUpdate = db.Posts.Include(p => p.Tags).Where(i => i.PostId == id).Single();
            if (TryUpdateModel(postToUpdate, "", new string[] { "PostID", "PostName", "PostDescription", "PostBody", "PostAuthorID", "CategoryID", "Published", "DateCreated" }))
            {
                try
                {
                    UpdatePostTags(selectedTags, postToUpdate);
                    db.Entry(postToUpdate).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Archive");
                }
                catch (RetryLimitExceededException)
                {
                    ModelState.AddModelError("", "Unable to save changes, please try again");
                }
            }
            ViewBag.PostAuthorID = new SelectList(db.AspNetUsers, "Id", "Email", postToUpdate.PostAuthorID);
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", postToUpdate.CategoryID);
            PopulateAssignedTagData(postToUpdate);
            return View(postToUpdate);
        }

        private void UpdatePostTags(string[] selectedTags, Post postToUpdate)
        {
            if (selectedTags == null)
            {
                postToUpdate.Tags = new List<Tag>();
                return;
            }

            var selectedTagsHS = new HashSet<string>(selectedTags);
            var postTags = new HashSet<int>(postToUpdate.Tags.Select(t => t.TagID));
            foreach (var tag in db.Tags)
            {
                if (selectedTagsHS.Contains(tag.TagID.ToString()))
                {
                    if (!postTags.Contains(tag.TagID))
                    {
                        postToUpdate.Tags.Add(tag);
                    }
                }
                else
                {
                    if (postTags.Contains(tag.TagID))
                    {
                        postToUpdate.Tags.Remove(tag);
                    }
                }
            }
        }

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
    }
}
