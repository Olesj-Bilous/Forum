using Data.Entities;
using Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Web.Extensions;
using Web.Models;

namespace Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly Context _context;

        public AccountController(ILogger<AccountController> logger, Context context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult SignIn()
        {
            return View(new SignInViewModel());
        }

        [HttpPost]
        public IActionResult SignIn(SignInViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Users.FirstOrDefault(x => x.Name == model.Username);

                model.UserNotFound = user is null;
                if (!model.UserNotFound)
                {
                    model.WrongPassword = user.Password != model.Password;
                    if (!model.WrongPassword)
                    {
                        HttpContext.Session.SetObject("User", user);
                        return RedirectToAction("Topics", "Message");
                    }
                }
            }
            return View(model);
        }

        public IActionResult ForumSignOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Topics", "Message");
        }

        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.NameNotUnique = _context.Users.Any(x => x.Name == model.Username);
                if (!model.NameNotUnique)
                {
                    model.EmailNotUnique = _context.Users.Any(x => x.EmailAddress == model.Email);
                    if (!model.EmailNotUnique)
                    {
                        User user = new();
                        user.Name = model.Username;
                        user.Password = model.Password;
                        user.EmailAddress = model.Email;

                        _context.Users.Add(user);
                        _context.SaveChanges();

                        HttpContext.Session.SetObject("User", user);

                        return RedirectToAction("Topics", "Message");
                    }
                }
            }
            return View(model);
        }

        public IActionResult Profile(int id)
        {
            return View();
        }
    }
}
