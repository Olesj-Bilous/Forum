using Data.Entities;
using Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Web.Extensions;
using Web.Models;

namespace Web.Controllers
{
    public class MessageController : Controller
    {
        private readonly ILogger<MessageController> _logger;
        private readonly Context _context;

        public MessageController(ILogger<MessageController> logger, Context context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Topics()
        {
            var user = HttpContext.Session.GetObject<User>("User");

            if (user is not null)
            {
                TopicsViewModel model = new();

                //load posts with tracking because groupby needs
                //identity resolution to distinguish post topics
                IEnumerable<Post> posts = _context.Posts
                    .Include(x => x.Replies)
                    .Include(x => x.Topic)
                    .Include(x => x.Author);

                model.MainPostsByTopic = posts
                    .GroupBy(
                        //group posts by topic
                        x => x.Topic,
                        //extract maximum rated post from each topic
                        (topic, groupedPosts) => groupedPosts.MaxBy(y => y.RateSum))
                    //order pairs of topics and posts by post rating
                    .OrderByDescending(x => x.RateSum);

                return View(model);
            }
            else
            {
                return RedirectToAction("SignIn", "Account");
            }
        }

        public IActionResult Topic(int id)
        {
            TopicViewModel model = new();

            model.Topic = _context.Topics
                .Include(x => x.Posts)
                    .ThenInclude(y => y.Author)
                .Include(x => x.Posts)
                   .ThenInclude(y => y.Replies)
                .FirstOrDefault(x => x.Id == id);

            return View(model);
        }

        public IActionResult Post(int id)
        {
            PostViewModel model = new();

            model.Post = _context.Posts
                .Include(x => x.Replies)
                    .ThenInclude(x => x.Author)
                .Include(x => x.Topic)
                .Include(x => x.Author)
                .FirstOrDefault(x => x.Id == id);

            model.Replies = model.Post.Replies.Where(x => x.Source is null);

            return View(model);
        }

        public IActionResult NewPost(int id)
        {
            NewViewModel model = new();
            model.TopicId = id;
            model.Topic = _context.Topics.Find(id);
            return View("New", model);
        }

        [HttpPost]
        public IActionResult NewPost(NewViewModel model)
        {
            if (ModelState.IsValid)
            {
                //try
                //{
                    Post post = new();
                    post.AuthorId = 1;
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
                return View("New", model);
            }
        }

        public IActionResult NewReply(int id)
        {
            NewViewModel model = new();
            model.SourceId = id;
            var source = _context.Messages.Find(id);
            if (source is Post)
            {
                var entry = _context.Entry((Post)source);
                entry.Reference(x => x.Author).Load();
                entry.Reference(x => x.Topic).Load();
                entry.Collection(x => x.Replies).Load();
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
            }
            model.Source = source;
            return View("New", model);
        }

        [HttpPost]
        public IActionResult NewReply(NewViewModel model)
        {
            if (ModelState.IsValid)
            {
                Message source = _context.Messages.Find(model.SourceId);
                if (source is Reply)
                {
                    _context.Entry((Reply)source).Reference(x => x.Post).Load();
                }
                Reply reply = new();
                reply.AuthorId = 2;
                reply.Post = source is Post ? (Post)source : ((Reply)source).Post;
                reply.Source = source is Reply ? (Reply)source : null;
                reply.Text = model.Text;
                _context.Add(reply);
                _context.SaveChanges();
                return RedirectToAction("Post", new { id = reply.Post.Id });
            }
            else
            {
                return View("New", model);
            }
        }

        public IActionResult Author(int id)
        {
            AuthorViewModel model = new();
            model.Author = _context.Users.Find(id);
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}