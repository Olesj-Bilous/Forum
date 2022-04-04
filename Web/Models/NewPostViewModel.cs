using Data.Entities;
using System.ComponentModel.DataAnnotations;
using Web.Validation;

namespace Web.Models
{
    public class NewPostViewModel
    {
        public int TopicId { get; set; }
        public Topic Topic { get; set; }

        [ForumRequired]
        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        [ForumRequired]
        public string Text { get; set; }
    }
}
