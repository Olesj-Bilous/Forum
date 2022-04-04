using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Web.Extensions;
using Web.Models;

namespace Web.Controllers
{
    public partial class MessageController
    {
        public IActionResult NewPost(int id)
        {
            NewPostViewModel model = new();
            model.TopicId = id;
            model.Topic = _context.Topics.Find(id);
            return View(model);
        }

        [HttpPost]
        public IActionResult NewPost(NewPostViewModel model)
        {
            if (ModelState.IsValid)
            {
                //try
                //{
                Post post = new();
                post.AuthorId = HttpContext.Session.GetObject<User>("User").Id;
                post.TopicId = model.TopicId;
                post.Title = model.Title;
                post.Text = model.Text;
                _context.Posts.Add(post);
                _context.SaveChanges();
                return RedirectToAction("Topic", new { id = model.TopicId });
                //}
                //catch
                //{
                //    return Error();
                //}
            }
            else
            {
                model.Topic = _context.Topics.Find(model.TopicId);
                return View(model);
            }
        }

        public IActionResult NewReply(int id)
        {
            NewReplyViewModel model = new();
            model.SourceId = id;
            var source = _context.Messages.Find(id);
            if (source is Post)
            {
                var post = (Post)source;

                var entry = _context.Entry(post);
                entry.Reference(x => x.Author).Load();
                entry.Reference(x => x.Topic).Load();
                entry.Collection(x => x.Replies).Load();

                model.Post = post;
            }
            else
            {
                var reply = (Reply)source;

                var entry = _context.Entry(reply);
                entry.Reference(x => x.Author).Load();
                entry.Reference(x => x.Post).Load();

                var postEntry = _context.Entry(reply.Post);
                postEntry.Reference(x => x.Author).Load();
                postEntry.Reference(x => x.Topic).Load();

                model.Post = reply.Post;
                model.Reply = reply;
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult NewReply(NewReplyViewModel model)
        {
            if (ModelState.IsValid)
            {
                Message source = _context.Messages.Find(model.SourceId);
                if (source is Reply)
                {
                    _context.Entry((Reply)source).Reference(x => x.Post).Load();
                }
                Reply reply = new();
                reply.AuthorId = HttpContext.Session.GetObject<User>("User").Id;
                reply.Post = source is Post ? (Post)source : ((Reply)source).Post;
                reply.Source = source is Reply ? (Reply)source : null;
                reply.Text = model.Text;
                _context.Add(reply);
                _context.SaveChanges();
                return RedirectToAction("Post", new { id = reply.Post.Id });
            }
            else
            {
                return View(model);
            }
        }
    }
}
