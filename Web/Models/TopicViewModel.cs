using Data.Entities;

namespace Web.Models
{
    public class TopicViewModel
    {
        public int UserId { get; set; }
        public Topic Topic { get; set; }
    }
}
