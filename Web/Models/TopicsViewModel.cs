using Data.Entities;

namespace Web.Models
{
    public class TopicsViewModel
    {
        public IEnumerable<Post> MainPostsByTopic { get; set; }
    }
}
