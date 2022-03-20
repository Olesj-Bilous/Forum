using Data.Entities;

namespace Web.Models
{
    public class PostViewModel
    {
        public Post Post { get; set; }
        public IEnumerable<Reply> Replies { get; set; }
    }
}