using Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class NewViewModel
    {
        public int TopicId { get; set; }
        public Topic Topic { get; set; }
        public int SourceId { get; set; }
        public Message Source { get; set; }
        public string Title { get; set; }
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }
    }
}
