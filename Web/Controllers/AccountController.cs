using Data.Repositories;
using Microsoft.AspNetCore.Mvc;
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

        [HttpPost] IActionResult SignIn(SignInViewModel model)
        {
            if (ModelState.IsValid)
            {

                return RedirectToAction("Topics", "Message");
            }
            else
            {
                return View(model);
            }
        }
    }
}
