using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data.Entities;
using Data.Repositories;
using Web.Models;

namespace Web.Controllers
{
    [Route("Rate")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly Context _context;

        public RatingController(Context context)
        {
            _context = context;
        }

        // POST: api/Rating
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostRating([FromBody] RatingViewModel model)
        {
            var rating = await _context.Ratings.FindAsync(model.UserId, model.MessageId);
            var msg = await _context.Messages.FindAsync(model.MessageId);
            
            if (rating is null)
            {
                rating = new Rating();
                rating.UserId = model.UserId;
                rating.MessageId = model.MessageId;
                rating.Rate = model.Rate;
                _context.Ratings.Add(rating);
                msg.RateSum += model.Rate;
            }
            else
            {
                if (rating.Rate == model.Rate)
                {
                    _context.Ratings.Remove(rating);
                    msg.RateSum -= model.Rate;
                }
                else
                {
                    rating.Rate = model.Rate;
                    _context.Ratings.Attach(rating).State = EntityState.Modified;
                    msg.RateSum += 2 * model.Rate;
                }
            }
            _context.Messages.Attach(msg).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw;
            }
            return Ok();
        }

        // DELETE: api/Rating/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRating(int id)
        {
            var rating = await _context.Ratings.FindAsync(id);
            if (rating == null)
            {
                return NotFound();
            }

            _context.Ratings.Remove(rating);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RatingExists(int userId, int msgId)
        {
            return _context.Ratings.Any(x => x.UserId == userId && x.MessageId == msgId);
        }
    }
}
