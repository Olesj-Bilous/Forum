using Data.Entities;
using System.ComponentModel.DataAnnotations;
using Web.Validation;

namespace Web.Models
{
    public class NewReplyViewModel
    {
        public int SourceId { get; set; }
        public Post Post { get; set; }
        public Reply Reply { get; set; }

        [DataType(DataType.MultilineText)]
        [ForumRequired]
        public string Text { get; set; }
    }
}
