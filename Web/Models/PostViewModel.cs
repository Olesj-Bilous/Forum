using Data.Entities;

namespace Web.Models
{
    public class PostViewModel
    {
        public int UserId { get; set; }
        public Post Post { get; set; }
        public IEnumerable<Reply> Replies { get; set; }
    }
}