using Data.Entities;

namespace Web.Models
{
    public class TopicsViewModel
    {
        public int UserId { get; set; }
        public IEnumerable<Post> MainPostsByTopic { get; set; }
    }
}
