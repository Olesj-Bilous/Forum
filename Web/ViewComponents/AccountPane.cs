using Data.Entities;
using Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Web.Extensions;

namespace Web.ViewComponents
{
    public class AccountPane : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(HttpContext.Session.GetObject<User>("User"));
        }
    }
}
