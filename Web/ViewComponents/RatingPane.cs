using Data.Entities;
using Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Web.Extensions;
using Web.Models;

namespace Web.ViewComponents
{
    public class RatingPane : ViewComponent
    {
        private readonly Context _context;

        public RatingPane(Context context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(Message msg)
        {
            RatingPaneViewModel model = new();
            var userId = HttpContext.Session.GetObject<User>("User").Id;
            var rating = await _context.Ratings.FindAsync(userId, msg.Id);
            model.UserId = userId;
            model.Message = msg;
            model.Rate = rating is null ? 0 : rating.Rate;
            return View(model);
        }
    }
}
