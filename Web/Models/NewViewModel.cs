using Data.Entities;

namespace Web.Models
{
    public class NewViewModel
    {
        public int TopicId { get; set; }
        public Topic Topic { get; set; }
        public int SourceId { get; set; }
        public Message Source { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
    }
}
