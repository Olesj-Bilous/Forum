using Data.Entities;
using Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Web.Extensions;
using Web.Models;

namespace Web.Controllers
{
    public partial class MessageController : Controller
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

                    //order posts by rating
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