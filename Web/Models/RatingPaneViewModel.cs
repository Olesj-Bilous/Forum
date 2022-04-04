using Data.Entities;

namespace Web.Models
{
    public class RatingPaneViewModel
    {
        public int UserId { get; set; }
        public int Rate { get; set; } = 0;
        public Message Message { get; set; }
    }
}
