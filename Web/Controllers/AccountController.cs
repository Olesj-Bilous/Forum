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
                
                if (user is not null)
                {
                    if (user.Password == model.Password)
                    {
                        HttpContext.Session.SetObject("User", user);
                        return RedirectToAction("Topics", "Message");
                    }
                    else
                    {
                        model.WrongPassword = true;
                        return View(model);
                    }
                }
                else
                {
                    model.UserNotFound = true;
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }
    }
}
